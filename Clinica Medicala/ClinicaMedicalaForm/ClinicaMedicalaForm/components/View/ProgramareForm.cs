/**************************************************************************
 *                                                                        *
 *  File:        ProgramareForm.cs                                        *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Formular folosit pentru a adauga o programare de catre   *
 *               client.                                                  *
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
using System.Windows.Forms;
using components.Model.Exceptions;

namespace ClinicaMedicalaFormNamespace
{
    /// <summary>
    /// Formular folosit pentru crearea unei programari la un doctor specificat.
    /// Clientul introduce data si specializarea pentru programare.
    /// </summary>
    public partial class ProgramareForm : Form
    {
        // Elemente create de VS
        private TextBox textBoxData;
        private TextBox textBoxSpecializare;
        private Button buttonProgramare;
        private Label label1;
        private Label label2;
        private Label labelDoctor;

        private string _doctorNume, _doctorPrenume;

        /// <summary>
        /// Data programarii introduse de utilizator.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Specializarea asociata programarii.
        /// </summary>
        public string Specializare { get; set; }

        /// <summary>
        /// Constructorul formularului primeste numele si prenumele doctorului.
        /// Arunca o exceptie daca acestea sunt nule.
        /// </summary>
        /// <param name="doctorNume">Numele doctorului</param>
        /// <param name="doctorPrenume">Prenumele doctorului</param>
        public ProgramareForm(string doctorNume, string doctorPrenume)
        {
            if(doctorNume == null || doctorPrenume == null)
            {
                throw new MasterExceptionHandler("Doctorul nu este disponibil!",402,null);
            }
            _doctorNume = doctorNume;
            _doctorPrenume = doctorPrenume;
            InitializeComponent();
            labelDoctor.Text += _doctorNume + " " + _doctorPrenume;
        }

        private void InitializeComponent()
        {
            this.labelDoctor = new System.Windows.Forms.Label();
            this.textBoxData = new System.Windows.Forms.TextBox();
            this.textBoxSpecializare = new System.Windows.Forms.TextBox();
            this.buttonProgramare = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelDoctor
            // 
            this.labelDoctor.AutoSize = true;
            this.labelDoctor.Location = new System.Drawing.Point(12, 46);
            this.labelDoctor.Name = "labelDoctor";
            this.labelDoctor.Size = new System.Drawing.Size(125, 16);
            this.labelDoctor.TabIndex = 0;
            this.labelDoctor.Text = "Sunteți Înscris la Dr. ";
            // 
            // textBoxData
            // 
            this.textBoxData.Location = new System.Drawing.Point(148, 154);
            this.textBoxData.Name = "textBoxData";
            this.textBoxData.Size = new System.Drawing.Size(100, 22);
            this.textBoxData.TabIndex = 1;
            // 
            // textBoxSpecializare
            // 
            this.textBoxSpecializare.Location = new System.Drawing.Point(148, 226);
            this.textBoxSpecializare.Name = "textBoxSpecializare";
            this.textBoxSpecializare.Size = new System.Drawing.Size(100, 22);
            this.textBoxSpecializare.TabIndex = 2;
            // 
            // buttonProgramare
            // 
            this.buttonProgramare.Location = new System.Drawing.Point(148, 281);
            this.buttonProgramare.Name = "buttonProgramare";
            this.buttonProgramare.Size = new System.Drawing.Size(100, 28);
            this.buttonProgramare.TabIndex = 3;
            this.buttonProgramare.Text = "Programare";
            this.buttonProgramare.UseVisualStyleBackColor = true;
            this.buttonProgramare.Click += new System.EventHandler(this.buttonProgramare_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(145, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(145, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Specializare";
            // 
            // ProgramareForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(468, 490);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonProgramare);
            this.Controls.Add(this.textBoxSpecializare);
            this.Controls.Add(this.textBoxData);
            this.Controls.Add(this.labelDoctor);
            this.Name = "ProgramareForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // Callback-uri
        private void buttonProgramare_Click(object sender, EventArgs e)
        {
            Data = textBoxData.Text;
            Specializare = textBoxSpecializare.Text;
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
