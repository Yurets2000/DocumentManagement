using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace DocumentManagement
{
    public class DirectorForm : Form
    {
        public Person Person { get; private set; }
        public int Salary { get; private set; }
        public byte[] Signature { get; private set; }
        public bool CorrectOnClose { get; private set; }

        public DirectorForm(Person person)
        {
            Person = person;
            InitializeForm();
        }

        private void InitializeForm()
        {
            Width = 500;
            Height = 400;
            Name = "DirectorForm";
            Text = "Director Form";
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

            Label signature = new Label
            {
                Text = "Подпись:"
            };
            signature.SetBounds(10, 160, 150, 30);

            SignatureBox signatureBox = new SignatureBox
            {
                Name = "signatureBox"
            };
            signatureBox.Location = new Point(10, 195);

            Button commitButton = new Button
            {
                Name = "commitButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Подтвердить"
            };
            commitButton.SetBounds(10, 295, 235, 30);
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
            Controls.Add(signature);
            Controls.Add(signatureBox);
        }

        private void CommitButton_Click(object sender, EventArgs e)
        {
            Control salaryBox = Utils.FindControl(this, "salaryBox");
            SignatureBox signatureBox = (SignatureBox)Utils.FindControl(this, "signatureBox");

            bool filled = !String.IsNullOrWhiteSpace(salaryBox.Text);

            if (filled)
            {
                string salaryString = Utils.FindControl(this, "salaryBox").Text;
                bool salaryValidated = DirectorFormValidator.ValidateSalary(salaryString);

                Bitmap bmp = new Bitmap(signatureBox.Width, signatureBox.Height);
                signatureBox.DrawToBitmap(bmp, new Rectangle(0, 0, signatureBox.Width, signatureBox.Height)); 
                byte[] signature = Utils.ImageToByte(bmp);

                if (salaryValidated)
                {
                    Salary = int.Parse(salaryString);
                    Signature = signature;
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
