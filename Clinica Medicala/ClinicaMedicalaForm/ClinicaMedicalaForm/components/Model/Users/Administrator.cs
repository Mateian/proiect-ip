using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ClinicaMedicalaForm.components.Model.Users
{
    public class Administrator : IUser
    {
        private string _nume, _prenume, _email, _username, _parola;
        private int _id;
        public string Prenume => _prenume;
        public string Nume => _nume;
        public string Rol => "Administrator";
        public int ID => _id;
        public string Username => _username;
        public string Parola => _parola;

        public Administrator(int ID, string username, string parola, string nume, string prenume)
        {
            this._id = ID;
            this._username = username;
            this._parola = parola;
            this._nume = nume;
            this._prenume = prenume;
        }
        override public string ToString()
        {
            return $"{ID} {_nume} {_prenume} {_email}";
        }
    }
}
