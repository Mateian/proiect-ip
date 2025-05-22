using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaMedicalaForm.components.Model.Exceptions
{
    public class MasterExceptionHandler : Exception
    {
        public MasterExceptionHandler() { }
        public MasterExceptionHandler(string message, int err_code, Exception inner) 
        {

            switch (err_code)
            {
                case 100:
                case 101:
                case 102:
                    SQLExceptionHandle(message, inner);
                    break;
                case 200:
                case 201:
                    ObjectNullHandle(message, inner);
                    break;
                case 300:
                    UserIDHandle(message, inner);
                    break;
            }
        }
        private void UserIDHandle(string message, Exception inner)
        {

        }
        private void ObjectNullHandle(string message, Exception inner)
        {

        }
        private void SQLExceptionHandle(string message, Exception inner)
        {

        }
    }
}
