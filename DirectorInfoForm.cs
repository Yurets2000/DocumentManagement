using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
            Width = 700;
            Height = 500;
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

            Label salary = new Label
            {
                Text = "Зарплата:"
            };
            salary.SetBounds(10, 160, 80, 30);

            Label signature = new Label
            {
                Text = "Подпись:"
            };
            signature.SetBounds(10, 195, 80, 30);

            Bitmap signatureBitmap;
            using (var ms = new MemoryStream(director.Signature))
            {
                signatureBitmap = new Bitmap(ms);
            }
            PictureBox signatureBox = new PictureBox
            {
                BackgroundImage = signatureBitmap,
                BackgroundImageLayout = ImageLayout.Zoom
            };
            signatureBox.SetBounds(10, 230, 150, 90);

            Label pendingDocuments = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Документы к подписи"
            };
            pendingDocuments.SetBounds(250, 20, 250, 30);

            ComboBox pendingDocumentsBox = new ComboBox
            {
                Name = "pendingDocumentsBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = director.PendingDocuments
            };
            pendingDocumentsBox.SetBounds(250, 55, 250, 30);

            Button selectDocumentButton = new Button
            {
                Name = "selectDocumentButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Просмотреть"
            };
            selectDocumentButton.SetBounds(250, 105, 250, 30);
            selectDocumentButton.Click += new EventHandler(SelectDocumentButton_Click);

            Button confirmDocumentButton = new Button
            {
                Name = "confirmDocumentButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Подписать"
            };
            confirmDocumentButton.SetBounds(250, 140, 250, 30);
            confirmDocumentButton.Click += new EventHandler(ConfirmDocumentButton_Click);

            Button rejectDocumentButton = new Button
            {
                Name = "rejectDocumentButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Не подписывать"
            };
            rejectDocumentButton.SetBounds(250, 175, 250, 30);
            rejectDocumentButton.Click += new EventHandler(RejectDocumentButton_Click);

            Controls.Add(name);
            Controls.Add(nameInfo);
            Controls.Add(surname);
            Controls.Add(surnameInfo);
            Controls.Add(age);
            Controls.Add(ageInfo);
            Controls.Add(salary);
            Controls.Add(workplace);
            Controls.Add(workplaceInfo);
            Controls.Add(signature);
            Controls.Add(signatureBox);
            Controls.Add(pendingDocuments);
            Controls.Add(pendingDocumentsBox);
            Controls.Add(confirmDocumentButton);
            Controls.Add(rejectDocumentButton);
            AddInfoForms(); 
        }

        private void SelectDocumentButton_Click(object sender, EventArgs e)
        {
            ComboBox documentsBox = (ComboBox)Utils.FindControl(this, "pendingDocumentsBox");
            Document document = (Document)documentsBox.SelectedItem;
            if (document == null)
            {
                MessageBox.Show("Документ не выбран!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DocumentInfoForm documentInfoForm = new DocumentInfoForm(document);
                documentInfoForm.Activate();
                documentInfoForm.ShowDialog();
            }
        }

        private void RejectDocumentButton_Click(object sender, EventArgs e)
        {
            ComboBox documentsBox = (ComboBox)Utils.FindControl(this, "pendingDocumentsBox");
            Document document = (Document)documentsBox.SelectedItem;
            if (document == null)
            {
                MessageBox.Show("Документ не выбран!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                director.RejectDocument(document);
            }
            UpdatePendingDocuments();
        }

        private void ConfirmDocumentButton_Click(object sender, EventArgs e)
        {
            ComboBox documentsBox = (ComboBox)Utils.FindControl(this, "pendingDocumentsBox");
            Document document = (Document)documentsBox.SelectedItem;
            if (document == null)
            {
                MessageBox.Show("Документ не выбран!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                director.ConfirmDocument(document);
            }
            UpdatePendingDocuments();
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
            editButton.SetBounds(10, 335, 235, 30);
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
            commitEditButton.SetBounds(10, 325, 235, 30);
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
            Control salaryBox = Utils.FindControl(this, "salaryBox");

            bool filled = !String.IsNullOrWhiteSpace(salaryBox.Text);
            
            if (filled)
            {
                string salaryString = salaryBox.Text;

                bool salaryValidated = DirectorFormValidator.ValidateSalary(salaryString);

                if (salaryValidated)
                {
                    director.Salary = int.Parse(salaryBox.Text);
                    director.Update();

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

        public void UpdatePendingDocuments()
        {
            ComboBox documentsBox = (ComboBox)Utils.FindControl(this, "pendingDocumentsBox");
            documentsBox.DataSource = null;
            documentsBox.Items.Clear();
            documentsBox.DataSource = director.PendingDocuments;
        }
    }
}
