using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DocumentManagement
{
    public class PersonForm : Form
    {
        public PersonForm()
        {
            InitializeForm();
        }

        private void InitializeForm()
        {
            Width = 500;
            Height = 400;
            Name = "PersonForm";
            Text = "Person Form";
            BackColor = Color.GreenYellow;
            StartPosition = FormStartPosition.CenterScreen;

            Label name = new Label
            {
                Text = "Имя:"
            };
            name.SetBounds(10, 20, 80, 30);
            TextBox nameBox = new TextBox
            {
                Name = "nameBox"
            };
            nameBox.SetBounds(95, 20, 150, 30);

            Label surname = new Label
            {
                Text = "Фамилия:"
            };
            surname.SetBounds(10, 55, 80, 30);
            TextBox surnameBox = new TextBox
            {
                Name = "surnameBox"
            };
            surnameBox.SetBounds(95, 55, 150, 30);

            Label age = new Label
            {
                Text = "Возраст:"
            };
            age.SetBounds(10, 90, 80, 30);

            TextBox ageBox = new TextBox
            {
                Name = "ageBox"
            };
            ageBox.SetBounds(95, 90, 150, 30);

            Button commitButton = new Button
            {
                Name = "commitButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Подтвердить"
            };
            commitButton.SetBounds(10, 125, 235, 30);
            commitButton.Click += new EventHandler(CommitButton_Click);

            Controls.Add(name);
            Controls.Add(nameBox);
            Controls.Add(surname);
            Controls.Add(surnameBox);
            Controls.Add(age);
            Controls.Add(ageBox);
            Controls.Add(commitButton);
        }

        private void CommitButton_Click(object sender, EventArgs e)
        {
            bool filled = Utils.CheckFormFilled(this);
            if (filled)
            {
                string name = Utils.FindControl(this, "nameBox").Text;
                string surname = Utils.FindControl(this, "surnameBox").Text;
                string ageString = Utils.FindControl(this, "ageBox").Text;

                bool nameValidated = PersonFormValidator.ValidateName(name, surname);
                bool ageValidated = PersonFormValidator.ValidateAge(ageString);

                if (nameValidated && ageValidated)
                {
                    Person person = new Person(name, surname, int.Parse(ageString))
                    {
                        Working = false
                    };
                    SqlPersonDAO.AddPerson(person);
                    DocumentManagementForm form = (DocumentManagementForm)Application.OpenForms["DocumentManagementForm"];
                    form.UpdatePersonsBox();
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
