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
using ClinicaMedicalaForm.components.Model;
using ClinicaMedicalaForm.components.Model.Interfaces;
using ClinicaMedicalaForm.components.Presenter;
using ClinicaMedicalaForm.components.Presenter.Interfaces;
using ClinicaMedicalaForm.components.View.Interfaces;
using FisaMedicalaForm;
using static FisaMedicalaForm.FisaMedicalaForm;

namespace ClinicaMedicalaForm
{
    public partial class ClinicaMedicalaForm : Form, IView
    {
        private IPresenter _presenter;
        private IModel _model;
        private int user;
        public ClinicaMedicalaForm()
        {
            InitializeComponent();
            InitForm();
        }

        public void InitForm()
        {
            // in caz de e nevoie de facut ceva cand se creeaza Form
            textBoxParola.UseSystemPasswordChar = true;
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
            string username = textBoxNumeUtilizator.Text;
            string parola = textBoxParola.Text;
            user = _presenter.VerificaAutentificare(username, parola);
            if (user != -100)//val cat mai mica sa nu incurce cu nimica
            {
                if (user == 0)//user
                {
                    tabControlUser.Visible = true;
                    loadPrograms(_model.GetProgramariIstoric());
                    loadIstoric(_model.GetIstoric());
                    loadProgramari(_model.GetCurrentProgramari());
                    tabPagePacient.Visible = true;
                    tabPageDoctor.Visible = false;
                }
                else if(user == 1)
                {
                    tabControlUser.Visible = true;
                }
                else if(user == -1)
                {
                    tabControlUser.Visible = true;
                    groupBoxAdministrator.Visible = true;
                }
            }
            else
            {
                tabControlUser.Visible = false;
                tabPagePacient.Visible = false;
                tabPageDoctor.Visible = false;
                groupBoxAdministrator.Visible = false;
                MessageBox.Show("No user that matches these credentials found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void loadPrograms(List<string> programs)
        {
            if (programs != null)
            {
                listBoxPacientProgramari.Items.Clear();
                listBoxPacientProgramari.DataSource = programs;
            }
        }

        private void loadIstoric(List<Form> istoric)
        {
            if (istoric != null)
            {
                foreach (Form form in istoric)
                {
                    listBoxIstoricMedical.Items.Add(form);
                }
            }
        }
        private void loadProgramari(List<string> programs)
        {
            if (programs != null)
            {
                listBoxProgramari.Items.Clear();
                listBoxProgramari.DataSource = programs;
            }
        }

        private void buttonDeconectare_Click(object sender, EventArgs e)
        {
            groupBoxAdministrator.Visible = false;
            labelWelcomeText.Visible = false;
            tabControlUser.Visible = false;
            textBoxNumeUtilizator.Clear();
            textBoxParola.Clear();
            user = -100;
        }
        
        public void SetModel(IModel model)
        {
            _model = model;
        }

        public void SetPresenter(IPresenter presenter)
        {
            _presenter = presenter;
        }

        private void tabControlUser_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (user == 0 && e.TabPage.Name=="tabPageDoctor")
            {
                e.Cancel = true;
            }
            if (user == 1 && e.TabPage.Name == "tabPagePacient")
            {
                e.Cancel = true;
            }
        }

        private void listBoxIstoricMedical_Click(object sender, EventArgs e)
        {
            if(listBoxIstoricMedical.SelectedItem!=null)
            {
                Form f;
                f=(FisaMedicalaForm.FisaMedicalaForm)listBoxIstoricMedical.SelectedItem;
                f.Show();
            }
        }

        private void buttonProgramare_Click(object sender, EventArgs e)
        {

        }
    }
}
