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
    public class DocumentInfoForm : Form
    {
        private Document document;

        public DocumentInfoForm(Document document)
        {
            this.document = document;
            InitializeForm();
        }

        private void InitializeForm()
        {
            Width = 500;
            Height = 500;
            Name = "DocumentInfoForm";
            Text = "Document Info Form";
            BackColor = Color.MediumTurquoise;
            StartPosition = FormStartPosition.CenterScreen;

            Label title = new Label
            {
                Text = "Название:"
            };
            title.SetBounds(10, 20, 80, 30);

            Label type = new Label
            {
                Text = "Тип:"
            };
            type.SetBounds(10, 55, 80, 30);

            Label documentCode = new Label
            {
                Text = "Код:"
            };
            documentCode.SetBounds(10, 90, 80, 30);

            Label time = new Label
            {
                Text = "Время создания:"
            };
            time.SetBounds(10, 125, 80, 30);

            Label receiver = new Label
            {
                Text = "Получатель:"
            };
            receiver.SetBounds(10, 160, 80, 30);

            Label sender = new Label
            {
                Text = "Отправитель:"
            };
            sender.SetBounds(10, 195, 80, 30);

            Label text = new Label
            {
                Text = "Содержание:"
            };
            text.SetBounds(10, 230, 80, 30);

            Button editButton = new Button
            {
                Name = "editButton",
                Text = "Редактировать",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204)))
            };
            editButton.SetBounds(10, 340, 235, 30);
            editButton.Click += new EventHandler(EditEvent);

            Controls.Add(title);
            Controls.Add(type);
            Controls.Add(documentCode);
            Controls.Add(receiver);
            Controls.Add(sender);
            Controls.Add(time);
            Controls.Add(text);
            Controls.Add(editButton);
            AddInfoForms();
        }

        private void AddInfoForms()
        {
            Label titleInfo = new Label
            {
                Name = "titleInfo",
                Text = document.Title
            };
            titleInfo.SetBounds(95, 20, 150, 30);

            Label typeInfo = new Label
            {
                Name = "typeInfo",
                Text = document.Type.type
            };
            typeInfo.SetBounds(95, 55, 150, 30);

            Label documentCodeInfo = new Label
            {
                Name = "documentCodeInfo",
                Text = document.DocumentCode.ToString()
            };
            documentCodeInfo.SetBounds(95, 90, 150, 30);

            Label timeInfo = new Label
            {
                Name = "timeInfo",
                Text = document.CreationDate.ToShortDateString()
            };
            timeInfo.SetBounds(95, 125, 150, 30);

            Label receiverInfo = new Label
            {
                Name = "receiverInfo",
                Text = document.Receiver.ToString()
            };
            receiverInfo.SetBounds(95, 160, 150, 30);

            Label senderInfo = new Label
            {
                Name = "senderInfo",
                Text = document.Sender.ToString()
            };
            senderInfo.SetBounds(95, 195, 150, 30);

            Label textInfo = new Label
            {
                Name = "textInfo",
                Text = document.Text
            };
            textInfo.SetBounds(10, 265, 235, 75);

            Controls.Add(titleInfo);
            Controls.Add(typeInfo);
            Controls.Add(documentCodeInfo);
            Controls.Add(timeInfo);
            Controls.Add(receiverInfo);
            Controls.Add(senderInfo);
            Controls.Add(textInfo);
        }

        private void AddEditForms()
        {

        }

        private void EditEvent(object sender, EventArgs e)
        {

        }

        private void CommitEditEvent(object sender, EventArgs e)
        {

        }
    }
}
