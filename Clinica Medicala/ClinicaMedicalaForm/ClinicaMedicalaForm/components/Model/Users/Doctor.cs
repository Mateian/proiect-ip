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

namespace ClinicaMedicalaForm.components.Model
{
    public class Doctor : IUser
    {
        private string _nume, _prenume, _email, _username, _parola;
        private int _id;
        private List<Programare> _programari;
        public int ID => _id;
        public string Rol => "Doctor";
        public string Specializare { get; set; }
        public string Prenume => _prenume;
        public string Nume => _nume;
        public string Username => _username;

        public string Parola => _parola;
        public Doctor(int id, string username, string parola, string nume, string prenume)
        {
            _id = id;
            _username = username;
            _parola = parola;
            _nume = nume;
            _prenume = prenume;
        }

        override public string ToString()
        {
            return $"{ID} {_nume} {_prenume} ({_username}) {Rol} {_email} {Specializare}";
        }

        public void SetProgramare(Programare programare)
        {
            if(_programari == null)
            {
                _programari = new List<Programare>();
            }
            _programari.Add(programare);
        }

        public Programare GetProgramare(int index)
        {
            if(_programari != null && index >= 0 && index < _programari.Count)
            {
                return _programari[index];
            }
            return null;
        }
    }
}
