using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DocumentManagement
{
    class MainSecretaryInfoForm : Form
    {
        private MainSecretary mainSecretary;

        public MainSecretaryInfoForm(MainSecretary mainSecretary)
        {
            this.mainSecretary = mainSecretary;
            InitializeForm();
        }

        private void InitializeForm()
        {
            Width = 700;
            Height = 500;
            Name = "MainSecretaryInfoForm";
            Text = "Main Secretary Info Form";
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
                Text = mainSecretary.Name
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
                Text = mainSecretary.Surname
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
                Text = mainSecretary.Age.ToString()
            };
            ageInfo.SetBounds(95, 90, 150, 30);

            Label salary = new Label
            {
                Text = "Зарплата:"
            };
            salary.SetBounds(10, 125, 80, 30);

            Label secretaries = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Список секретарей"
            };
            secretaries.SetBounds(300, 20, 250, 30);

            ComboBox secretariesBox = new ComboBox
            {
                Name = "secretariesBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = mainSecretary.Company.Chancery.Secretaries
            };
            secretariesBox.SetBounds(300, 55, 250, 30);

            Label documents = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Список документов"
            };
            documents.SetBounds(300, 105, 250, 30);

            ComboBox pendingDocumentsBox = new ComboBox
            {
                Name = "pendingDocumentsBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = mainSecretary.Company.Chancery.PendingDocuments
            };
            pendingDocumentsBox.SetBounds(300, 140, 250, 30);

            Button redirectDocumentToSecretaryButton = new Button
            {
                Name = "redirectDocumentToSecretaryButton",
                Text = "Отправить секретарю",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204)))
            };
            redirectDocumentToSecretaryButton.SetBounds(300, 175, 250, 30);
            redirectDocumentToSecretaryButton.Click += new EventHandler(RedirectDocumentToSecretaryButton_Click);

            Button redirectDocumentByCountButton = new Button
            {
                Name = "editButton",
                Text = "Распределить",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204)))
            };
            redirectDocumentByCountButton.SetBounds(300, 210, 250, 30);
            redirectDocumentByCountButton.Click += new EventHandler(RedirectDocumentByCountButton_Click);

            Controls.Add(name);
            Controls.Add(nameInfo);
            Controls.Add(surname);
            Controls.Add(surnameInfo);
            Controls.Add(age);
            Controls.Add(ageInfo);
            Controls.Add(salary);
            Controls.Add(secretaries);
            Controls.Add(secretariesBox);
            Controls.Add(documents);
            Controls.Add(pendingDocumentsBox);
            Controls.Add(redirectDocumentToSecretaryButton);
            Controls.Add(redirectDocumentByCountButton);
            AddInfoForms();
        }

        public void RedirectDocumentToSecretaryButton_Click(object sender, EventArgs e)
        {
            ComboBox pendingDocumentsBox = (ComboBox)Utils.FindControl(this, "pendingDocumentsBox");
            ComboBox secretariesBox = (ComboBox)Utils.FindControl(this, "secretariesBox");
            Document document = (Document)pendingDocumentsBox.SelectedItem;
            Secretary secretary = (Secretary)secretariesBox.SelectedItem;
            if (document == null || secretary == null)
            {
                MessageBox.Show("Документ или секретарь не выбран!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                mainSecretary.RedirectPendingDocument(secretary, document);
                UpdatePendingDocumentsBox();
            }
        }

        public void RedirectDocumentByCountButton_Click(object sender, EventArgs e)
        {
            ComboBox pendingDocumentsBox = (ComboBox) Utils.FindControl(this, "pendingDocumentsBox");
            Document document = (Document)pendingDocumentsBox.SelectedItem;
            if(document == null)
            {
                MessageBox.Show("Документ не выбран!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                mainSecretary.RedirectPendingDocument(document);
                UpdatePendingDocumentsBox();
            }
        }

        private void AddInfoForms()
        {
            Label salaryInfo = new Label
            {
                Name = "salaryInfo",
                Text = mainSecretary.Salary.ToString()
            };
            salaryInfo.SetBounds(95, 125, 150, 30);

            Button editButton = new Button
            {
                Name = "editButton",
                Text = "Редактировать",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204)))
            };
            editButton.SetBounds(10, 160, 235, 30);
            editButton.Click += new EventHandler(EditEvent);

            Controls.Add(salaryInfo);
            Controls.Add(editButton);
        }

        private void AddEditForms()
        {
            TextBox salaryBox = new TextBox
            {
                Name = "salaryBox",
                Text = mainSecretary.Salary.ToString()
            };
            salaryBox.SetBounds(95, 125, 150, 30);

            Button commitEditButton = new Button
            {
                Name = "commitEditButon",
                Text = "Подтвердить",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204)))
            };
            commitEditButton.SetBounds(10, 160, 235, 30);
            commitEditButton.Click += new EventHandler(CommitEditEvent);

            Controls.Add(salaryBox);
            Controls.Add(commitEditButton);
        }

        private void CommitEditEvent(object sender, EventArgs e)
        {
            Control salaryBox = Utils.FindControl(this, "salaryBox");

            bool filled = !String.IsNullOrWhiteSpace(salaryBox.Text);

            if (filled)
            {
                string salaryString = salaryBox.Text;

                bool salaryValidated = SecretaryFormValidator.ValidateSalary(salaryString);

                if (salaryValidated)
                {
                    mainSecretary.Salary = int.Parse(salaryBox.Text);

                    Controls.Remove(salaryBox);
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

        private void EditEvent(object sender, EventArgs e)
        {
            Control salaryInfo = Utils.FindControl(this, "salaryInfo");
            Controls.Remove(salaryInfo);
            Controls.Remove((Control)sender);
            AddEditForms();
        }

        public void UpdatePendingDocumentsBox()
        {
            ComboBox pendingDocumentsBox = (ComboBox)Utils.FindControl(this, "pendingDocumentsBox");
            pendingDocumentsBox.DataSource = null;
            pendingDocumentsBox.Items.Clear();
            pendingDocumentsBox.DataSource = mainSecretary.Company.Chancery.PendingDocuments;
        }
    }
}
