using System;
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
    public class PersonInfoForm : Form
    {
        private Person person;

        public PersonInfoForm(Person person)
        {
            this.person = person;
            InitializeForm();
        }

        private void InitializeForm()
        {
            Width = 500;
            Height = 400;
            Name = "PersonInfoForm";
            Text = "Person Info Form";
            BackColor = Color.Aqua;
            StartPosition = FormStartPosition.CenterScreen;

            Label name = new Label
            {
                Text = "Имя:"
            };
            name.SetBounds(10, 20, 80, 30);

            Label surname = new Label
            {
                Text = "Фамилия:"
            };
            surname.SetBounds(10, 55, 80, 30);

            Label age = new Label
            {
                Text = "Возраст:"
            };
            age.SetBounds(10, 90, 80, 30);

            Controls.Add(name);
            Controls.Add(surname);
            Controls.Add(age);

            AddInfoForms();
        }

        private void AddInfoForms()
        {
            Label nameInfo = new Label
            {
                Name = "nameInfo",
                Text = person.Name
            };
            nameInfo.SetBounds(95, 20, 150, 30);

            Label surnameInfo = new Label
            {
                Name = "surnameInfo",
                Text = person.Surname
            };
            surnameInfo.SetBounds(95, 55, 150, 30);

            Label ageInfo = new Label
            {
                Name = "ageInfo",
                Text = person.Age.ToString()
            };
            ageInfo.SetBounds(95, 90, 150, 30);

            Button editButton = new Button
            {
                Name = "editButton",
                Text = "Редактировать",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204)))
            };
            editButton.SetBounds(10, 250, 235, 30);
            editButton.Click += new EventHandler(EditButton_Click);

            Controls.Add(nameInfo);
            Controls.Add(surnameInfo);
            Controls.Add(ageInfo);
            Controls.Add(editButton);
        }

        private void AddEditForms()
        {
            TextBox nameBox = new TextBox
            {
                Name = "nameBox",
                Text = person.Name
            };
            nameBox.SetBounds(95, 20, 150, 30);

            TextBox surnameBox = new TextBox
            {
                Name = "surnameBox",
                Text = person.Surname
            };
            surnameBox.SetBounds(95, 55, 150, 30);

            TextBox ageBox = new TextBox
            {
                Name = "ageBox",
                Text = person.Age.ToString()
            };
            ageBox.SetBounds(95, 90, 150, 30);

            Button commitEditButton = new Button
            {
                Name = "commitButon",
                Text = "Подтвердить",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204)))
            };
            commitEditButton.SetBounds(10, 250, 235, 30);
            commitEditButton.Click += new EventHandler(CommitButton_Click);

            Controls.Add(nameBox);
            Controls.Add(surnameBox);
            Controls.Add(ageBox);
            Controls.Add(commitEditButton);
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            Control nameInfo = Utils.FindControl(this, "nameInfo");
            Control surnameInfo = Utils.FindControl(this, "surnameInfo");
            Control ageInfo = Utils.FindControl(this, "ageInfo");

            Controls.Remove(nameInfo);
            Controls.Remove(surnameInfo);
            Controls.Remove(ageInfo);
            Controls.Remove((Control)sender);

            AddEditForms();
        }

        private void CommitButton_Click(object sender, EventArgs e)
        {
            bool filled = Utils.CheckFormFilled(this);
            if (filled)
            {
                Control nameBox = Utils.FindControl(this, "nameBox");
                Control surnameBox = Utils.FindControl(this, "surnameBox");
                Control ageBox = Utils.FindControl(this, "ageBox");

                string name = nameBox.Text;
                string surname = surnameBox.Text;
                string ageString = ageBox.Text;

                bool nameValidated = PersonFormValidator.ValidateName(name, surname);
                bool ageValidated = PersonFormValidator.ValidateAge(ageString);

                if (nameValidated && ageValidated)
                {
                    person.Name = name;
                    person.Surname = surname;
                    person.Age = int.Parse(ageString);
                    person.Update();

                    DocumentManagementForm form = (DocumentManagementForm)Application.OpenForms["DocumentManagementForm"];
                    form.UpdatePersonsBox();
                    form.UpdateCompaniesBox();
                    Controls.Remove(nameBox);
                    Controls.Remove(surnameBox);
                    Controls.Remove(ageBox);
                    Controls.Remove((Control)sender);
                    AddInfoForms();
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
