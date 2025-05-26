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
    public class Programare
    {
        private int _pacientID, _doctorID, _id;
        private string _specializare, _data, _valabilitate;

        public int ID => _id;
        public int PacientID => _pacientID;
        public int DoctorID => _doctorID;
        public string Specializare => _specializare;
        public string Data => _data;
        public string Valabilitate => _valabilitate;
        public Programare(int ID, int pacientID, int doctorID, string date, string spec, string valabilitate)
        {
            this._id = ID;
            this._pacientID = pacientID;
            this._doctorID = doctorID;
            this._specializare = spec;
            this._data = date;
            this._valabilitate = valabilitate;
        }
        public override string ToString()
        {
            return ID + " " + Data.ToString() + ", Pacientul: " + PacientID + ", Specialitatea: " + Specializare + ", Valabilitatea: " + Valabilitate + "\n";
        }
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
