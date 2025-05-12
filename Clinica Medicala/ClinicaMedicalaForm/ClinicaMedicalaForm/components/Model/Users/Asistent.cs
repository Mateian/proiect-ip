using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaMedicalaForm.components.Model.Medical;

namespace ClinicaMedicalaForm.components.Model
{
    public class Asistent
    {
        private string _nume, _prenume, _email, _username, _parola;
        public int ID { get; }
        public string Rol => "Asistent";
        public string Specializare { get; set; }
        public Doctor Doctor { get; set; }

        override public string ToString()
        {
            return $"{ID} {_nume} {_prenume} ({_username}) {Rol} {_email} {Specializare}";
        }
    }
}
