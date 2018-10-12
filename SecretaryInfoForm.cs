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
            Width = 500;
            Height = 500;
            Name = "SecretaryInfoForm";
            Text = "Secretary Info Form";
            BackColor = Color.MediumTurquoise;
            StartPosition = FormStartPosition.CenterScreen;

            Label name = new Label
            {
                Text = "Имя:"
            };
            name.SetBounds(10, 20, 80, 30);

            Label nameInfo = new Label
            {
                Text = secretary.Name
            };
            nameInfo.SetBounds(95, 20, 250, 30);

            Label surname = new Label
            {
                Text = "Фамилия:"
            };
            surname.SetBounds(10, 55, 80, 30);

            Label surnameInfo = new Label
            {
                Text = secretary.Surname
            };
            surnameInfo.SetBounds(95, 55, 250, 30);

            Label age = new Label
            {
                Text = "Возраст:"
            };
            age.SetBounds(10, 90, 80, 30);

            Label ageInfo = new Label
            {
                Text = secretary.Age.ToString()
            };
            ageInfo.SetBounds(95, 90, 250, 30);

            Label salary = new Label
            {
                Text = "Зарплата:"
            };
            salary.SetBounds(10, 125, 80, 30);

            Label salaryInfo = new Label
            {
                Text = secretary.Salary.ToString()
            };
            salaryInfo.SetBounds(95, 125, 250, 30);

            Label workplace = new Label
            {
                Text = "Место работы:"
            };
            workplace.SetBounds(10, 160, 80, 30);

            Label workplaceInfo = new Label
            {
                Text = secretary.Company.ToString()
            };
            workplaceInfo.SetBounds(95, 160, 250, 30);

            Label documents = new Label
            {
                Text = "Документы:"
            };

            ComboBox documentsBox = new ComboBox
            {
                Name = "documentsBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = secretary.PendingDocuments
            };
            documentsBox.SetBounds(10, 195, 220, 30);

            Button createDocumentButton = new Button
            {
                Name = "createDocumentButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Создать"
            };
            createDocumentButton.SetBounds(10, 285, 220, 30);
            createDocumentButton.Click += new EventHandler(CreateDocumentEvent);

            Button removeDocumentButton = new Button
            {
                Name = "createDocumentButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Удалить"
            };
            removeDocumentButton.SetBounds(10, 320, 220, 30);
            removeDocumentButton.Click += new EventHandler(RemoveDocumentEvent);

            Button sendCopyToArchiveButton = new Button
            {
                Name = "sendCopyToArchiveButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Отправить в архив"
            };
            sendCopyToArchiveButton.SetBounds(10, 355, 220, 30);
            sendCopyToArchiveButton.Click += new EventHandler(SendToArchiveEvent);

            Button selectDocumentButton = new Button
            {
                Name = "selectDocumentButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Просмотреть"
            };
            selectDocumentButton.SetBounds(10, 390, 220, 30);
            selectDocumentButton.Click += new EventHandler(SelectDocumentEvent);

            Controls.Add(name);
            Controls.Add(nameInfo);
            Controls.Add(surname);
            Controls.Add(surnameInfo);
            Controls.Add(age);
            Controls.Add(ageInfo);
            Controls.Add(salary);
            Controls.Add(salaryInfo);
            Controls.Add(workplace);
            Controls.Add(workplaceInfo);
            Controls.Add(documentsBox);
            Controls.Add(createDocumentButton);
            Controls.Add(removeDocumentButton);
            Controls.Add(sendCopyToArchiveButton);
            Controls.Add(selectDocumentButton);
        }

        private void CreateDocumentEvent(object sender, EventArgs e)
        {
            DocumentForm documentForm = new DocumentForm();
            documentForm.Disposed += new EventHandler(DocumentFormDisposedEvent);
            documentForm.Activate();
            documentForm.ShowDialog();
        }

        private void RemoveDocumentEvent(object sender, EventArgs e)
        {
            ComboBox documentsBox = (ComboBox)Utils.FindControl(this, "documentsBox");
            Document document = (Document)documentsBox.SelectedItem;
            if (document == null)
            {
                MessageBox.Show("Документ не выбран!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                secretary.RemoveDocument(document);
                UpdateDocumentsBox();
            }
        }

        private void SendToArchiveEvent(object sender, EventArgs e)
        {
            ComboBox documentsBox = (ComboBox)Utils.FindControl(this, "documentsBox");
            Document document = (Document)documentsBox.SelectedItem;
            if (document == null)
            {
                MessageBox.Show("Документ не выбран!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                secretary.SendDocumentToArchive(document);
                ChanceryInfoForm chanceryInfoForm = (ChanceryInfoForm) Application.OpenForms["ChanceryInfoForm"];
                chanceryInfoForm.UpdateArchiveBox();
            }
        }

        private void SelectDocumentEvent(object sender, EventArgs e)
        {
            ComboBox documentsBox = (ComboBox)Utils.FindControl(this, "documentsBox");
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

        private void DocumentFormDisposedEvent(object sender, EventArgs e)
        {
            DocumentForm documentForm = (DocumentForm)sender;
            if (documentForm.Correct)
            {
                DocumentType type = documentForm.Type;
                int documentCode = documentForm.DocumentCode;
                string title = documentForm.Title;
                string text = documentForm.Content;
                Company receiver = documentForm.Receiver;
                Document document = secretary.CreateDocument(type, documentCode, title, text, receiver);
                secretary.SendDocument(document);
            }
        }

        public void UpdateDocumentsBox()
        {
            ComboBox documentsBox = (ComboBox)Utils.FindControl(this, "documentsBox");
            documentsBox.DataSource = null;
            documentsBox.Items.Clear();
            documentsBox.DataSource = secretary.PendingDocuments;
        }
    
    }
}
