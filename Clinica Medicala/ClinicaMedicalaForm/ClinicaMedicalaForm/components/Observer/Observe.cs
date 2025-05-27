/**************************************************************************
 *                                                                        *
 *  File:        Observe.cs                                               *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Clasa Observer pentru fiecare utilizator. Trimite        *
 *               notificari si le adauga in administrator in lista        *
 *               de comenzi.                                              *
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
using ClinicaMedicalaForm.components.Model;

namespace ClinicaMedicalaForm.components.Observer
{
    public class Observe
    {
        private List<string> _updates;
        public Observe()
        {
            _updates = new List<string>();
        }
        public void Update(string message)
        {
            if(_updates!=null)
                _updates.Add(message);
        }
        public List<string> GetUpdates() { return _updates; }
    }
}
