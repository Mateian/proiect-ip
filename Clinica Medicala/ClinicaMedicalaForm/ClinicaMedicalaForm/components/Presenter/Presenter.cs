using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaMedicalaForm.components.Model.Interfaces;
using ClinicaMedicalaForm.components.Presenter.Interfaces;
using ClinicaMedicalaForm.components.View.Interfaces;

namespace ClinicaMedicalaForm.components.Presenter
{
    public class Presenter : IPresenter
    {
        private IView view;
        private IModel model;

        public Presenter(IView view, IModel model)
        {
            this.view = view;
            this.model = model;
        }
    }
}
