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
    public class CompanyInfoForm : Form
    {
        private Company company;
        private Director director;
        private bool directorChanged;

        public CompanyInfoForm(Company company)
        {
            this.company = company;
            director = company.Director;
            InitializeForm();
        }

        private void InitializeForm()
        {
            Width = 600;
            Height = 400;
            Name = "CompanyInfoForm";
            Text = "Company Info Form";
            BackColor = Color.MediumSpringGreen;
            StartPosition = FormStartPosition.CenterScreen;

            Label name = new Label
            {
                Text = "Название:",
            };
            name.SetBounds(10, 20, 80, 30);

            Label address = new Label
            {
                Text = "Адрес:",
            };
            address.SetBounds(10, 55, 80, 30);

            Label type = new Label
            {
                Text = "Тип:",
            };
            type.SetBounds(10, 90, 80, 30);

            Label director = new Label
            {
                Text = "Директор:"
            };
            director.SetBounds(10, 125, 80, 30);

            Button openChanceryButton = new Button
            {
                Name = "openChanceryButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Войти в секретариат"
            };
            openChanceryButton.SetBounds(10, 250, 235, 30);
            openChanceryButton.Click += new EventHandler(OpenChanceryEvent);

            Controls.Add(name);
            Controls.Add(address);
            Controls.Add(type);
            Controls.Add(director);
            Controls.Add(openChanceryButton);
            AddInfoForms();
        }

        private void AddInfoForms()
        {
            Label nameInfo = new Label
            {
                Name = "nameInfo",
                Text = company.Name
            };
            nameInfo.SetBounds(95, 20, 150, 30);

            Label addressInfo = new Label
            {
                Name = "addressInfo",
                Text = company.Address
            };
            addressInfo.SetBounds(95, 55, 150, 30);

            Label typeInfo = new Label
            {
                Name = "typeInfo",
                Text = company.Type.ToString()
            };
            typeInfo.SetBounds(95, 90, 150, 30);

            Label directorInfo = new Label
            {
                Name = "directorInfo",
                Text = company.Director.ToString()
            };
            directorInfo.SetBounds(95, 125, 150, 30);

            Button directorInfoButton = new Button
            {
                Name = "directorInfoButton",
                BackColor = Color.Tomato,
                TabStop = false,
                FlatStyle = FlatStyle.Flat,
                BackgroundImage = Properties.Resources.search,
                BackgroundImageLayout = ImageLayout.Zoom
            };
            directorInfoButton.SetBounds(250, 125, 20, 20);
            directorInfoButton.Click += new EventHandler(DirectorInfoEvent);

            Button editButton = new Button
            {
                Name = "editButton",
                Text = "Редактировать",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204)))
            };
            editButton.SetBounds(10, 285, 235, 30);
            editButton.Click += new EventHandler(EditEvent);

            Controls.Add(nameInfo);
            Controls.Add(addressInfo);
            Controls.Add(typeInfo);
            Controls.Add(directorInfo);
            Controls.Add(editButton);
            Controls.Add(directorInfoButton);
        }

        private void AddEditForms()
        {
            TextBox nameBox = new TextBox
            {
                Name = "nameBox",
                Text = company.Name
            };
            nameBox.SetBounds(95, 20, 150, 30);

            TextBox addressBox = new TextBox
            {
                Name = "addressBox",
                Text = company.Address
            };
            addressBox.SetBounds(95, 55, 150, 30);

            ComboBox typeBox = new ComboBox
            {
                Name = "typeBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = DataStorage.GetInstance().DataLists.CompanyTypes
            };
            typeBox.SetBounds(95, 90, 150, 30);
            Controls.Add(typeBox);
            typeBox.SelectedIndex = typeBox.FindString(company.Type.ToString());

            Button directorChooserButton = new Button
            {
                Name = "directorChooserButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Сменить директора"
            };
            directorChooserButton.SetBounds(10, 160, 235, 30);
            directorChooserButton.Click += new EventHandler(DirectorChooseEvent);

            ComboBox personsBox = new ComboBox
            {
                Name = "personsBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = DataStorage.GetInstance().DataLists.Persons.Where(p => !p.Working).ToList()
            };
            personsBox.SetBounds(10, 195, 235, 30);

            Button commitEditButton = new Button
            {
                Name = "commitEditButon",
                Text = "Подтвердить",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204)))
            };
            commitEditButton.SetBounds(10, 285, 235, 30);
            commitEditButton.Click += new EventHandler(CommitEditEvent);

            Controls.Add(nameBox);
            Controls.Add(addressBox);
            Controls.Add(typeBox);
            Controls.Add(directorChooserButton);
            Controls.Add(personsBox);
            Controls.Add(commitEditButton);
        }

        private void OpenChanceryEvent(object sender, EventArgs e)
        {
            Chancery chancery = company.Chancery;
            ChanceryInfoForm chanceryInfoForm = new ChanceryInfoForm(chancery);
            chanceryInfoForm.Activate();
            chanceryInfoForm.ShowDialog();
        }

        private void DirectorChooseEvent(object sender, EventArgs e)
        {
            ComboBox personsBox = (ComboBox)Utils.FindControl(this, "personsBox");
            if (personsBox.SelectedItem == null)
            {
                MessageBox.Show("Персона не выбрана!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Person person = (Person)personsBox.SelectedItem;
                DirectorForm employeeForm = new DirectorForm(person);
                employeeForm.Disposed += new EventHandler(DirectorDisposeEvent);
                employeeForm.Activate();
                employeeForm.ShowDialog();
            }
        }

        private void DirectorDisposeEvent(object sender, EventArgs e)
        {         
            DirectorForm form = (DirectorForm)sender;
            if (form.CorrectOnClose)
            {
                Person person = form.Person;
                director = new Director(person, company, form.Signature, form.Salary)
                {
                    PendingDocuments = new List<Document>()
                };
                directorChanged = true;
            }
        }

        private void DirectorInfoEvent(object sender, EventArgs e)
        {
            DirectorInfoForm form = new DirectorInfoForm(company.Director);
            form.Activate();
            form.Show();
        }

        private void EditEvent(object sender, EventArgs e)
        {
            Control nameInfo = Utils.FindControl(this, "nameInfo");
            Control addressInfo = Utils.FindControl(this, "addressInfo");
            Control typeInfo = Utils.FindControl(this, "typeInfo");
            Control directorInfo = Utils.FindControl(this, "directorInfo");
            Control directorInfoButton = Utils.FindControl(this, "directorInfoButton");
            Button openChanceryButton = (Button)Utils.FindControl(this, "openChanceryButton");

            Controls.Remove(nameInfo);
            Controls.Remove(addressInfo);
            Controls.Remove(typeInfo);
            Controls.Remove(directorInfo);
            Controls.Remove(directorInfoButton);
            Controls.Remove((Control)sender);
            openChanceryButton.Enabled = false;
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
                    if (string.IsNullOrWhiteSpace(c.Text) || director == null)
                    {
                        MessageBox.Show("Одно из полей пустое!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        filled = false;
                        break;
                    }
                }
            }
            if (filled)
            {
                Control nameBox = Utils.FindControl(this, "nameBox");
                Control addressBox = Utils.FindControl(this, "addressBox");
                Control personsBox = Utils.FindControl(this, "personsBox");
                Control typeBox = Utils.FindControl(this, "typeBox");
                Control directorBox = Utils.FindControl(this, "directorBox");
                Control directorChooserButton = Utils.FindControl(this, "directorChooserButton");
                Button openChanceryButton = (Button)Utils.FindControl(this, "openChanceryButton");
                try
                {
                    if (directorChanged)
                    {
                        //Удаляем старого директора
                        company.Director.Delete();
                        company.Director = null;
                        //Добавляем нового директора
                        director.Persist();
                        company.Director = director;
                    }
                    //Обновляем компанию
                    string name = nameBox.Text;
                    string address = addressBox.Text;
                    CompanyType type = (CompanyType)((ComboBox)typeBox).SelectedItem;
                    company.Name = name;
                    company.Address = address;
                    company.Type = type;
                    DocumentManagementForm form = (DocumentManagementForm)Application.OpenForms["DocumentManagementForm"];
                    form.UpdateCompaniesBox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Неправильно введенные данные!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    correct = false;
                }
                if (correct)
                {
                    Controls.Remove(nameBox);
                    Controls.Remove(addressBox);
                    Controls.Remove(typeBox);
                    Controls.Remove(personsBox);
                    Controls.Remove(directorBox);
                    Controls.Remove(directorChooserButton);
                    Controls.Remove((Control)sender);
                    openChanceryButton.Enabled = true;
                    directorChanged = false;
                    AddInfoForms();
                }
            }
        }
    }
}