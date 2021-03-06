﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentManagement
{
    public class CompanyForm : Form
    {
        private Director director;

        public CompanyForm()
        {
            InitializeForm();
        }

        private void InitializeForm()
        {
            Width = 600;
            Height = 400;
            Name = "CompanyForm";
            Text = "Company Adder Form";
            BackColor = Color.Orange;
            StartPosition = FormStartPosition.CenterScreen;

            Label name = new Label
            {
                Text = "Название:"
            };
            name.SetBounds(10, 20, 80, 30);

            TextBox nameBox = new TextBox
            {
                Name = "nameBox"
            };
            nameBox.SetBounds(95, 20, 150, 30);

            Label address = new Label
            {
                Text = "Адрес:"
            };
            address.SetBounds(10, 55, 80, 30);

            TextBox addressBox = new TextBox
            {
                Name = "addressBox"
            };
            addressBox.SetBounds(95, 55, 150, 30);

            Label type = new Label
            {
                Text = "Тип:"
            };
            type.SetBounds(10, 90, 80, 30);

            ComboBox typeBox = new ComboBox
            {
                Name = "typeBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = DataStorage.GetInstance().DataLists.CompanyTypes
            };
            typeBox.SetBounds(95, 90, 150, 30);

            Button directorChooseButton = new Button
            {
                Name = "directorChooseButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Выбрать директора"
            };
            directorChooseButton.SetBounds(10, 125, 235, 30);
            directorChooseButton.Click += new EventHandler(DirectorChooseButton_Click);

            ComboBox personsBox = new ComboBox
            {
                Name = "personsBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = DataStorage.GetInstance().DataLists.Persons.Where(p => !p.Working).ToList()
            };
            personsBox.SetBounds(10, 160, 235, 30);

            Button addCompanyButton = new Button
            {
                Name = "addCompanyButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Добавить организацию"
            };
            addCompanyButton.SetBounds(10, 250, 235, 30);
            addCompanyButton.Click += new EventHandler(AddCompanyButton_Click);

            Controls.Add(name);
            Controls.Add(nameBox);
            Controls.Add(address);
            Controls.Add(addressBox);
            Controls.Add(type);
            Controls.Add(typeBox);
            Controls.Add(directorChooseButton);
            Controls.Add(personsBox);
            Controls.Add(addCompanyButton);
        }

        private void DirectorChooseButton_Click(object sender, EventArgs e)
        {
            ComboBox personsBox = (ComboBox)Utils.FindControl(this, "personsBox");
            if (personsBox.SelectedItem == null)
            {
                MessageBox.Show("Персона не выбрана!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Person person = (Person)personsBox.SelectedItem;
                DirectorForm directorForm = new DirectorForm(person);
                directorForm.Disposed += new EventHandler(DirectorFormDisposeEvent);
                directorForm.Activate();
                directorForm.ShowDialog();
            }
        }

        private void DirectorFormDisposeEvent(object sender, EventArgs e)
        {
            DirectorForm form = (DirectorForm)sender;
            if (form.CorrectOnClose)
            {
                Person person = form.Person;
                director = new Director(person, null, form.Signature, form.Salary)
                {
                    PendingDocuments = new List<Document>()
                };
            }
        }

        private void AddCompanyButton_Click(object sender, EventArgs e)
        {
            bool filled = Utils.CheckFormFilled(this);
            if (filled && director != null)
            {
                String name = Utils.FindControl(this, "nameBox").Text;
                String address = Utils.FindControl(this, "addressBox").Text;
                CompanyType type = (CompanyType)((ComboBox)Utils.FindControl(this, "typeBox")).SelectedValue;

                bool nameValidated = CompanyFormValidator.ValidateName(name);
                bool addressValidated = CompanyFormValidator.ValidateAddress(address);
                if (nameValidated && addressValidated)
                {
                    Chancery chancery = new Chancery
                    {
                        Archive = new List<Document>(),
                        PendingDocuments = new List<Document>(),
                        Secretaries = new List<Secretary>()
                    };
                    Company company = new Company(director, type, chancery, name, address);
                    company.Persist();
                    DocumentManagementForm form = (DocumentManagementForm)Application.OpenForms["DocumentManagementForm"];
                    form.UpdateCompaniesBox();
                    Close();
                    Dispose();
                }
                else
                {
                    MessageBox.Show("Неправильно введенные данные!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Одно из полей пустое!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
