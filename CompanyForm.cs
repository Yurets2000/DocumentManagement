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
    public class CompanyForm : Form
    {
        private Director _director;

        public CompanyForm()
        {
            InitializeForm();
        }

        private void InitializeForm()
        {
            Width = 600;
            Height = 400;
            Name = "CompanyForm";
            Text = "Company Adder Form";
            BackColor = Color.Orange;
            StartPosition = FormStartPosition.CenterScreen;

            Label name = new Label
            {
                Text = "Название:"
            };
            name.SetBounds(10, 20, 80, 30);

            TextBox nameBox = new TextBox
            {
                Name = "nameBox"
            };
            nameBox.SetBounds(95, 20, 150, 30);

            Label address = new Label
            {
                Text = "Адрес:"
            };
            address.SetBounds(10, 55, 80, 30);

            TextBox addressBox = new TextBox
            {
                Name = "addressBox"
            };
            addressBox.SetBounds(95, 55, 150, 30);

            Label type = new Label
            {
                Text = "Тип:"
            };
            type.SetBounds(10, 90, 80, 30);

            ComboBox typeBox = new ComboBox
            {
                Name = "typeBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = SqlCompanyTypeDAO.GetAllCompanyTypes()
            };
            typeBox.SetBounds(95, 90, 150, 30);

            Button directorChooseButton = new Button
            {
                Name = "directorChooseButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Выбрать директора"
            };
            directorChooseButton.SetBounds(10, 125, 235, 30);
            directorChooseButton.Click += new EventHandler(DirectorChooseButton_Click);

            ComboBox personsBox = new ComboBox
            {
                Name = "personsBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = SqlPersonDAO.GetAllPersons().Where(p => !p.Working).ToList()
            };
            personsBox.SetBounds(10, 160, 235, 30);

            Button addCompanyButton = new Button
            {
                Name = "addCompanyButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Добавить организацию"
            };
            addCompanyButton.SetBounds(10, 250, 235, 30);
            addCompanyButton.Click += new EventHandler(AddCompanyButton_Click);

            Controls.Add(name);
            Controls.Add(nameBox);
            Controls.Add(address);
            Controls.Add(addressBox);
            Controls.Add(type);
            Controls.Add(typeBox);
            Controls.Add(directorChooseButton);
            Controls.Add(personsBox);
            Controls.Add(addCompanyButton);
        }

        private void DirectorChooseButton_Click(object sender, EventArgs e)
        {
            ComboBox personsBox = (ComboBox)Utils.FindControl(this, "personsBox");
            if (personsBox.SelectedItem == null)
            {
                MessageBox.Show("Персона не выбрана!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Person person = (Person)personsBox.SelectedItem;
                EmployeeForm directorForm = new EmployeeForm(person);
                directorForm.Disposed += new EventHandler(DirectorFormDisposeEvent);
                directorForm.Activate();
                directorForm.ShowDialog();
            }
        }

        private void DirectorFormDisposeEvent(object sender, EventArgs e)
        {
            EmployeeForm form = (EmployeeForm)sender;
            if (form.CorrectOnClose)
            {
                Person person = form.Person;
                _director = new Director(person, null, form.Salary);
            }
        }

        private void AddCompanyButton_Click(object sender, EventArgs e)
        {
            bool filled = Utils.CheckFormFilled(this);
            if (filled && _director != null)
            {
                String name = Utils.FindControl(this, "nameBox").Text;
                String address = Utils.FindControl(this, "addressBox").Text;
                CompanyType type = (CompanyType)((ComboBox)Utils.FindControl(this, "typeBox")).SelectedValue;

                bool nameValidated = CompanyFormValidator.ValidateName(name);
                bool addressValidated = CompanyFormValidator.ValidateAddress(address);
                if (nameValidated && addressValidated)
                {
                    Chancery chancery = new Chancery();
                    Company company = new Company(_director, type, chancery, name, address);
                    //Добавляем директора в БД
                    int directorId = SqlDirectorDAO.AddDirector(_director);
                    _director.EmployeeId = directorId;
                    SqlPersonDAO.UpdatePerson(_director);
                    //Добавляем секретариат в БД
                    int chanceryId = SqlChanceryDAO.AddChancery(chancery);
                    chancery.Id = chanceryId;
                    //Добавляем компанию в БД
                    int companyId = SqlCompanyDAO.AddCompany(company);
                    company.Id = companyId;
                    //Обновляем поля в объектах Директор и Секретариат
                    _director.Company = company;
                    chancery.ChanceryCompany = company;
                    SqlDirectorDAO.UpdateDirector(_director);
                    SqlChanceryDAO.UpdateChancery(chancery);
                    DocumentManagementForm form = (DocumentManagementForm)Application.OpenForms["DocumentManagementForm"];
                    form.UpdateCompaniesBox();
                    Close();
                    Dispose();
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

    }
}
