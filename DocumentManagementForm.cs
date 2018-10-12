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
    public partial class DocumentManagementForm : Form
    {
        public DocumentManagementForm()
        {
            InitializeComponent();
            UpdateCompaniesBox();
            UpdatePersonsBox();
            /*
            CompanyType type1 = new CompanyType("АО");
            CompanyType type2 = new CompanyType("ЗАО");
            CompanyType type3 = new CompanyType("ООО");
            CompanyType type4 = new CompanyType("Корпорация");
            CompanyType type5 = new CompanyType("Муниципалитет");
            CompanyType type6 = new CompanyType("Ассоциация");
            SqlCompanyTypeDAO.AddCompanyType(type1);
            SqlCompanyTypeDAO.AddCompanyType(type2);
            SqlCompanyTypeDAO.AddCompanyType(type3);
            SqlCompanyTypeDAO.AddCompanyType(type4);
            SqlCompanyTypeDAO.AddCompanyType(type5);
            SqlCompanyTypeDAO.AddCompanyType(type6);

            DocumentType dtype1 = new DocumentType("Бизнес план");
            DocumentType dtype2 = new DocumentType("Справка");
            DocumentType dtype3 = new DocumentType("Акт");
            DocumentType dtype4 = new DocumentType("Приказ");
            DocumentType dtype5 = new DocumentType("Докладная записка");
            SqlDocumentTypeDAO.AddDocumentType(dtype1);
            SqlDocumentTypeDAO.AddDocumentType(dtype2);
            SqlDocumentTypeDAO.AddDocumentType(dtype3);
            SqlDocumentTypeDAO.AddDocumentType(dtype4);
            SqlDocumentTypeDAO.AddDocumentType(dtype5);
            */

        }

        private void AddCompany_Click(object sender, EventArgs e)
        {
             CompanyForm companyForm = new CompanyForm();
             companyForm.Activate();
             companyForm.ShowDialog();
        }

        private void SelectCompany_Click(object sender, EventArgs e)
        {
            ComboBox companiesBox = (ComboBox) Utils.FindControl(this, "companiesBox");
            Company company = (Company) companiesBox.SelectedItem; 
            if(company == null)
            {
                MessageBox.Show("Организация не выбрана!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                CompanyInfoForm companyInfoForm = new CompanyInfoForm(company);
                companyInfoForm.Activate();
                companyInfoForm.ShowDialog();
            }
        }

        private void RemoveCompany_Click(object sender, EventArgs e)
        {
            ComboBox companiesBox = (ComboBox) Utils.FindControl(this, "companiesBox");
            Company company = (Company) companiesBox.SelectedItem;
            if (company == null)
            {
                MessageBox.Show("Организация не выбрана!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DeleteCompany(company);
                UpdateCompaniesBox();
            }
        }

        private void AddPerson_Click(object sender, EventArgs e)
        {
            PersonForm personForm = new PersonForm();
            personForm.Activate();
            personForm.ShowDialog();
        }

        private void SelectPerson_Click(object sender, EventArgs e)
        {
            ComboBox personsBox = (ComboBox) Utils.FindControl(this, "personsBox");
            Person person = (Person) personsBox.SelectedItem;
            if (person == null)
            {
                MessageBox.Show("Персона не выбрана!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                PersonInfoForm personInfoForm = new PersonInfoForm(person);
                personInfoForm.Activate();
                personInfoForm.ShowDialog();
            }
        }

        private void RemovePerson_Click(object sender, EventArgs e)
        {
            ComboBox personsBox = (ComboBox) Utils.FindControl(this, "personsBox");
            Person person = (Person) personsBox.SelectedItem;
            if (person == null)
            {
                MessageBox.Show("Персона не выбрана!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                List<Company> companies = SqlCompanyDAO.GetAllCompanies();
                foreach(Company c in companies)
                {
                    if(c.Director.Id == person.Id)
                    {
                        DeleteCompany(c);
                    }
                }
                SqlPersonDAO.DeletePerson(person.Id);
                UpdatePersonsBox();
                UpdateCompaniesBox();
            }
        }

        public void UpdatePersonsBox()
        {
            personsBox.DataSource = null;
            personsBox.Items.Clear();
            personsBox.DataSource = SqlPersonDAO.GetAllPersons();
        }

        public void UpdateCompaniesBox()
        {
            companiesBox.DataSource = null;
            companiesBox.Items.Clear();
            companiesBox.DataSource = SqlCompanyDAO.GetAllCompanies();
        }

        private void DeleteCompany(Company company)
        {
            Director director = company.Director;
            director.Working = false;
            SqlPersonDAO.UpdatePerson(director);

            Chancery chancery = company.CompanyChancery;
            MainSecretary mainSecretary = chancery.MainSecretary;
            if (mainSecretary != null)
            {
                mainSecretary.Working = false;
                SqlPersonDAO.UpdatePerson(mainSecretary);
            }
            List<Secretary> secretaries = chancery.Secretaries;
            foreach (Secretary secretary in secretaries)
            {
                secretary.Working = false;
                SqlPersonDAO.UpdatePerson(secretary);
            }
            SqlCompanyDAO.DeleteCompany(company.Id);
        }
    } 
}

