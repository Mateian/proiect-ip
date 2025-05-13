using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaMedicalaForm.components.Model.Users;

namespace ClinicaMedicalaForm.components.Model.Factory
{
    public class UserFactory
    {
        public IUser CreateUser(string[] infoArray)
        {
            int.TryParse(infoArray[0], out int ID);
            string rol = infoArray[1];
            string username = infoArray[2];
            string parola = infoArray[3];
            string nume = infoArray[4];
            string prenume = infoArray[5];

            switch(rol)
            {
                case "Administrator":
                    return new Administrator(ID, username, parola, nume, prenume);
                case "Pacient":
                    return new Pacient(ID, username, parola, nume, prenume);
            }
            return null;
        }
    }
}
