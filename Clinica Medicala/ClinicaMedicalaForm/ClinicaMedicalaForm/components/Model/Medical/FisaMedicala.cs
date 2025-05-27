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
    /// <summary>
    /// CLasa care contine datele unei fise medicale.
    /// </summary>
    public class FisaMedicala
    {
        // Campuri private care stocheaza datele fisei medicale
        private string _nume, _prenume, _dataConsultatie, _motiv, _diagnostic,_tratament,_numeMedic,_examenClinic,_recomandari,_investigatiiRecomandate;
        
        /// <summary>
        /// Constructor pentru initializare completa a unei fise medicale.
        /// </summary>
        /// <param name="nume">Numele pacientului.</param>
        /// <param name="prenume">Prenumele pacientului.</param>
        /// <param name="data">Data consultatiei.</param>
        /// <param name="motiv">Motivul consultatiei.</param>
        /// <param name="diagnostic">Diagnostic prezumtiv sau final.</param>
        /// <param name="tratament">Tratamentul prescris.</param>
        /// <param name="numeMedic">Numele medicului curant.</param>
        /// <param name="examenClinic">Rezultatul examenului clinic.</param>
        /// <param name="recomandari">Recomandari suplimentare.</param>
        /// <param name="investigatii">Investigatii recomandate.</param>
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

        /// <summary>
        /// Suprascrie metoda ToString pentru a returna un rezumat simplu al fisei medicale
        /// </summary>
        /// <returns>Rezumat cu data consultatiei, motivul, diagnosticul si tratamentul.</returns>
        public override string ToString()
        {
            return _dataConsultatie + " " + _motiv + " " + _diagnostic + " " + _tratament;
        }

        /// <summary>
        /// Genereaza un text de previzualizare cu toate informatiile relevante ale fisei medicale.
        /// </summary>
        /// <returns>String formatat cu detaliile fisei medicale.</returns>
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
