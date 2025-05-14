using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaMedicalaForm.components.Model.Medical
{
    public class Programare
    {
        private int pacientID,doctorID;
        private bool executata;
        private string specializare;
        private string data;

        public int PacientID => pacientID;
        public int DoctorID => doctorID;
        public bool EsteInTrecut => executata;
        public string Specializare => specializare;
        public string Data => data;
        public Programare(int pacientID,int doctorID,bool exec,string spec,string date)
        {
            this.pacientID = pacientID;
            this.doctorID = doctorID;
            this.executata = exec;
            this.specializare = spec;
            this.data = date;
        }
    }
}
