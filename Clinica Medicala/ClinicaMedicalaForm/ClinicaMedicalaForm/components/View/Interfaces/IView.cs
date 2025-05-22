/**************************************************************************
 *                                                                        *
 *  File:        IView.cs                                                 *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Interfata folosita pentru definirea unui obiect de       *
 *               tip View.                                                *
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
using ClinicaMedicalaForm.components.Model.Interfaces;
using ClinicaMedicalaForm.components.Presenter.Interfaces;

namespace ClinicaMedicalaForm.components.View.Interfaces
{
    public interface IView
    {
        void SetModel(IModel model);
        void SetPresenter(IPresenter presenter);
    }
}
