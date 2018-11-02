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
            SqlCompanyType.AddCompanyType(type1);
            SqlCompanyType.AddCompanyType(type2);
            SqlCompanyType.AddCompanyType(type3);
            SqlCompanyType.AddCompanyType(type4);
            SqlCompanyType.AddCompanyType(type5);
            SqlCompanyType.AddCompanyType(type6);

            DocumentType dtype2 = new DocumentType("Справка");
            DocumentType dtype3 = new DocumentType("Акт");
            DocumentType dtype4 = new DocumentType("Приказ");
            DocumentType dtype5 = new DocumentType("Докладная записка");
            SqlDocumentType.AddDocumentType(dtype1);
            SqlDocumentType.AddDocumentType(dtype2);
            SqlDocumentType.AddDocumentType(dtype3);
            SqlDocumentType.AddDocumentType(dtype4);
            SqlDocumentType.AddDocumentType(dtype5);
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
                company.Delete();
                company = null;
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
                person.Delete();
                person = null;
                UpdatePersonsBox();
                UpdateCompaniesBox();
            }
        }

        private void DocumentManagementForm_Closing(object sender, FormClosingEventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Save changes?", "Question", MessageBoxButtons.YesNo);

            if (dlg == DialogResult.Yes)
            {
                SaveChanges();
                e.Cancel = false;          
            }
            if (dlg == DialogResult.No)
            {
                e.Cancel = false;
            }
        }

        public void SaveChanges()
        {

        }

        public void UpdatePersonsBox()
        {
            personsBox.DataSource = null;
            personsBox.Items.Clear();
            personsBox.DataSource = SqlPerson.GetAllPersons();
        }

        public void UpdateCompaniesBox()
        {
            companiesBox.DataSource = null;
            companiesBox.Items.Clear();
            companiesBox.DataSource = SqlCompany.GetAllCompanies();
        }
    } 
}

