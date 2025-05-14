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
        private List<IUser> _users;
        private List<Programare> _programari;    
        private List<IUser> _pacienti;    
        private DatabaseManager _databaseManager;
        public Model()
        {
            string location=Directory.GetCurrentDirectory()+ "\\..\\..\\components\\Resources\\ClinicaMedicala-DB.db";
            string dataSource = "Data Source="+location+";Version=3;";

            _databaseManager = new DatabaseManager(dataSource);
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
        public List<Programare> Programari => _programari;
        public List<IUser> Pacienti => _pacienti;
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
                    }
                    else
                    {
                        ok = true;
                    }
                    _programari.Add(new Programare(pacID, docID, ok, infoArray[4], infoArray[3]));
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
        public void CitirePacienti()
        {
            //throw new NotImplementedException();
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
