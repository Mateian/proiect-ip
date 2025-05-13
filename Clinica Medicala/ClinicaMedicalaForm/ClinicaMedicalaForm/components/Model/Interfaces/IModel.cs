using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaMedicalaForm.components.Model.Interfaces
{
    public interface IModel
    {
        List<IUser> CitireUtilizatori();
        bool VerificaAutentificare(string username, string parola);
    }
}
