/**************************************************************************
 *                                                                        *
 *  File:        Programare.cs                                            *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Clasa folosita pentru memorarea informatiilor unei       *
 *               programari.                                              *
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
    /// Clasa care reprezinta o programare medicala si contine informatii
    /// despre pacient, doctor, specializare si data programarii.
    /// </summary>
    public class Programare
    {
        // Campuri private pentru stocarea datelor programarii
        private int _pacientID, _doctorID, _id;
        private string _specializare, _data, _valabilitate;

        /// <summary>
        /// ID-ul unic al programarii.
        /// </summary>
        public int ID => _id;

        /// <summary>
        /// ID-ul pacientului programat.
        /// </summary>
        public int PacientID => _pacientID;

        /// <summary>
        /// ID-ul doctorului la care s-a facut programarea.
        /// </summary>
        public int DoctorID => _doctorID;

        /// <summary>
        /// Specializarea pentru care a fost facuta programarea.
        /// </summary>
        public string Specializare => _specializare;

        /// <summary>
        /// Data programarii.
        /// </summary>
        public string Data => _data;

        /// <summary>
        /// Perioada de valabilitate a programarii.
        /// </summary>
        public string Valabilitate => _valabilitate;
       
        /// <summary>
        /// Constructor pentru initializarea completa a unei programari.
        /// </summary>
        /// <param name="ID">ID-ul programarii.</param>
        /// <param name="pacientID">ID-ul pacientului.</param>
        /// <param name="doctorID">ID-ul doctorului.</param>
        /// <param name="date">Data programarii</param>
        /// <param name="spec">Specializarea medicala.</param>
        /// <param name="valabilitate">Valabilitatea programarii.</param>
        public Programare(int ID, int pacientID, int doctorID, string date, string spec, string valabilitate)
        {
            this._id = ID;
            this._pacientID = pacientID;
            this._doctorID = doctorID;
            this._specializare = spec;
            this._data = date;
            this._valabilitate = valabilitate;
        }

        /// <summary>
        /// Returneaza un rezumat al programarii sub forma de string.
        /// </summary>
        /// <returns>String formatat cu datele esentiale ale programarii.</returns>
        public override string ToString()
        {
            return ID + " " + Data.ToString() + ", Pacientul: " + PacientID + ", Specialitatea: " + Specializare + ", Valabilitatea: " + Valabilitate + "\n";
        }

        /// <summary>
        /// Genereaza un preview detaliat al programarii.
        /// </summary>
        /// <returns>Text formatat cu detalii despre data, specializare si valabilitatea programarii.</returns>
        public string GeneratePreview()
        {
            //Generare preview pentru programare
            string text = "Data programarii: " + _data + "\r\n" +
                          "Specializarea: " + _specializare + "\r\n" +
                          "Valabilitatea: " + _valabilitate + "\r\n\r\n";
            return text;
        }
    }
}
