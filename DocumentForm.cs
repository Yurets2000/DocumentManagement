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
    public class DocumentForm : Form
    {
        public DocumentType Type { get; private set; }
        public int DocumentCode { get; private set; }
        public string Title { get; private set; }
        public Company Receiver { get; private set; }
        public string Content { get; private set; }
        public bool Correct { get; private set; }

        public DocumentForm()
        {
            InitializeForm();
        }

        private void InitializeForm()
        {
            Width = 500;
            Height = 500;
            Name = "DocumentForm";
            Text = "Document Form";
            BackColor = Color.MediumTurquoise;
            StartPosition = FormStartPosition.CenterScreen;

            Label title = new Label
            {
                Text = "Название:"
            };
            title.SetBounds(10, 20, 80, 30);

            TextBox titleBox = new TextBox
            {
                Name = "titleBox"
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
                DataSource = SqlDocumentTypeDAO.GetAllDocumentTypes()
            };
            typeBox.SetBounds(95, 55, 150, 30);

            Label documentCode = new Label
            {
                Text = "Код:"
            };
            documentCode.SetBounds(10, 90, 80, 30);

            TextBox documentCodeBox = new TextBox
            {
                Name = "documentCodeBox"
            };
            documentCodeBox.SetBounds(95, 90, 150, 30);

            Label receiver = new Label
            {
                Text = "Получатель:"
            };
            receiver.SetBounds(10, 125, 80, 30);

            ComboBox companiesBox = new ComboBox
            {
                Name = "companiesBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = SqlCompanyDAO.GetAllCompanies()
            };
            companiesBox.SetBounds(95, 125, 150, 30);

            Label text = new Label
            {
                Text = "Содержание:"
            };
            text.SetBounds(10, 155, 80, 30);

            TextBox textBox = new TextBox
            {
                Name = "textBox",
                Multiline = true
            };
            textBox.SetBounds(10, 190, 235, 100);

            Button commitButton = new Button
            {
                Name = "commitButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Добавить"
            };
            commitButton.SetBounds(10, 295, 235, 30);
            commitButton.Click += new EventHandler(CommitEvent);

            Controls.Add(title);
            Controls.Add(titleBox);
            Controls.Add(type);
            Controls.Add(typeBox);
            Controls.Add(documentCode);
            Controls.Add(documentCodeBox);
            Controls.Add(receiver);
            Controls.Add(companiesBox);
            Controls.Add(text);
            Controls.Add(textBox);
            Controls.Add(commitButton);
        }

        private void CommitEvent(object sender, EventArgs e)
        {
            bool filled = true;
            Correct = true;
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
                ComboBox companiesBox = (ComboBox)Utils.FindControl(this, "companiesBox");
                ComboBox typeBox = (ComboBox)Utils.FindControl(this, "typeBox");
                Control documentCodeBox = Utils.FindControl(this, "documentCodeBox");
                Control textBox = Utils.FindControl(this, "textBox");
                Control titleBox = Utils.FindControl(this, "titleBox");               
                try
                {
                    Title = titleBox.Text;
                    Type = (DocumentType) typeBox.SelectedItem;
                    DocumentCode = int.Parse(documentCodeBox.Text);
                    Content = textBox.Text;
                    Receiver = (Company)companiesBox.SelectedItem;
                }
                catch (Exception ex)
                {
                    Correct = false;
                    MessageBox.Show("Нерпавильно введенные данные!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);    
                }
                if (Correct)
                {
                    Close();
                }
            }
        }
    }
}
