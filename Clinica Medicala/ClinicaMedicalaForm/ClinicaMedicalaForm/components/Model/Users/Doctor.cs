using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaMedicalaForm.components.Model.Medical;
using ClinicaMedicalaForm.components.Model.Users;

namespace ClinicaMedicalaForm.components.Model
{
    public class Doctor : IUser
    {
        private string _nume, _prenume, _email, _username, _parola;
        private List<Programare> _programari;
        public int ID { get; }
        public string Rol => "Doctor";
        public string Specializare { get; set; }

        override public string ToString()
        {
            return $"{ID} {_nume} {_prenume} ({_username}) {Rol} {_email} {Specializare}";
        }
    }
}
