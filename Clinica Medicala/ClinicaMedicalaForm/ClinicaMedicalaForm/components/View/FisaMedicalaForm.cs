/**************************************************************************
 *                                                                        *
 *  File:        FisaMedicalaForm.cs                                      *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Formular pentru introducerea datelor pacientilor de      *
 *               catre doctori.                                           *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/


using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FisaMedicalaFormNamespace
{
    /// <summary>
    /// Clasa principala a formularului medical.
    /// Permite introducerea si salvarea datelor pacientului.
    /// </summary>
    public partial class FisaMedicalaForm : Form
    {
        /// <summary>
        /// Lista ce contine datele completate ale pacientului.
        /// </summary>
        public List<string> datePacient = new List<string>();

        /// <summary>
        /// Constructorul formularului. Initializeaza componentele UI si incarca logo-ul.
        /// </summary>
        public FisaMedicalaForm()
        {
            InitializeComponent();
            try
            {
                // Modul de afisare al imaginii ca Zoom
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                var path = Directory.GetCurrentDirectory() + "\\..\\..\\images\\logo.png";
                pictureBox1.Image = Image.FromFile(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        // Callback-uri
        private void buttonSemnare_Click(object sender, EventArgs e)
        {
            datePacient.Clear();
            
            // Adaugare campuri in lista
            datePacient.Add(textBoxNumePacient.Text);
            datePacient.Add(textBoxCNP.Text);
            datePacient.Add(textBoxDataNastere.Text);
            datePacient.Add(textBoxTelefon.Text);
            datePacient.Add(textBoxAdresa.Text);
            
            // Gen pacient
            if (radioButtonBarbat.Checked)
            {
                datePacient.Add(radioButtonBarbat.Text);
            }
            if (radioButtonFemeie.Checked)
            {
                datePacient.Add(radioButtonFemeie.Text);
            }
            
            // Date medicale
            datePacient.Add(textBoxDataConsultatie.Text);
            datePacient.Add(textBoxNumeMedic.Text);
            datePacient.Add(textBoxExamenClinic.Text);
            datePacient.Add(textBoxInvestigatii.Text);
            datePacient.Add(textBoxDiagnostic.Text);
            datePacient.Add(textBoxTratament.Text);
            datePacient.Add(textBoxRecomandari.Text);
            
            // Motiv
            if (comboBoxMotiv.TabIndex == 0)
            {
                datePacient.Add(textBoxMotiv.Text);
            }
            else
            {
                datePacient.Add(comboBoxMotiv.Text);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void comboBoxMotiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxMotiv.SelectedIndex == 0)
            {
                textBoxMotiv.Enabled = true;
            }
            else
            {
                textBoxMotiv.Enabled = false;
            }
        }
    }
}
