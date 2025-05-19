using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClinicaMedicalaForm.components.Model.Factory;
using ClinicaMedicalaForm.components.Model.Interfaces;
using ClinicaMedicalaForm.components.Model.Medical;
using ClinicaMedicalaForm.components.Model.Users;
using FisaMedicalaForm;


namespace ClinicaMedicalaForm.components.Model
{
    public class Model : IModel
    {
        private UserFactory _userFactory;
        private SQLiteConnection _connection;
        private List<Programare> _programari;
        private List<FisaMedicala> _fiseMedicale;
        private DatabaseManager _databaseManager;
        private List<IUser> _users;
        private List<IUser> _pacienti;
        private List<IUser> _doctori;
        public List<IUser> Utilizatori => _users;
        public List<Programare> Programari => _programari;
        public List<IUser> Doctori => _doctori;
        public List<IUser> Pacienti => _pacienti;

        public Model()
        {
            string location = Directory.GetCurrentDirectory() + "\\..\\..\\components\\Resources\\ClinicaMedicalaDB.db";
            string dataSource = "Data Source=" + location + ";Version=3;";

            _databaseManager = new DatabaseManager(dataSource);
        }
        public void AdaugareFisaMedicala(List<string> dateFisaMedicala)
        {
            _databaseManager.InsertCommand(dateFisaMedicala);
        }
        public List<IUser> CitireUtilizatori()
        {
            _userFactory = new UserFactory();
            _users = new List<IUser>();
            string tableName = "Users";
            string query = $"SELECT * FROM {tableName};";
            var reader = _databaseManager.ExecuteSelectQuery(query);
            try
            {
                while (reader.Read())
                {
                    int k = 0;

                    // trebuie pusa exceptie aici la new string[6] (daca nu sunt 6, ce face?)
                    string[] infoArray = new string[6];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string columnName = reader.GetName(i);
                        object value = reader.GetValue(i);
                        infoArray[k++] = value.ToString();
                    }

                    _users.Add(_userFactory.CreateUser(infoArray));
                }
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }
            return _users;
        }
        public void CitireDoctori()
        {
            _doctori = new List<IUser>();
            foreach (IUser user in _users)
            {
                if (user.Rol == "Doctor")
                {
                    _doctori.Add(user);
                }
            }
        }
        public void CitirePacienti()
        {
            _pacienti = new List<IUser>();

            string tableName = "Pacienti";
            string query = $"SELECT * FROM {tableName};";
            var reader = _databaseManager.ExecuteSelectQuery(query);

            try
            {
                while (reader.Read())
                {
                    int ID = reader.GetInt32(0);
                    int doctorID = reader.GetInt32(1);
                    int pacientID = reader.GetInt32(2);

                    var pacientUser = _users.FirstOrDefault(u => u.ID == pacientID && u.Rol == "Pacient");
                    var doctorUser = _doctori.FirstOrDefault(d => d.ID == doctorID);

                    if (pacientUser != null && doctorUser != null)
                    {
                        Pacient pacient = pacientUser as Pacient;
                        Doctor doctor = doctorUser as Doctor;

                        if (pacient != null && doctor != null)
                        {
                            pacient.Doctor = doctor;
                            _pacienti.Add(pacient);
                        }
                    }
                }
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }
        }
        public void CitireProgramari()
        {
            string tableName = "Programari";
            _programari = new List<Programare>();
            var reader = _databaseManager.ExecuteSelectQuery($"SELECT * FROM {tableName};");
            try
            {
                while (reader.Read())
                {
                    int k = 0;

                    // trebuie pusa exceptie aici la new string[6] (daca nu sunt 6, ce face?)
                    string[] infoArray = new string[6];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        object value = reader.GetValue(i);
                        infoArray[k++] = value.ToString();
                    }
                    int pacID, docID;
                    int.TryParse(infoArray[1], out pacID);
                    int.TryParse(infoArray[2], out docID);
                    _programari.Add(new Programare(pacID, docID, infoArray[3], infoArray[4], infoArray[5]));
                }
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }
        }

        public IUser VerificaAutentificare(string username, string parola)
        {
            foreach (IUser user in _users)
            {
                if (user.Username == username && user.Parola == parola)
                {
                    return user;
                }
            }
            return null;
        }
        public void AdaugaProgramareViitoare(Programare programare)
        {
            Programari.Add(programare);
            string tableName = "Programari";
            string query = $"INSERT INTO {tableName}(PacientID, DoctorID, Date, Specializare, Valabilitate) " +
               "VALUES (@PacientID, @DoctorID, @Date, @Specializare, @Valabilitate);";

            var parameters = new Dictionary<string, object>
            {
                { "@PacientID", programare.PacientID },
                { "@DoctorID", programare.DoctorID },
                { "@Date", programare.Data },
                { "@Specializare", programare.Specializare },
                { "@Valabilitate", programare.Valabilitate }
            };
            _databaseManager.ExecuteNonQuery(query, parameters);
        }
        public void AdaugaProgramare(Programare programare)
        {
            Programari.Add(programare);
            string tableName = "Programari";
            string query = $"INSERT INTO {tableName}(PacientID, DoctorID, Date, Specializare, Valabilitate) " +
               "VALUES (@PacientID, @DoctorID, @Date, @Specializare, @Valabilitate);";

            var parameters = new Dictionary<string, object>
            {
                { "@PacientID", programare.PacientID },
                { "@DoctorID", programare.DoctorID },
                { "@Date", programare.Data },
                { "@Specializare", programare.Specializare },
                { "@Valabilitate", programare.Valabilitate }
            };
            _databaseManager.ExecuteNonQuery(query, parameters);
        }
        public List<FisaMedicala> PreluareIstoricMedical(int userID)
        {
            _fiseMedicale = new List<FisaMedicala>();
            string query = $"SELECT Nume, Prenume FROM Users WHERE ID = {userID}";
            string nume = "";
            string prenume = "";
            string numeComplet = "";
            using (var reader = _databaseManager.ExecuteSelectQuery(query))//se selecteaza persoana dupa ID din tabelul Users
            {
                if (reader.Read())
                {
                    nume = reader["Nume"].ToString();
                    prenume = reader["Prenume"].ToString();
                    numeComplet = $"{nume} {prenume}";
                }
            }
            if (!string.IsNullOrEmpty(numeComplet))
            {
                query = $"SELECT DataConsult, NumeMedic,ExamenClinic,DiagnosticPrezumtiv,Recomandari,InvestigatiiRecomandate,TratamentPrescris,Motiv FROM FisaMedicalaDB WHERE NumePacient = '{numeComplet}'";
                using (var reader = _databaseManager.ExecuteSelectQuery(query))//se selecteaza toate fisele cu numele persoanei din tabelul FisaMedicalaDB
                {
                    while (reader.Read())
                    {
                        FisaMedicala fisa = new FisaMedicala(
                            nume,
                            prenume,
                            reader["DataConsult"].ToString(),
                            reader["Motiv"].ToString(),
                            reader["DiagnosticPrezumtiv"].ToString(),
                            reader["TratamentPrescris"].ToString(),
                            reader["NumeMedic"].ToString(),
                            reader["ExamenClinic"].ToString(),
                            reader["Recomandari"].ToString(),
                            reader["InvestigatiiRecomandate"].ToString()
                        );
                        _fiseMedicale.Add(fisa);
                    }
                }
            }
            return _fiseMedicale;
        }
        public string PreviewIstoric(int nrFisa)
        {
            //Se selecteaza fisa cu nrFisa corespunzator si se apeleaza functia de generare a textului
            FisaMedicala fisa = _fiseMedicale[nrFisa];
            return fisa.GeneratePreview();
        }
        public void DeletePacient(int id)
        {
            Pacient pacient = (Pacient)_users.FirstOrDefault(u => u.ID == id);
            _pacienti.Remove(pacient);
            List<Programare> programariPacientDeSters = _programari.FindAll(p => p.PacientID == pacient.ID);
            foreach (var aux in programariPacientDeSters)
            {
                _programari.Remove(aux);
            }
            string tableName = "Pacienti";
            string query = $"DELETE FROM {tableName} WHERE PacientID = @ID";
            var parameters = new Dictionary<string, object>
            {
                { "@ID", id }
            };
            _databaseManager.ExecuteNonQuery(query, parameters);

            tableName = "Programari";
            query = $"DELETE FROM {tableName} WHERE PacientID = @ID";
            _databaseManager.ExecuteNonQuery(query, parameters);

            tableName = "Users";
            query = $"DELETE FROM {tableName} WHERE ID = @ID";
            _databaseManager.ExecuteNonQuery(query, parameters);
        }
        public void DeleteDoctor(int id)
        {
            Doctor doctor = (Doctor)_users.FirstOrDefault(u => u.ID == id);
            _doctori.Remove(doctor);
            List<Programare> programariPacientDeSters = _programari.FindAll(p => p.DoctorID ==  doctor.ID);
            foreach (var aux in programariPacientDeSters)
            {
                _programari.Remove(aux);
            }
            string tableName = "Pacienti";
            string query = $"DELETE FROM {tableName} WHERE DoctorID = @ID";
            var parameters = new Dictionary<string, object>
            {
                { "@ID", id }
            };
            _databaseManager.ExecuteNonQuery(query, parameters);

            tableName = "Programari";
            query = $"DELETE FROM {tableName} WHERE DoctorID = @ID";
            _databaseManager.ExecuteNonQuery(query, parameters);

            tableName = "Users";
            query = $"DELETE FROM {tableName} WHERE ID = @ID";
            _databaseManager.ExecuteNonQuery(query, parameters);
        }
        public void StergeUser(int id)
        {
            foreach(IUser user in _users)
            {
                if(user.ID == id && user.Rol == "Pacient")
                {
                    DeletePacient(user.ID);
                }
                if(user.ID == id && user.Rol == "Doctor")
                {
                    DeleteDoctor(user.ID);
                }
            }
        }
        public void AdaugaPacient(int doctorID, Pacient pacient)
        {
            Doctor doctor = (Doctor)_users.FirstOrDefault(u => u.ID == doctorID);
            pacient.Doctor = doctor;
            Pacienti.Add(pacient);

            string tableName = "Pacienti";
            string query = $"INSERT INTO {tableName}(DoctorID, PacientID) VALUES(@DoctorID, @PacientID);";
            var parameters = new Dictionary<string, object>
            {
                { "@DoctorID", doctorID },
                { "@PacientID", pacient.ID }
            };
            _databaseManager.ExecuteNonQuery(query, parameters);
        }

        public void AdaugaDoctor(Doctor doctor)
        {
            string tableName = "Users";
            string query = $"INSERT INTO {tableName}(Role, Username, HashPassword, Nume, Prenume) VALUES(@Role, @Username, @HashPassword, @Nume, @Prenume);";
            var parameters = new Dictionary<string, object>
            {
                { "@Role", doctor.Rol},
                { "@Username", doctor.Username },
                { "@HashPassword", doctor.Parola},
                { "@Nume", doctor.Nume },
                { "@Prenume", doctor.Prenume}
            };
            _databaseManager.ExecuteNonQuery(query, parameters);

            tableName = "Users";
            query = $"SELECT ID FROM {tableName} WHERE Role = '{doctor.Rol}' AND Username = '{doctor.Username}' AND HashPassword = '{doctor.Parola}' AND Nume = '{doctor.Nume}' AND Prenume = '{doctor.Prenume}';";
            using (var reader = _databaseManager.ExecuteSelectQuery(query))
            {
                while (reader.Read())
                {
                    string id = reader["ID"].ToString();
                    Doctor doctorNou = new Doctor(int.Parse(id), doctor.Username, doctor.Parola, doctor.Nume, doctor.Prenume);
                    Doctori.Add(doctorNou);
                    Utilizatori.Add(doctorNou);
                }
            }
            
       }

        public void ValidareProgramare(Programare programare)
        {
            Programare newProg = new Programare(programare.PacientID, programare.DoctorID, programare.Data, programare.Specializare, "Valabila");
            Programari.Add(newProg);
            Programari.Remove(programare);
            string tableName = "Programari";
            string query = $"INSERT INTO {tableName}(PacientID, DoctorID, Date, Specializare, Valabilitate) " +
               "VALUES (@PacientID, @DoctorID, @Date, @Specializare, @Valabilitate);";

            var parameters = new Dictionary<string, object>
            {
                { "@PacientID", newProg.PacientID },
                { "@DoctorID", newProg.DoctorID },
                { "@Date", newProg.Data },
                { "@Specializare", newProg.Specializare },
                { "@Valabilitate", newProg.Valabilitate }
            };
            _databaseManager.ExecuteNonQuery(query, parameters);

            tableName = "Programari";
            query = $"DELETE FROM {tableName} WHERE PacientID = @PacientID AND DoctorID = @DoctorID and Date = @Data AND Valabilitate = @Valabilitate";
            parameters = new Dictionary<string, object>
            {
                { "@PacientID", programare.PacientID },
                { "@DoctorID", programare.DoctorID },
                { "@Data", programare.Data },
                { "@Valabilitate", programare.Valabilitate }
            };
            _databaseManager.ExecuteNonQuery(query, parameters);
        }
        public bool CheckUserExists(List<string> data)
        {
            return _databaseManager.CheckUserExists(data);
        }
        public IUser InsertUserCommand(List<string> data)
        {
            int id = _databaseManager.InsertUserCommand(data);
            if (id < 0)
            {
                throw new Exception("Eroare user!");
            }
            else
            {
                Pacient pacient = new Pacient(id, data[0], data[1], data[2], data[3]);
                _users.Add(pacient);
                _pacienti.Add(pacient);
                return pacient;
            }
        }
    }
}
