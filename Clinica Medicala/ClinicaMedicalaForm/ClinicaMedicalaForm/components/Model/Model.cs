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
        private DatabaseManager _databaseManager;
        private List<IUser> _users;
        private List<IUser> _pacienti;
        private List<IUser> _doctori;
        public List<Programare> Programari => _programari;
        public List<IUser> Doctori => _doctori;
        public List<IUser> Pacienti => _pacienti;

        public Model()
        {
            string location=Directory.GetCurrentDirectory()+ "\\..\\..\\components\\Resources\\ClinicaMedicalaDB.db";
            string dataSource = "Data Source="+location+";Version=3;";

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
                while(reader.Read())
                {
                    int k = 0;

                    // trebuie pusa exceptie aici la new string[6] (daca nu sunt 6, ce face?)
                    string[] infoArray = new string[6];
                    for(int i = 0; i < reader.FieldCount; i++)
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
            foreach(IUser user in _users)
            {
                if(user.Rol == "Doctor")
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
                    int pacID,docID;
                    bool ok;
                    int.TryParse(infoArray[1], out pacID);
                    int.TryParse(infoArray[2], out docID);
                    if (infoArray[5] == "Valabila")
                    {
                        ok = false;
                        _programari.Add(new Programare(pacID, docID, ok, infoArray[4], infoArray[3]));
                    }
                    else if (infoArray[5] == "Nevalabila")
                    {
                        ok = true;
                    }
                    else if (infoArray[5] == "In curs de validare")
                    {

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

        public List<string> GetProgramariIstoric()
        {

            return null;
            //TO DO;
        }

        public List<Form> GetIstoric()
        {
            return null;
            //TO DO;
        }
        public List<string> GetCurrentProgramari()
        {
            return null;
            //TO DO;
        }
    }
}
