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
using ClinicaMedicalaForm.components.Model.Users;
using FisaMedicalaForm;

namespace ClinicaMedicalaForm.components.Model
{
    public class Model : IModel
    {
        private UserFactory _userFactory;
        private List<IUser> _users;
        private string _userName;
        private SQLiteConnection connection;
        public Model()
        {
            string location=Directory.GetCurrentDirectory()+ "\\..\\..\\components\\Resources\\ClinicaMedicala-DB.db";
            string dataSource = "Data Source="+location+";Version=3;";
            try
            {
                connection = new SQLiteConnection(dataSource);
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return false;
            }
        }
        public SQLiteConnection GetConnection()
        {
            return connection;
        }
        public List<IUser> CitireUtilizatori()
        {
            _userFactory = new UserFactory();
            _users = new List<IUser>();
            string Tableee = "Users";
            string query = $"SELECT * FROM {Tableee};";
            using (var command = new SQLiteCommand(query, connection))
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int k = 0;
                    string []infoArray=new string[6];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string columnName = reader.GetName(i);
                        object value = reader.GetValue(i);
                        infoArray[k++] = value.ToString();
                    }
                    _users.Add(_userFactory.CreateUser(infoArray));
                }
            }
            return _users;
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
