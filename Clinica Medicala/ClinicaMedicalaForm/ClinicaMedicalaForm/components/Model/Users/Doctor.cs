/**************************************************************************
 *                                                                        *
 *  File:        Doctor.cs                                                *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Clasa folosita pentru memorarea si gestionarea           *
 *               informatiilor unui utilizator de tip Doctor.             *
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
using ClinicaMedicalaForm.components.Model.Medical;
using ClinicaMedicalaForm.components.Model.Users;
using ClinicaMedicalaForm.components.Observer;

namespace ClinicaMedicalaForm.components.Model
{
    /// <summary>
    /// Clasa care defineste un utilizator de tip Doctor.
    /// </summary>
    public class Doctor : IUser
    {
        private string _nume, _prenume, _email, _username, _parola;
        private int _id;
        private List<Programare> _programari; // Lista programarilor asociate doctorului.
        private Observe _obs; // Observator folosit pentru notificari

        /// <summary>
        /// ID-ul doctorului.
        /// </summary>
        public int ID => _id;

        /// <summary>
        /// Rolul utilizatorului (Doctor).
        /// </summary>
        public string Rol => "Doctor";

        /// <summary>
        /// Specializarea doctorului.
        /// </summary>
        public string Specializare { get; set; }

        /// <summary>
        /// Prenumele doctorului.
        /// </summary>
        public string Prenume => _prenume;


        /// <summary>
        /// Numele doctorului.
        /// </summary>
        public string Nume => _nume;

        /// <summary>
        /// Username-ul contului doctorului.
        /// </summary>
        public string Username => _username;

        /// <summary>
        /// Parola contului doctorului.
        /// </summary>
        public string Parola => _parola;

        /// <summary>
        /// Constructorul clasei Doctor.
        /// </summary>
        /// <param name="id">ID-ul unic al utilizatorului.</param>
        /// <param name="username">Numele de utilizator.</param>
        /// <param name="parola">Parola asociata contului.</param>
        /// <param name="nume">Numele real al doctorului.</param>
        /// <param name="prenume">Prenumele real al doctorului.</param>
        /// <param name="obs">Referinta catre observator pentru notificari.</param>
        public Doctor(int id, string username, string parola, string nume, string prenume, Observe obs)
        {
            _id = id;
            _username = username;
            _parola = parola;
            _nume = nume;
            _prenume = prenume;

            this._obs = obs;
        }

        /// <summary>
        /// Returneaza o reprezentare textuala a doctorului.
        /// </summary>
        /// <returns>String cu detalii despre doctor.</returns>
        override public string ToString()
        {
            return $"{ID} {_nume} {_prenume} ({_username}) {Rol} {_email} {Specializare}";
        }

        /// <summary>
        /// Asociaza o programare cu doctorul.
        /// </summary>
        /// <param name="programare">Obiectul Programare ce urmeaza sa fie adaugat.</param>
        public void SetProgramare(Programare programare)
        {
            if(_programari == null)
            {
                _programari = new List<Programare>();
            }
            _programari.Add(programare);
            
            // Notifica observatorul ca doctorul a facut o programare
            _obs.Update("USERNAME " + Nume + " " + Prenume + "(ID=" + _id + ")" + ": Doctor made appointment - " + programare.ToString());
        }

        /// <summary>
        /// Returneaza o programare dupa index.
        /// </summary>
        /// <param name="index">Indexul dorit din lista programarilor.</param>
        /// <returns>Obiect de tip Programare sau null daca indexul nu este valid.</returns>
        public Programare GetProgramare(int index)
        {
            if(_programari != null && index >= 0 && index < _programari.Count)
            {
                return _programari[index];
            }
            return null;
        }

        /// <summary>
        /// Notifica observatorul cu un mesaj personalizat.
        /// </summary>
        /// <param name="s">Mesajul transmis.</param>
        public void NotifyObs(string s)
        {
            if (_obs != null)
                _obs.Update("USERNAME " + Nume + " " + Prenume + "(ID=" + _id + ")" + ": " + s);
        }
    }
}