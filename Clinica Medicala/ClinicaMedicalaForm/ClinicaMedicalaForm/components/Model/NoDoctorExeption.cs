using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaMedicalaForm
{
    public class NoDoctorExeption:Exception
    {
        public NoDoctorExeption(string message) : base(message) { }
    }
}
