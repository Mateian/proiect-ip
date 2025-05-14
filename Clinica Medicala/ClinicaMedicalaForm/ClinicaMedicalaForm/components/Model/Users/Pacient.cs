using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaMedicalaForm.components.Model.Medical;

namespace ClinicaMedicalaForm.components.Model
{
    public class Pacient : IUser
    {
        private string _nume, _prenume, _email, _username, _parola, _gen;
        private int _id;

        private List<Programare> _programari;
        private DateTime _dataNastere;
        public int ID => _id;
        public string Rol => "Pacient";
        public string Username => _username;
        public string Parola => _parola;

        public string Nume => _nume;

        public string Prenume => _prenume;

        public Pacient(int ID, string username, string parola, string nume, string prenume)
        {
            this._id = ID;
            this._username = username;
            this._parola = parola;
            this._nume = nume;
            this._prenume = prenume;
        }
        override public string ToString()
        {
            return $"{ID} {_nume} {_prenume} ({_username}) {Rol} {_email} {_dataNastere}";
        }

    }
}
