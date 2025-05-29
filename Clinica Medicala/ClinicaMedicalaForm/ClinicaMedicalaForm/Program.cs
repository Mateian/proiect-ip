/**************************************************************************
 *                                                                        *
 *  File:        Program.cs                                               *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Clasa de baza a programului. Aici ruleaza programul      *
 *               cu toate componentele sale.                              *
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
using System.Windows.Forms;
using components.ModelClass;
using components.Model.Interfaces;
using ClinicaMedicalaForm.components.Presenter;
using components.Presenter.Interfaces;
using components.View.Interfaces;

namespace ClinicaMedicalaFormNamespace
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application..
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IModel model = new Model();
            IView view = new ClinicaMedicalaForm();
            IPresenter presenter = new Presenter(view, model);
            view.SetPresenter(presenter);
            ((ClinicaMedicalaForm)view).SetModel(model);
            Application.Run((ClinicaMedicalaForm)view);
        }
    }
}
