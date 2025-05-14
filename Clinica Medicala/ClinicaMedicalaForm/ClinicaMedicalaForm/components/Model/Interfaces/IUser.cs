using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaMedicalaForm.components.Model
{
    public interface IUser
    {
        int ID { get; }
        string Username { get; }
        string Nume { get; }
        string Prenume { get; }
        string Rol { get; }
        string Parola { get; }
        string ToString();
    }
}
