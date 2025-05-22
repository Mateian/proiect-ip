/**************************************************************************
 *                                                                        *
 *  File:        FisaMedicala.cs                                          *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Clasa folosita pentru memorarea informatiilor unei       *
 *               fise medicale.                                           *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

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
