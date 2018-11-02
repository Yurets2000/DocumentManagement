using System;
using System.Windows.Forms;

namespace DocumentManagement
{
    partial class DocumentManagementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AddCompany = new System.Windows.Forms.Button();
            this.SelectCompany = new System.Windows.Forms.Button();
            this.companiesBox = new System.Windows.Forms.ComboBox();
            this.AddPerson = new System.Windows.Forms.Button();
            this.RemoveCompany = new System.Windows.Forms.Button();
            this.RemovePerson = new System.Windows.Forms.Button();
            this.personsBox = new System.Windows.Forms.ComboBox();
            this.SelectPerson = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddCompany
            // 
            this.AddCompany.BackColor = System.Drawing.Color.Tomato;
            this.AddCompany.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddCompany.Location = new System.Drawing.Point(593, 148);
            this.AddCompany.Name = "AddCompany";
            this.AddCompany.Size = new System.Drawing.Size(250, 30);
            this.AddCompany.TabIndex = 1;
            this.AddCompany.Text = "Добавить организацию";
            this.AddCompany.UseVisualStyleBackColor = false;
            this.AddCompany.Click += new System.EventHandler(this.AddCompany_Click);
            // 
            // SelectCompany
            // 
            this.SelectCompany.BackColor = System.Drawing.Color.Tomato;
            this.SelectCompany.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SelectCompany.Location = new System.Drawing.Point(592, 16);
            this.SelectCompany.Name = "SelectCompany";
            this.SelectCompany.Size = new System.Drawing.Size(251, 30);
            this.SelectCompany.TabIndex = 3;
            this.SelectCompany.Text = "Выбрать организацию";
            this.SelectCompany.UseVisualStyleBackColor = false;
            this.SelectCompany.Click += new System.EventHandler(this.SelectCompany_Click);
            // 
            // companiesBox
            // 
            this.companiesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.companiesBox.FormattingEnabled = true;
            this.companiesBox.Location = new System.Drawing.Point(592, 52);
            this.companiesBox.Name = "companiesBox";
            this.companiesBox.Size = new System.Drawing.Size(251, 21);
            this.companiesBox.TabIndex = 4;
            // 
            // AddPerson
            // 
            this.AddPerson.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.AddPerson.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddPerson.Location = new System.Drawing.Point(12, 148);
            this.AddPerson.Name = "AddPerson";
            this.AddPerson.Size = new System.Drawing.Size(249, 30);
            this.AddPerson.TabIndex = 6;
            this.AddPerson.Text = "Добавить персону";
            this.AddPerson.UseVisualStyleBackColor = false;
            this.AddPerson.Click += new System.EventHandler(this.AddPerson_Click);
            // 
            // RemoveCompany
            // 
            this.RemoveCompany.BackColor = System.Drawing.Color.Tomato;
            this.RemoveCompany.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RemoveCompany.Location = new System.Drawing.Point(592, 196);
            this.RemoveCompany.Name = "RemoveCompany";
            this.RemoveCompany.Size = new System.Drawing.Size(250, 30);
            this.RemoveCompany.TabIndex = 7;
            this.RemoveCompany.Text = "Удалить организацию";
            this.RemoveCompany.UseVisualStyleBackColor = false;
            this.RemoveCompany.Click += new System.EventHandler(this.RemoveCompany_Click);
            // 
            // RemovePerson
            // 
            this.RemovePerson.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.RemovePerson.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RemovePerson.Location = new System.Drawing.Point(12, 196);
            this.RemovePerson.Name = "RemovePerson";
            this.RemovePerson.Size = new System.Drawing.Size(249, 30);
            this.RemovePerson.TabIndex = 8;
            this.RemovePerson.Text = "Удалить персону";
            this.RemovePerson.UseVisualStyleBackColor = false;
            this.RemovePerson.Click += new System.EventHandler(this.RemovePerson_Click);
            // 
            // personsBox
            // 
            this.personsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.personsBox.FormattingEnabled = true;
            this.personsBox.Location = new System.Drawing.Point(10, 52);
            this.personsBox.Name = "personsBox";
            this.personsBox.Size = new System.Drawing.Size(251, 21);
            this.personsBox.TabIndex = 9;
            // 
            // SelectPerson
            // 
            this.SelectPerson.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.SelectPerson.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SelectPerson.Location = new System.Drawing.Point(10, 15);
            this.SelectPerson.Name = "SelectPerson";
            this.SelectPerson.Size = new System.Drawing.Size(251, 30);
            this.SelectPerson.TabIndex = 11;
            this.SelectPerson.Text = "Выбрать персону";
            this.SelectPerson.UseVisualStyleBackColor = false;
            this.SelectPerson.Click += new System.EventHandler(this.SelectPerson_Click);
            // 
            // DocumentManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.ClientSize = new System.Drawing.Size(854, 403);
            this.Controls.Add(this.SelectPerson);
            this.Controls.Add(this.personsBox);
            this.Controls.Add(this.RemovePerson);
            this.Controls.Add(this.RemoveCompany);
            this.Controls.Add(this.AddPerson);
            this.Controls.Add(this.companiesBox);
            this.Controls.Add(this.SelectCompany);
            this.Controls.Add(this.AddCompany);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "DocumentManagementForm";
            this.Text = "DocumentManagementForm";
            this.ResumeLayout(false);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(DocumentManagementForm_Closing);

        }

        #endregion
        private System.Windows.Forms.Button AddCompany;
        private System.Windows.Forms.Button SelectCompany;
        private System.Windows.Forms.ComboBox companiesBox;
        private System.Windows.Forms.Button AddPerson;
        private System.Windows.Forms.Button RemoveCompany;
        private System.Windows.Forms.Button RemovePerson;
        private System.Windows.Forms.ComboBox personsBox;
        private System.Windows.Forms.Button SelectPerson;
    }
}

