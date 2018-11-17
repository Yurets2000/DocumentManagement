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
    public class SecretaryInfoForm : Form
    {
        private Secretary secretary;

        public SecretaryInfoForm(Secretary secretary)
        {
            this.secretary = secretary;
            InitializeForm();
        }

        private void InitializeForm()
        {
            Width = 900;
            Height = 500;
            Name = "SecretaryInfoForm";
            Text = "Secretary Info Form";
            BackColor = Color.MediumTurquoise;
            StartPosition = FormStartPosition.CenterScreen;

            Label info = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Информация о секретаре"
            };
            info.SetBounds(10, 20, 250, 30);

            Label name = new Label
            {
                Text = "Имя:"
            };
            name.SetBounds(10, 55, 80, 30);

            Label nameInfo = new Label
            {
                Text = secretary.Name
            };
            nameInfo.SetBounds(95, 55, 150, 30);

            Label surname = new Label
            {
                Text = "Фамилия:"
            };
            surname.SetBounds(10, 90, 80, 30);

            Label surnameInfo = new Label
            {
                Text = secretary.Surname
            };
            surnameInfo.SetBounds(95, 90, 150, 30);

            Label age = new Label
            {
                Text = "Возраст:"
            };
            age.SetBounds(10, 125, 80, 30);

            Label ageInfo = new Label
            {
                Text = secretary.Age.ToString()
            };
            ageInfo.SetBounds(95, 125, 150, 30);

            Label salary = new Label
            {
                Text = "Зарплата:"
            };
            salary.SetBounds(10, 160, 80, 30);

            Label salaryInfo = new Label
            {
                Text = secretary.Salary.ToString()
            };
            salaryInfo.SetBounds(95, 160, 150, 30);

            Label workplace = new Label
            {
                Text = "Место работы:"
            };
            workplace.SetBounds(10, 195, 80, 30);

            Label workplaceInfo = new Label
            {
                Text = secretary.Company.ToString()
            };
            workplaceInfo.SetBounds(95, 195, 150, 30);

            Button createDocumentButton = new Button
            {
                Name = "createDocumentButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Создать документ"
            };
            createDocumentButton.SetBounds(10, 265, 245, 30);
            createDocumentButton.Click += new EventHandler(CreateDocumentButton_Click);

            Label createdDocuments = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Созданные документы"
            };
            createdDocuments.SetBounds(270, 20, 250, 30);

            ComboBox createdDocumentsBox = new ComboBox
            {
                Name = "createdDocumentsBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = secretary.CreatedDocuments
            };
            createdDocumentsBox.SetBounds(270, 55, 250, 30);

            Button selectCreatedDocumentButton = new Button
            {
                Name = "selectCreatedDocumentButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Просмотреть"
            };
            selectCreatedDocumentButton.SetBounds(270, 105, 250, 30);
            selectCreatedDocumentButton.Click += new EventHandler(SelectCreatedDocumentButton_Click);

            Button editCreatedDocumentButton = new Button
            {
                Name = "editCreatedDocumentButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Редактировать"
            };
            editCreatedDocumentButton.SetBounds(270, 140, 250, 30);
            editCreatedDocumentButton.Click += new EventHandler(EditCreatedDocumentButton_Click);

            Button removeCreatedDocumentButton = new Button
            {
                Name = "removeCreatedDocumentButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Удалить"
            };
            removeCreatedDocumentButton.SetBounds(270, 175, 250, 30);
            removeCreatedDocumentButton.Click += new EventHandler(RemoveCreatedDocumentButton_Click);

            Button sendCreatedDocumentButton = new Button
            {
                Name = "sendCreatedDocumentButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Отправить директору"
            };
            sendCreatedDocumentButton.SetBounds(270, 210, 250, 30);
            sendCreatedDocumentButton.Click += new EventHandler(SendCreatedDocumentButton_Click);

            //------------------------------------------------------------------------------
            Label pendingDocuments = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Документы к редактированию"
            };
            pendingDocuments.SetBounds(540, 20, 250, 30);

            ComboBox pendingDocumentsBox = new ComboBox
            {
                Name = "pendingDocumentsBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = secretary.PendingDocuments
            };
            pendingDocumentsBox.SetBounds(540, 55, 250, 30);

            Button selectPendingDocumentButton = new Button
            {
                Name = "selectPendingDocumentButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Просмотреть"
            };
            selectPendingDocumentButton.SetBounds(540, 105, 250, 30);
            selectPendingDocumentButton.Click += new EventHandler(SelectPendingDocumentButton_Click);

            Button editPendingDocumentButton = new Button
            {
                Name = "editPendingDocumentButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Редактировать"
            };
            editPendingDocumentButton.SetBounds(540, 140, 250, 30);
            editPendingDocumentButton.Click += new EventHandler(EditPendingDocumentButton_Click);

            Button removePendingDocumentButton = new Button
            {
                Name = "removePendingDocumentButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Удалить"
            };
            removePendingDocumentButton.SetBounds(540, 175, 250, 30);
            removePendingDocumentButton.Click += new EventHandler(RemovePendingDocumentButton_Click);

            Button sendPendingDocumentButton = new Button
            {
                Name = "sendPendingDocumentButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Отправить директору"
            };
            sendPendingDocumentButton.SetBounds(540, 210, 250, 30);
            sendPendingDocumentButton.Click += new EventHandler(SendPendingDocumentButton_Click);

            Controls.Add(info);
            Controls.Add(name);
            Controls.Add(nameInfo);
            Controls.Add(surname);
            Controls.Add(surnameInfo);
            Controls.Add(age);
            Controls.Add(ageInfo);
            Controls.Add(salary);
            Controls.Add(workplace);
            Controls.Add(workplaceInfo);
            Controls.Add(createdDocuments);
            Controls.Add(createdDocumentsBox);
            Controls.Add(createDocumentButton);
            Controls.Add(selectCreatedDocumentButton);
            Controls.Add(editCreatedDocumentButton);
            Controls.Add(removeCreatedDocumentButton);
            Controls.Add(sendCreatedDocumentButton);
            Controls.Add(pendingDocuments);
            Controls.Add(pendingDocumentsBox);
            Controls.Add(selectPendingDocumentButton);
            Controls.Add(editPendingDocumentButton);
            Controls.Add(removePendingDocumentButton);
            Controls.Add(sendPendingDocumentButton);
            
            AddInfoForms();
        }

        private void SelectPendingDocumentButton_Click(object sender, EventArgs e)
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

        private void RemovePendingDocumentButton_Click(object sender, EventArgs e)
        {
            ComboBox documentsBox = (ComboBox)Utils.FindControl(this, "pendingDocumentsBox");
            Document document = (Document)documentsBox.SelectedItem;
            if (document == null)
            {
                MessageBox.Show("Документ не выбран!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                secretary.RemoveCreatedDocument(document);
                UpdatePendingDocumentsBox();
            }
        }

        private void EditPendingDocumentButton_Click(object sender, EventArgs e)
        {
            ComboBox createdDocumentsBox = (ComboBox)Utils.FindControl(this, "pendingDocumentsBox");
            Document document = (Document)createdDocumentsBox.SelectedItem;
            if (document == null)
            {
                MessageBox.Show("Документ не выбран!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DocumentEditForm documentEditForm = new DocumentEditForm(secretary, document);
                documentEditForm.Disposed += new EventHandler(DocumentEditForm_Disposed);
                documentEditForm.Activate();
                documentEditForm.ShowDialog();
            }
        }

        private void SendPendingDocumentButton_Click(object sender, EventArgs e)
        {
            ComboBox createdDocumentsBox = (ComboBox)Utils.FindControl(this, "pendingDocumentsBox");
            Document document = (Document)createdDocumentsBox.SelectedItem;
            if (document == null)
            {
                MessageBox.Show("Документ не выбран!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                secretary.SendPendingDocument(document);
                UpdatePendingDocumentsBox();
            }
        }

        private void AddInfoForms()
        {
            Label salaryInfo = new Label
            {
                Name = "salaryInfo",
                Text = secretary.Salary.ToString()
            };
            salaryInfo.SetBounds(95, 160, 150, 30);

            Button editSecretaryButton = new Button
            {
                Name = "editSecretaryButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Редактировать"
            };
            editSecretaryButton.SetBounds(10, 230, 245, 30);
            editSecretaryButton.Click += new EventHandler(EditSecretaryButton_Click);

            Controls.Add(salaryInfo);
            Controls.Add(editSecretaryButton);
        }

        private void AddEditForms()
        {
            TextBox salaryBox = new TextBox
            {
                Name = "salaryBox",
                Text = secretary.Salary.ToString()
            };
            salaryBox.SetBounds(95, 160, 150, 30);

            Button commitEditSecretaryButton = new Button
            {
                Name = "commitEditSecretaryButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Подтвердить"
            };
            commitEditSecretaryButton.SetBounds(10, 230, 245, 30);
            commitEditSecretaryButton.Click += new EventHandler(CommitEditSecretaryButton_Click);

            Controls.Add(salaryBox);
            Controls.Add(commitEditSecretaryButton);
        }

        private void EditSecretaryButton_Click(object sender, EventArgs e)
        {
            Control salaryInfo = Utils.FindControl(this, "salaryInfo");
            Controls.Remove(salaryInfo);    
            Controls.Remove((Control)sender);

            AddEditForms();
        }

        private void CommitEditSecretaryButton_Click(object sender, EventArgs e)
        {
            Control salaryBox = Utils.FindControl(this, "salaryBox");

            bool filled = !String.IsNullOrWhiteSpace(salaryBox.Text);

            if (filled)
            {
                string salaryString = salaryBox.Text;

                bool salaryValidated = SecretaryFormValidator.ValidateSalary(salaryString);

                if (salaryValidated)
                {
                    secretary.Salary = int.Parse(salaryBox.Text);

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

        private void CreateDocumentButton_Click(object sender, EventArgs e)
        {
            DocumentForm documentForm = new DocumentForm(secretary);
            documentForm.Activate();
            documentForm.ShowDialog();
        }

        private void SelectCreatedDocumentButton_Click(object sender, EventArgs e)
        {
            ComboBox documentsBox = (ComboBox)Utils.FindControl(this, "createdDocumentsBox");
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

        private void EditCreatedDocumentButton_Click(object sender, EventArgs e)
        {
            ComboBox createdDocumentsBox = (ComboBox)Utils.FindControl(this, "createdDocumentsBox");
            Document document = (Document)createdDocumentsBox.SelectedItem;
            if (document == null)
            {
                MessageBox.Show("Документ не выбран!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DocumentEditForm documentEditForm = new DocumentEditForm(secretary, document);
                documentEditForm.Disposed += new EventHandler(DocumentEditForm_Disposed);
                documentEditForm.Activate();
                documentEditForm.ShowDialog();
            }
        }

        private void RemoveCreatedDocumentButton_Click(object sender, EventArgs e)
        {
            ComboBox documentsBox = (ComboBox)Utils.FindControl(this, "createdDocumentsBox");
            Document document = (Document)documentsBox.SelectedItem;
            if (document == null)
            {
                MessageBox.Show("Документ не выбран!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                secretary.RemoveCreatedDocument(document);
                UpdateCreatedDocumentsBox();
            }
        }

        private void SendCreatedDocumentButton_Click(object sender, EventArgs e)
        {
            ComboBox createdDocumentsBox = (ComboBox)Utils.FindControl(this, "createdDocumentsBox");
            Document document = (Document)createdDocumentsBox.SelectedItem;
            if (document == null)
            {
                MessageBox.Show("Документ не выбран!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                secretary.SendCreatedDocument(document);
                UpdateCreatedDocumentsBox();
            }
        }

        private void DocumentEditForm_Disposed(object sender, EventArgs e)
        {
            UpdateCreatedDocumentsBox();
            UpdatePendingDocumentsBox();
        }

        public void UpdateCreatedDocumentsBox()
        {
            ComboBox documentsBox = (ComboBox)Utils.FindControl(this, "createdDocumentsBox");
            documentsBox.DataSource = null;
            documentsBox.Items.Clear();
            documentsBox.DataSource = secretary.CreatedDocuments;
        }

        public void UpdatePendingDocumentsBox()
        {
            ComboBox documentsBox = (ComboBox)Utils.FindControl(this, "pendingDocumentsBox");
            documentsBox.DataSource = null;
            documentsBox.Items.Clear();
            documentsBox.DataSource = secretary.PendingDocuments;
        }
    
    }
}
