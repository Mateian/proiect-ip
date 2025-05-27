/**************************************************************************
 *                                                                        *
 *  File:        DoctorForm.cs                                            *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Formular folosit pentru a adauga un doctor.              *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using ClinicaMedicalaForm.components.Model;
using Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaMedicalaForm.components.View
{
    /// <summary>
    /// Formular pentru adaugarea unui doctor nou in aplicatie.
    /// Colecteaza datele necesare si valideaza input-ul utilizatorului.
    /// </summary>
    class DoctorForm : Form
    {
        // Elemente interfata create de VS
        private TextBox textBoxUsername;
        private TextBox textBoxParola;
        private TextBox textBoxNume;
        private TextBox textBoxPrenume;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button buttonAdaugaDoctor;

        /// <summary>
        /// Doctorul creat pe baza datelor introduse in formular.
        /// </summary>
        public Doctor DoctorNou { get; private set; }

        /// <summary>
        /// Constructorul formularului initializeaza componentele UI.
        /// </summary>
        public DoctorForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxParola = new System.Windows.Forms.TextBox();
            this.textBoxNume = new System.Windows.Forms.TextBox();
            this.textBoxPrenume = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonAdaugaDoctor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(67, 39);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(121, 22);
            this.textBoxUsername.TabIndex = 0;
            // 
            // textBoxParola
            // 
            this.textBoxParola.Location = new System.Drawing.Point(67, 104);
            this.textBoxParola.Name = "textBoxParola";
            this.textBoxParola.Size = new System.Drawing.Size(121, 22);
            this.textBoxParola.TabIndex = 1;
            // 
            // textBoxNume
            // 
            this.textBoxNume.Location = new System.Drawing.Point(67, 170);
            this.textBoxNume.Name = "textBoxNume";
            this.textBoxNume.Size = new System.Drawing.Size(121, 22);
            this.textBoxNume.TabIndex = 2;
            // 
            // textBoxPrenume
            // 
            this.textBoxPrenume.Location = new System.Drawing.Point(67, 234);
            this.textBoxPrenume.Name = "textBoxPrenume";
            this.textBoxPrenume.Size = new System.Drawing.Size(121, 22);
            this.textBoxPrenume.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Parola";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nume";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 215);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Prenume";
            // 
            // buttonAdaugaDoctor
            // 
            this.buttonAdaugaDoctor.Location = new System.Drawing.Point(76, 297);
            this.buttonAdaugaDoctor.Name = "buttonAdaugaDoctor";
            this.buttonAdaugaDoctor.Size = new System.Drawing.Size(101, 37);
            this.buttonAdaugaDoctor.TabIndex = 8;
            this.buttonAdaugaDoctor.Text = "Adauga";
            this.buttonAdaugaDoctor.UseVisualStyleBackColor = true;
            this.buttonAdaugaDoctor.Click += new System.EventHandler(this.buttonAdaugaDoctor_Click);
            // 
            // DoctorForm
            // 
            this.ClientSize = new System.Drawing.Size(257, 361);
            this.Controls.Add(this.buttonAdaugaDoctor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPrenume);
            this.Controls.Add(this.textBoxNume);
            this.Controls.Add(this.textBoxParola);
            this.Controls.Add(this.textBoxUsername);
            this.Name = "DoctorForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // Callback-uri
        private void buttonAdaugaDoctor_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text == "" || textBoxParola.Text == "" || textBoxNume.Text == "" || textBoxPrenume.Text == "")
            {
                MessageBox.Show("Trebuie completate toate campurile!");
                return;
            }

            if (!Regex.IsMatch(textBoxNume.Text, @"^[a-zA-Z]+$") || !Regex.IsMatch(textBoxPrenume.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Numele/Prenumele trebuie sa contina doar litere!");
                return;
            }
            Observe o=new Observe();
            DoctorNou = new Doctor(1 /*DEFAULT VALUE*/, textBoxUsername.Text, textBoxParola.Text, textBoxNume.Text, textBoxPrenume.Text, o);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
