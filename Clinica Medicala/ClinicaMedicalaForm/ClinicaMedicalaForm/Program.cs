using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClinicaMedicalaForm.components.Model;
using ClinicaMedicalaForm.components.Model.Interfaces;
using ClinicaMedicalaForm.components.Presenter;
using ClinicaMedicalaForm.components.Presenter.Interfaces;
using ClinicaMedicalaForm.components.View.Interfaces;

namespace ClinicaMedicalaForm
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
