using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FisaMedicalaForm
{
    public partial class FisaMedicalaForm : Form
    {
        public List<string> datePacient = new List<string>();
        public FisaMedicalaForm()
        {
            InitializeComponent();
            try
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = Image.FromFile("images\\logo.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonSemnare_Click(object sender, EventArgs e)
        {
            datePacient.Clear();
            datePacient.Add(textBoxNumePacient.Text);
            datePacient.Add(textBoxCNP.Text);
            datePacient.Add(textBoxDataNastere.Text);
            datePacient.Add(textBoxTelefon.Text);
            datePacient.Add(textBoxAdresa.Text);
            if (radioButtonBarbat.Checked) {
                datePacient.Add(radioButtonBarbat.Text);
            }
            if (radioButtonFemeie.Checked)
            {
                datePacient.Add(radioButtonFemeie.Text);
            }
            datePacient.Add(textBoxDataConsultatie.Text);
            datePacient.Add(textBoxNumeMedic.Text);
            datePacient.Add(textBoxExamenClinic.Text);
            datePacient.Add(textBoxInvestigatii.Text);
            datePacient.Add(textBoxDiagnostic.Text);
            datePacient.Add(textBoxTratament.Text);
            datePacient.Add(textBoxRecomandari.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBoxMotiv_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
