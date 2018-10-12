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
    public class DirectorInfoForm : Form
    {
        private Director director;

        public DirectorInfoForm(Director director)
        {
            this.director = director;
            InitializeForm();
        }

        private void InitializeForm()
        {
            Width = 500;
            Height = 400;
            Name = "DirectorInfoForm";
            Text = "Director Info Form";
            BackColor = Color.MediumTurquoise;
            StartPosition = FormStartPosition.CenterScreen;

            Label name = new Label
            {
                Text = "Имя:"
            };
            name.SetBounds(10, 20, 80, 30);

            Label nameInfo = new Label
            {
                Name = "nameInfo",
                Text = director.Name
            };
            nameInfo.SetBounds(95, 20, 150, 30);

            Label surname = new Label
            {
                Text = "Фамилия:"
            };
            surname.SetBounds(10, 55, 80, 30);

            Label surnameInfo = new Label
            {
                Name = "surnameInfo",
                Text = director.Surname
            };
            surnameInfo.SetBounds(95, 55, 150, 30);

            Label age = new Label
            {
                Text = "Возраст:"
            };
            age.SetBounds(10, 90, 80, 30);

            Label ageInfo = new Label
            {
                Name = "ageInfo",
                Text = director.Age.ToString()
            };
            ageInfo.SetBounds(95, 90, 150, 30);

            Label salary = new Label
            {
                Text = "Зарплата:"
            };
            salary.SetBounds(10, 160, 80, 30);

            Label workplace = new Label
            {
                Text = "Место работы:"
            };
            workplace.SetBounds(10, 125, 80, 30);

            Label workplaceInfo = new Label
            {
                Name = "workplaceInfo",
                Text = director.Company.ToString()
            };
            workplaceInfo.SetBounds(95, 125, 150, 30);

            Controls.Add(name);
            Controls.Add(nameInfo);
            Controls.Add(surname);
            Controls.Add(surnameInfo);
            Controls.Add(age);
            Controls.Add(ageInfo);
            Controls.Add(salary);
            Controls.Add(workplace);
            Controls.Add(workplaceInfo);
            AddInfoForms();
        }

        private void AddInfoForms()
        {
            Label salaryInfo = new Label
            {
                Name = "salaryInfo",
                Text = director.Salary.ToString()
            };
            salaryInfo.SetBounds(95, 160, 150, 30);

            Button editButton = new Button
            {
                Name = "editButton",
                Text = "Редактировать",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204)))
            };
            editButton.SetBounds(10, 285, 235, 30);
            editButton.Click += new EventHandler(EditEvent);

            Controls.Add(salaryInfo);
            Controls.Add(editButton);
        }

        private void AddEditForms()
        {
            TextBox salaryBox = new TextBox
            {
                Name = "salaryBox",
                Text = director.Salary.ToString()
            };
            salaryBox.SetBounds(95, 160, 150, 30);

            Button commitEditButton = new Button
            {
                Name = "commitEditButon",
                Text = "Подтвердить",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204)))
            };
            commitEditButton.SetBounds(10, 285, 235, 30);
            commitEditButton.Click += new EventHandler(CommitEditEvent);

            Controls.Add(salaryBox);
            Controls.Add(commitEditButton);
        }

        private void EditEvent(object sender, EventArgs e)
        {
            Control salaryInfo = Utils.FindControl(this, "salaryInfo");
            Controls.Remove(salaryInfo);
            Controls.Remove((Control) sender);
            AddEditForms();
        }

        private void CommitEditEvent(object sender, EventArgs e)
        {
            bool filled = true;
            bool correct = true;
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    if (string.IsNullOrWhiteSpace(c.Text))
                    {
                        MessageBox.Show("Одно из полей пустое!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        filled = false;
                        break;
                    }
                }
            }
            if (filled)
            {
                Control salaryBox = Utils.FindControl(this, "salaryBox");
                try
                {
                    director.Salary = int.Parse(salaryBox.Text);
                    SqlDirectorDAO.UpdateDirector(director);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Неправильно введенные данные!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    correct = false;
                }
                if (correct)
                {
                    Controls.Remove(salaryBox);
                    Controls.Remove((Control) sender);
                    AddInfoForms();
                }
            }
        }
    }
}
