using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaMedicalaForm.components.Model;
using ClinicaMedicalaForm.components.Model.Interfaces;

namespace ClinicaMedicalaForm.components.Presenter.Interfaces
{
    public interface IPresenter
    {
        IUser VerificaAutentificare(string username, string parola);
    }
}
