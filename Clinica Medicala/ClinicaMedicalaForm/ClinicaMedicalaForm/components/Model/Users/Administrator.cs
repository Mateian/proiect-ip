/**************************************************************************
 *                                                                        *
 *  File:        Administrator.cs                                         *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Clasa folosita pentru memorarea si gestionarea           *
 *               informatiilor unui utilizator de tip Administrator.      *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using ClinicaMedicalaForm.components.Model.Exceptions;
using ClinicaMedicalaForm.components.Model.Medical;
using ClinicaMedicalaForm.components.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ClinicaMedicalaForm.components.Model.Users
{
    public class Administrator : IUser
    {
        private string _nume, _prenume, _email, _username, _parola;
        private int _id;

        private Observe _obs;
        public string Prenume => _prenume;
        public string Nume => _nume;
        public string Rol => "Administrator";
        public int ID => _id;
        public string Username => _username;
        public string Parola => _parola;

        public Administrator(int ID, string username, string parola, string nume, string prenume,Observe obs)
        {
            this._id = ID;
            this._username = username;
            this._parola = parola;
            this._nume = nume;
            this._prenume = prenume;

            this._obs = obs;
        }
        override public string ToString()
        {
            return $"{ID} {_nume} {_prenume} {_email}";
        }

        public void SetProgramare(Programare programare)
        {
            new MasterExceptionHandler("Administratorul nu poate avea programari!", 400, null);
        }

        public Programare GetProgramare(int index)
        {
            new MasterExceptionHandler("Administratorul nu poate avea programari!", 400, null);
            return new Programare(-1, -1, -1, "", "", "");
        }
        public void NotifyObs(string s)
        {
            if (_obs != null)
                _obs.Update("USERNAME " + Nume + " " + Prenume + "(ID=" + _id + ")" + ": " + s);
        }
    }
}
