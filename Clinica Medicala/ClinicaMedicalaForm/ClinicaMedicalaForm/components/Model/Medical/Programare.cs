using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaMedicalaForm.components.Model.Medical
{
    public class Programare
    {
        private int _pacientID, _doctorID;
        private string _specializare, _data, _valabilitate;

        public int PacientID => _pacientID;
        public int DoctorID => _doctorID;
        public string Specializare => _specializare;
        public string Data => _data;
        public string Valabilitate => _valabilitate;
        public Programare(int pacientID, int doctorID, string date, string spec, string valabilitate)
        {
            this._pacientID = pacientID;
            this._doctorID = doctorID;
            this._specializare = spec;
            this._data = date;
            this._valabilitate = valabilitate;
        }
        public override string ToString()
        {
            return Data.ToString() + ", Pacientul: " + PacientID + ", Specialitatea: " + Specializare + ", Valabilitatea: " + Valabilitate  + "\n";
        }
    }
}
