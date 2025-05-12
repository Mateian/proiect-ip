using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FisaMedicalaForm.FisaMedicalaForm;

namespace ClinicaMedicalaForm
{
    public partial class ClinicaMedicalaForm : Form
    {
        public ClinicaMedicalaForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonCreareFisaMedicala_Click(object sender, EventArgs e)
        {
            FisaMedicalaForm.FisaMedicalaForm fisaMedicala = new FisaMedicalaForm.FisaMedicalaForm();
            if (fisaMedicala.ShowDialog() == DialogResult.OK)
            {
                List<string> dateFisaMedicala = fisaMedicala.datePacient;
                string date = "";
                foreach(string data in  dateFisaMedicala)
                {
                    date += data + " ";
                }
                // trebuie facut ceva cu datele astea
                MessageBox.Show(date);
            }
        }

        private void buttonAutentificare_Click(object sender, EventArgs e)
        {
            StreamReader userFile = new StreamReader(Path.Combine("..\\..\\data\\users.txt"));
            string user = userFile.ReadToEnd();
            string[] usersArray = user.Split('\n');
            string aux;
            List<string> date = new List<string>();
            foreach(string data in usersArray)
            {
                aux = data.Replace("\r", "");
                date.Add(aux);
            }
            string username = textBoxNumeUtilizator.Text;
            string parola = textBoxParola.Text;
            foreach(string data in date)
            {
                if(data.Split(' ')[0] ==  username && data.Split(' ')[1] == parola)
                {
                    labelWelcomeText.Text = "Bine ai venit, " + username + "!";
                    groupBoxAdministrator.Visible = true;
                    tabControlUser.Visible = true;
                    labelWelcomeText.Visible = true;
                    break;
                }
            }
        }

        private void buttonDeconectare_Click(object sender, EventArgs e)
        {
            groupBoxAdministrator.Visible = false;
            labelWelcomeText.Visible = false;
            tabControlUser.Visible = false;
        }
    }
}
