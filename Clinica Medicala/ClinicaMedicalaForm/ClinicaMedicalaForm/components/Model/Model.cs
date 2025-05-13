using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaMedicalaForm.components.Model.Factory;
using ClinicaMedicalaForm.components.Model.Interfaces;

namespace ClinicaMedicalaForm.components.Model
{
    public class Model : IModel
    {
        private UserFactory _userFactory;
        private List<IUser> _users;
        public List<IUser> CitireUtilizatori()
        {
            _users = new List<IUser>();
            StreamReader streamReader = new StreamReader(Directory.GetCurrentDirectory() + "\\..\\..\\data\\users.txt");
            string line = "";
            while((line = streamReader.ReadLine()) != null)
            {
                // ID ROL USERNAME PAROLA NUME PRENUME
                string[] infoArray = line.Split(' ');
                
                // Data din Array
                

                // user factory ... trebuie verificat rolul, creat o clasa "UserFactory" in care dupa rolul citit, se va crea
                // un utilizator de tipul rol. Se va face intr-un switch
                _userFactory = new UserFactory();
                _users.Add(_userFactory.CreateUser(infoArray));
            }
            return _users;
        }
        public bool VerificaAutentificare(string username, string parola)
        {
            foreach(IUser user in _users)
            {
                if(user.Username == username && user.Parola == parola)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
