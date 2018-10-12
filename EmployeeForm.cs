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
    public class EmployeeForm : Form
    {
        public Person Person { get; private set; }
        public int Salary { get; private set; }
        public bool CorrectOnClose { get; private set; }

        public EmployeeForm(Person person)
        {
            Person = person;
            InitializeForm();
        }

        private void InitializeForm()
        {
            Width = 500;
            Height = 400;
            Name = "EmployeeForm";
            Text = "Employee Form";
            BackColor = Color.GreenYellow;
            StartPosition = FormStartPosition.CenterScreen;

            Label name = new Label
            {
                Text = "Имя:"
            };
            name.SetBounds(10, 20, 80, 30);

            Label nameInfo = new Label
            {
                Text = Person.Name
            };
            nameInfo.SetBounds(95, 20, 150, 30);

            Label surname = new Label
            {
                Text = "Фамилия:"
            };
            surname.SetBounds(10, 55, 80, 30);

            Label surnameInfo = new Label
            {
                Text = Person.Surname
            };
            surnameInfo.SetBounds(95, 55, 150, 30);

            Label age = new Label
            {
                Text = "Возраст:"
            };
            age.SetBounds(10, 90, 80, 30);

            Label ageInfo = new Label
            {
                Text = Person.Age.ToString()
            };
            ageInfo.SetBounds(95, 90, 150, 30);

            Label salary = new Label
            {
                Text = "Зарплата:"
            };
            salary.SetBounds(10, 125, 80, 30);

            TextBox salaryBox = new TextBox
            {
                Name = "salaryBox"
            };
            salaryBox.SetBounds(95, 125, 150, 30);
            
            Button commitButton = new Button
            {
                Name = "commitButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Подтвердить"
            };
            commitButton.SetBounds(10, 150, 235, 30);
            commitButton.Click += new EventHandler(CommitButton_Click);

            Controls.Add(name);
            Controls.Add(nameInfo);
            Controls.Add(surname);
            Controls.Add(surnameInfo);
            Controls.Add(age);
            Controls.Add(ageInfo);
            Controls.Add(commitButton);
            Controls.Add(salary);
            Controls.Add(salaryBox);
        }

        private void CommitButton_Click(object sender, EventArgs e)
        {
            bool filled = Utils.CheckFormFilled(this);
            if (filled)
            {
                string salaryString = Utils.FindControl(this, "salaryBox").Text;
                bool salaryValidated = EmployeeFormValidator.ValidateSalary(salaryString);
                if (salaryValidated)
                {
                    Salary = int.Parse(salaryString);
                    CorrectOnClose = true;
                    Close();
                    Dispose();
                }
                else
                {
                    MessageBox.Show("Нерпавильно введенные данные!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CorrectOnClose = false;
                }
            }
            else
            {
                MessageBox.Show("Одно из полей пустое!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CorrectOnClose = false;
            }
        }
    }
}
