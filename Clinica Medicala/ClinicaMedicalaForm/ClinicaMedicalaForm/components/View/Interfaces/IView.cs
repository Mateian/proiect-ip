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
