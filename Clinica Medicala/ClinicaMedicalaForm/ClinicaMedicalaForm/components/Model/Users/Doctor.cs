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
        private int _id;
        private List<Programare> _programari;
        public int ID => _id;
        public string Rol => "Doctor";
        public string Specializare { get; set; }

        public string Username => _username;

        public string Parola => _parola;
        public Doctor(int id, string username, string parola, string nume, string prenume)
        {
            _id = id;
            _username = username;
            _parola = parola;
            _nume = nume;
            _prenume = prenume;
        }

        override public string ToString()
        {
            return $"{ID} {_nume} {_prenume} ({_username}) {Rol} {_email} {Specializare}";
        }
    }
}
