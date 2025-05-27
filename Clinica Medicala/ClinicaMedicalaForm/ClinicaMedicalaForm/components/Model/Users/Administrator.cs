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
using components.Model.Medical;
using Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ClinicaMedicalaForm.components.Model.Users
{
    /// <summary>
    /// Clasa care defineste un utilizator de tip Administrator.
    /// </summary>
    public class Administrator : IUser
    {
        private string _nume, _prenume, _email, _username, _parola;
        private int _id;

        private Observe _obs;

        /// <summary>
        /// Prenumele administratorului
        /// </summary>
        public string Prenume => _prenume;

        /// <summary>
        /// Numele administratorului.
        /// </summary>
        public string Nume => _nume;

        /// <summary>
        /// Rolul utilizatorului (Administrator).
        /// </summary>
        public string Rol => "Administrator";

        /// <summary>
        /// ID-ul administratorului.
        /// </summary>
        public int ID => _id;

        /// <summary>
        /// Numele de utilizator.
        /// </summary>
        public string Username => _username;

        /// <summary>
        /// Parola asociata contului.
        /// </summary>
        public string Parola => _parola;

        /// <summary>
        /// Constructor pentru initializarea unui administrator.
        /// </summary>
        /// <param name="ID">ID administrator.</param>
        /// <param name="username">Nume utilizator administrator.</param>
        /// <param name="parola">Parola administrator.</param>
        /// <param name="nume">Nume administrator.</param>
        /// <param name="prenume">Prenume administrator.</param>
        /// <param name="obs">Referinta catre un observer pentru notificari.</param>
        public Administrator(int ID, string username, string parola, string nume, string prenume,Observe obs)
        {
            this._id = ID;
            this._username = username;
            this._parola = parola;
            this._nume = nume;
            this._prenume = prenume;

            this._obs = obs;
        }

        /// <summary>
        /// Returneaza o reprezentare sub forma de string a obiectului Administrator.
        /// </summary>
        /// <returns>String cu ID-ul, numele si prenumele.</returns>
        override public string ToString()
        {
            return $"{ID} {_nume} {_prenume} {_email}";
        }


        /// <summary>
        /// Metoda nepermisa pentru administratori - acestia nu pot avea programari.
        /// </summary>
        /// <param name="programare">Obiect de tip Programare (ignorata).</param>
        public void SetProgramare(Programare programare)
        {
            // Arunca exceptie pentru ca administratorul nu poate avea programari
            new MasterExceptionHandler("Administratorul nu poate avea programari!", 400, null);
        }

        /// <summary>
        /// Metoda nepermisa pentru administratori - acestia nu au programari.
        /// </summary>
        /// <param name="index">Indexul programarii (ignorata).</param>
        /// <returns>Programare goala de tip fallback.</returns>
        public Programare GetProgramare(int index)
        {
            new MasterExceptionHandler("Administratorul nu poate avea programari!", 400, null);
            return new Programare(-1, -1, -1, "", "", "");
        }

        /// <summary>
        /// Trimite un mesaj de notificare catre obervator, daca este disponibil.
        /// </summary>
        /// <param name="s"></param>
        public void NotifyObs(string s)
        {
            if (_obs != null)
                _obs.Update("USERNAME " + Nume + " " + Prenume + "(ID=" + _id + ")" + ": " + s);
        }
    }
}
