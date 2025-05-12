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
        public void CitireUtilizatori()
        {
            StreamReader streamReader = new StreamReader("\\data\\users.txt");
            string line = "";
            while((line = streamReader.ReadLine()) != null)
            {
                // ID ROL USERNAME PAROLA NUME PRENUME
                string[] infoArray = line.Split(' ');
                
                // Data din Array
                int.TryParse(infoArray[0], out int ID);
                string rol = infoArray[1];
                string username = infoArray[2];
                string parola = infoArray[3];
                string nume = infoArray[4];
                string prenume = infoArray[5];

                // user factory ... trebuie verificat rolul, creat o clasa "UserFactory" in care dupa rolul citit, se va crea
                // un utilizator de tipul rol. Se va face intr-un switch
            }
        }
    }
}
