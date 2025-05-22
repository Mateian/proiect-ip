using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaMedicalaForm.components.Model.Medical
{
    public class FisaMedicala
    {
        private string _nume, _prenume, _dataConsultatie, _motiv, _diagnostic,_tratament,_numeMedic,_examenClinic,_recomandari,_investigatiiRecomandate;
        public FisaMedicala(string nume,string prenume,string data,string motiv,string diagnostic,string tratament,string numeMedic,string examenClinic, string recomandari,string investigatii ) 
        { 
            _nume = nume;
            _prenume = prenume;
            _dataConsultatie = data;
            _motiv = motiv;
            _diagnostic = diagnostic;
            _tratament = tratament;
            _numeMedic = numeMedic;
            _examenClinic = examenClinic;
            _recomandari = recomandari;
            _investigatiiRecomandate = investigatii;
        }
        public override string ToString()
        {
            return _dataConsultatie + " " + _motiv + " " + _diagnostic + " " + _tratament;
        }
        public string GeneratePreview()
        {
            //Generare preview pentru fisa medicala
            string text = "Data consultatiei: " + _dataConsultatie + "\r\n" +
                          "Motivul consultatiei: " + _motiv + "\r\n\r\n" +
                          "Diagnostic: " + _diagnostic + "\r\n" +
                          "Tratament: " + _tratament + "\r\n\r\n" +
                          "Nume medic: " + _numeMedic + "\r\n" +
                          "Examen clinic: " + _examenClinic + "\r\n" +
                          "Recomandari: " + _recomandari + "\r\n" +
                          "Investigatii recomandate: " + _investigatiiRecomandate + "\r\n\r\n";
            return text;
        }
    }
}
