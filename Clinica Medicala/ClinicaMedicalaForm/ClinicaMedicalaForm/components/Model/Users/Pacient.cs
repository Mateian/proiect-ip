/**************************************************************************
 *                                                                        *
 *  File:        Pacient.cs                                               *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Clasa folosita pentru memorarea si gestionarea           *
 *               informatiilor unui utilizator de tip Pacient.            *
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
using ClinicaMedicalaForm.components.Model.Exceptions;
using components.Model.Medical;
using Observer;

namespace ClinicaMedicalaForm.components.Model
{
    /// <summary>
    /// Clasa ce defineste un utilizator de tip Pacient.
    /// </summary>
    public class Pacient : IUser
    {
        private string _nume, _prenume, _email, _username, _parola, _gen;
        private int _id;
        private Observe _obs; // Observator pentru notificari
        private List<Programare> _programari; // Lista programarilor pacientului
        private DateTime _dataNastere;

        /// <summary>
        /// ID-ul pacientului.
        /// </summary>
        public int ID => _id;

        /// <summary>
        /// Rolul utilizatorului (Pacient).
        /// </summary>
        public string Rol => "Pacient";

        /// <summary>
        /// Numele de utilizator.
        /// </summary>
        public string Username => _username;

        /// <summary>
        /// Parola contului.
        /// </summary>
        public string Parola => _parola;


        /// <summary>
        /// Numele pacientului.
        /// </summary>
        public string Nume => _nume;

        /// <summary>
        /// Prenumele pacientului.
        /// </summary>
        public string Prenume => _prenume;

        /// <summary>
        /// Referinta catre doctorul asociat pacientului (optional).
        /// </summary>
        public Doctor Doctor { get; set; }

        /// <summary>
        /// Constructorul clasei Pacient.
        /// </summary>
        /// <param name="ID">ID-ul pacientului.</param>
        /// <param name="username">Username-ul contului.</param>
        /// <param name="parola">Parola contului.</param>
        /// <param name="nume">Numele real.</param>
        /// <param name="prenume">Prenumele real.</param>
        /// <param name="obs">Observator pentru notificari.</param>
        public Pacient(int ID, string username, string parola, string nume, string prenume, Observe obs)
        {
            this._id = ID;
            this._username = username;
            this._parola = parola;
            this._nume = nume;
            this._prenume = prenume;

            this._obs = obs;
        }

        /// <summary>
        /// Returneaza o reprezentare textuala a pacientului.
        /// </summary>
        /// <returns>String cu datele pacientului.</returns>
        override public string ToString()
        {
            return $"{ID} {_nume} {_prenume} ({_username}) {Rol} {_email} {_dataNastere}";
        }


        /// <summary>
        /// Asociaza o programare pacientului.
        /// </summary>
        /// <param name="programare">Obiectul Programare de adaugat.</param>
        public void SetProgramare(Programare programare)
        {
            if (_programari == null)
                _programari = new List<Programare>();
            _programari.Add(programare);
        }

        /// <summary>
        /// Returneaza o programare dupa indexul specificat.
        /// </summary>
        /// <param name="index">Indexul programarii in lista.</param>
        /// <returns>Programarea daca exista, altfel null.</returns>
        public Programare GetProgramare(int index)
        {
            try
            {
                _programari = null;
                if (index >= 0 && index < _programari.Count)
                {
                    return _programari[index];
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new MasterExceptionHandler("Array object null", 200, ex);
            }
        }

        /// <summary>
        /// Creeaza si adauga o noua programare, notificand observatorul.
        /// </summary>
        /// <param name="programare">Obiectul Programare de adaugat.</param>
        public void CreeazaProgramare(Programare programare)
        {
            if (_programari == null)
                _programari = new List<Programare>();
            _programari.Add(programare);

            // Trimite notificare catre observator
            _obs.Update("USERNAME "+Nume+" "+Prenume+"(ID=" + _id +")"+ ": Pacient made appointment - " + programare.ToString());
        }

        /// <summary>
        /// Notifica observatorul cu un mesaj personalizat.
        /// </summary>
        /// <param name="s">Mesajul transmis observatorului.</param>
        public void NotifyObs(string s)
        {
            if(_obs!=null)
                _obs.Update("USERNAME " + Nume + " " + Prenume + "(ID=" + _id + ")" + ": " + s);
        }
    }
}
