using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaMedicalaForm.components.Model;

namespace ClinicaMedicalaForm.components.Observer
{
    public class Observe
    {
        private List<string> updates;
        public Observe()
        {
            updates = new List<string>();
        }
        public void Update(string message)
        {
            if(updates!=null)
                updates.Add(message);
        }
        public List<string> GetUpdates() { return updates; }

    }
}
