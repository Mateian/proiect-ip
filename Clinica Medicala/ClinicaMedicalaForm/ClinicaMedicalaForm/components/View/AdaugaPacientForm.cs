/**************************************************************************
 *                                                                        *
 *  File:        AdaugaPacientForm.cs                                     *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Formular folosit pentru a adauga un pacient.             *
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClinicaMedicalaForm.components.Model;

namespace ClinicaMedicalaForm.components.View
{
    public class AdaugaPacientForm : Form
    {
        private ComboBox comboBoxPacienti;
        private Label label1;
        private Button buttonAdauga;

        private List<IUser> _utilizatori;
        public IUser Pacient { get; set; }

        public AdaugaPacientForm(List<IUser> utilizatori)
        {
            _utilizatori = utilizatori;
            InitializeComponent();
            InitForm();
        } 

        void InitForm()
        {
            foreach (IUser utilizator in _utilizatori)
            {
                if(utilizator.Rol == "Pacient")
                {
                    string displayText = $"{utilizator.ID} {utilizator.Nume} {utilizator.Prenume}";
                    comboBoxPacienti.Items.Add(displayText);
                }
            }
            this.Controls.Add(comboBoxPacienti);
        }

        private void InitializeComponent()
        {
            this.comboBoxPacienti = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAdauga = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxPacienti
            // 
            this.comboBoxPacienti.FormattingEnabled = true;
            this.comboBoxPacienti.Location = new System.Drawing.Point(60, 76);
            this.comboBoxPacienti.Name = "comboBoxPacienti";
            this.comboBoxPacienti.Size = new System.Drawing.Size(158, 24);
            this.comboBoxPacienti.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Adaugare pacient";
            // 
            // buttonAdauga
            // 
            this.buttonAdauga.Location = new System.Drawing.Point(98, 216);
            this.buttonAdauga.Name = "buttonAdauga";
            this.buttonAdauga.Size = new System.Drawing.Size(75, 23);
            this.buttonAdauga.TabIndex = 2;
            this.buttonAdauga.Text = "Adauga";
            this.buttonAdauga.UseVisualStyleBackColor = true;
            this.buttonAdauga.Click += new System.EventHandler(this.buttonAdauga_Click);
            // 
            // AdaugaPacientForm
            // 
            this.ClientSize = new System.Drawing.Size(289, 271);
            this.Controls.Add(this.buttonAdauga);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxPacienti);
            this.Name = "AdaugaPacientForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            int id;
            int.TryParse(comboBoxPacienti.SelectedItem.ToString().Split(' ')[0], out id);
            Pacient = (Pacient)_utilizatori.FirstOrDefault(u => u.ID == id);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
