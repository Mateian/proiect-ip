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
using ClinicaMedicalaForm.components.Model.Medical;

namespace ClinicaMedicalaForm.components.Model
{
    public class Pacient : IUser
    {
        private string _nume, _prenume, _email, _username, _parola, _gen;
        private int _id;

        private List<Programare> _programari;
        private DateTime _dataNastere;
        public int ID => _id;
        public Doctor Doctor {  get; set; }
        public string Rol => "Pacient";
        public string Username => _username;
        public string Parola => _parola;

        public string Nume => _nume;

        public string Prenume => _prenume;

        public Pacient(int ID, string username, string parola, string nume, string prenume)
        {
            this._id = ID;
            this._username = username;
            this._parola = parola;
            this._nume = nume;
            this._prenume = prenume;
        }
        override public string ToString()
        {
            return $"{ID} {_nume} {_prenume} ({_username}) {Rol} {_email} {_dataNastere}";
        }
        public void SetProgramare(Programare programare)
        {
            if (_programari == null)
                _programari = new List<Programare>();
            _programari.Add(programare);
        }
        public Programare GetProgramare(int index)
        {
            try
            {
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
    }
}
