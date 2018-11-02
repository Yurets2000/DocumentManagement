using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DocumentManagement
{
    public class DocumentEditForm : Form
    {
        private Secretary secretary;
        private Document document;

        public DocumentEditForm(Secretary secretary, Document document)
        {
            this.secretary = secretary;
            this.document = document;
            InitializeForm();
        }

        private void InitializeForm()
        {
            Width = 500;
            Height = 500;
            Name = "DocumentEditForm";
            Text = "Document Edit Form";
            BackColor = Color.MediumTurquoise;
            StartPosition = FormStartPosition.CenterScreen;

            Label title = new Label
            {
                Text = "Название:"
            };
            title.SetBounds(10, 20, 80, 30);

            TextBox titleBox = new TextBox
            {
                Name = "titleBox",
                Text = document.Title
            };
            titleBox.SetBounds(95, 20, 150, 30);

            Label type = new Label
            {
                Text = "Тип:"
            };
            type.SetBounds(10, 55, 80, 30);

            ComboBox typeBox = new ComboBox
            {
                Name = "typeBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = SqlDocumentType.GetAllDocumentTypes()
            };
            typeBox.SetBounds(95, 55, 150, 30);

            Label documentCode = new Label
            {
                Text = "Код:"
            };
            documentCode.SetBounds(10, 90, 80, 30);

            TextBox documentCodeBox = new TextBox
            {
                Name = "documentCodeBox",
                Text = document.DocumentCode.ToString()
            };
            documentCodeBox.SetBounds(95, 90, 150, 30);

            Label time = new Label
            {
                Text = "Время создания:"
            };
            time.SetBounds(10, 125, 80, 30);

            Label timeInfo = new Label
            {
                Name = "timeInfo",
                Text = document.CreationDate.ToShortDateString()
            };
            timeInfo.SetBounds(95, 125, 150, 30);

            Label receiver = new Label
            {
                Text = "Получатель:"
            };
            receiver.SetBounds(10, 160, 80, 30);

            ComboBox receiverBox = new ComboBox
            {
                Name = "receiverBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = SqlCompany.GetAllCompanies()
            };
            receiverBox.SetBounds(95, 160, 150, 30);

            Label sender = new Label
            {
                Text = "Отправитель:"
            };
            sender.SetBounds(10, 195, 80, 30);

            Label senderInfo = new Label
            {
                Name = "senderInfo",
                Text = document.Sender.ToString()
            };
            senderInfo.SetBounds(95, 195, 150, 30);

            Label text = new Label
            {
                Text = "Содержание:"
            };
            text.SetBounds(10, 230, 80, 30);

            TextBox textBox = new TextBox
            {
                Name = "textBox",
                Multiline = true,
                Text = document.Text
            };
            textBox.SetBounds(10, 265, 235, 100);

            Button commitButton = new Button
            {
                Name = "commitButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Подтвердить"
            };
            commitButton.SetBounds(10, 370, 235, 30);
            commitButton.Click += new EventHandler(CommitEvent);

            Controls.Add(title);
            Controls.Add(titleBox);
            Controls.Add(type);
            Controls.Add(typeBox);
            Controls.Add(documentCode);
            Controls.Add(documentCodeBox);
            Controls.Add(time);
            Controls.Add(timeInfo);
            Controls.Add(receiver);
            Controls.Add(receiverBox);
            Controls.Add(sender);
            Controls.Add(senderInfo);
            Controls.Add(text);
            Controls.Add(textBox);
            Controls.Add(commitButton);
        }

        private void CommitEvent(object sender, EventArgs e)
        {
            ComboBox receiverBox = (ComboBox)Utils.FindControl(this, "receiverBox");
            ComboBox typeBox = (ComboBox)Utils.FindControl(this, "typeBox");
            Control documentCodeBox = Utils.FindControl(this, "documentCodeBox");
            Control textBox = Utils.FindControl(this, "textBox");
            Control titleBox = Utils.FindControl(this, "titleBox");

            bool filled = receiverBox.SelectedItem != null &&
                          typeBox.SelectedItem != null &&
                          !String.IsNullOrWhiteSpace(documentCodeBox.Text) &&
                          !String.IsNullOrWhiteSpace(documentCodeBox.Text);

            if (filled)
            {
                string title = titleBox.Text;
                DocumentType type = (DocumentType)typeBox.SelectedItem;
                string documentCodeString = documentCodeBox.Text;
                string text = textBox.Text;
                Company receiver = (Company)receiverBox.SelectedItem;

                bool titleValidated = DocumentFormValidator.ValidateTitle(title);
                bool documentCodeValidated = DocumentFormValidator.ValidateDocumentCode(documentCodeString);

                if (titleValidated && documentCodeValidated)
                {                    
                    document.Type = type;
                    document.DocumentCode = int.Parse(documentCodeString);
                    document.Title = title;
                    document.Text = text;
                    document.Receiver = receiver;
                    secretary.EditDocument(document);
                    Dispose();
                    Close();
                }
                else
                {
                    MessageBox.Show("Нерпавильно введенные данные!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Одно из полей пустое!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
