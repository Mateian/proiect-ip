using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaMedicalaForm.components.Model.Users
{
    public class Administrator : IUser
    {
        private string _nume, _prenume, _email, _username, _parola;
        public string Rol => "Administrator";
        public int ID { get; }

        override public string ToString()
        {
            return $"{ID} {_nume} {_prenume} {_email}";
        }
    }
}
