using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaMedicalaForm.components.Model.Medical;

namespace ClinicaMedicalaForm.components.Model
{
    public class Asistent : IUser
    {
        private string _nume, _prenume, _email, _username, _parola;
        private int _id;
        public int ID { get; }
        public string Rol => "Asistent";
        public string Specializare { get; set; }
        public string Prenume => _prenume;
        public string Nume => _nume;
        public Doctor Doctor { get; set; }
        public string Username => _username;
        public string Parola => _parola;

        public Asistent(int id, string username, string parola, string nume, string prenume)
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
