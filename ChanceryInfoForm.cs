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
    public class ChanceryInfoForm : Form
    {
        private Chancery chancery;
        private MainSecretary mainSecretary;
        private bool mainSecretaryChanged;

        public ChanceryInfoForm(Chancery chancery)
        {
            this.chancery = chancery;
            InitializeForm();
        }

        private void InitializeForm()
        {
            Width = 900;
            Height = 400;
            Name = "ChanceryInfoForm";
            Text = "Chancery Info Form";
            BackColor = Color.MediumTurquoise;
            StartPosition = FormStartPosition.CenterScreen;

            Label secretaries = new Label
            {
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Список секретарей"
            };
            secretaries.SetBounds(10, 20, 150, 30);

            Label mainSecretary = new Label
            {
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Управляющий секретариатом"
            };
            mainSecretary.SetBounds(250, 20, 250, 30);

            ComboBox secretariesBox = new ComboBox
            {
                Name = "secretariesBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = chancery.Secretaries
            };
            secretariesBox.SetBounds(10, 55, 150, 30);

            Button removeSecretaryButton = new Button
            {
                Name = "deleteSecretaryButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Удалить"
            };
            removeSecretaryButton.SetBounds(10, 145, 150, 30);
            removeSecretaryButton.Click += new EventHandler(RemoveSecretaryEvent);

            Button selectSecretaryButton = new Button
            {
                Name = "selectSecretaryButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Выбрать"
            };
            selectSecretaryButton.SetBounds(10, 180, 150, 30);
            selectSecretaryButton.Click += new EventHandler(SelectSecretaryEvent);

            Button addSecretaryButton = new Button
            {
                Name = "addSecretaryButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Добавить"
            };
            addSecretaryButton.SetBounds(10, 215, 150, 30);
            addSecretaryButton.Click += new EventHandler(AddSecretaryEvent);

            ComboBox personsToSecretaryBox = new ComboBox
            {
                Name = "personsToSecretaryBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = SqlPersonDAO.GetAllPersons().Where(p => !p.Working).ToList()
            };
            personsToSecretaryBox.SetBounds(10, 250, 150, 30);

            Label archive = new Label
            {
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Архив"
            };
            archive.SetBounds(590, 20, 250, 30);

            Button selectArchivedDocumentButton = new Button
            {
                Name = "selectArchivedDocumentButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Выбрать документ"
            };
            selectArchivedDocumentButton.SetBounds(590, 55, 250, 30);
            selectArchivedDocumentButton.Click += new EventHandler(SelectArchivedDocumentEvent);

            ComboBox archiveBox = new ComboBox
            {
                Name = "archiveBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = chancery.Archive
            };
            archiveBox.SetBounds(590, 90, 250, 30);

            Label pendingDocuments = new Label
            {
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Документы к выполнению"
            };
            pendingDocuments.SetBounds(590, 150, 250, 30);

            Button selectPendingDocumentButton = new Button
            {
                Name = "selectPendingDocumentButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Выбрать документ"
            };
            selectPendingDocumentButton.SetBounds(590, 185, 250, 30);
            selectPendingDocumentButton.Click += new EventHandler(SelectPendingDocumentEvent);

            ComboBox pendingDocumentsBox = new ComboBox
            {
                Name = "pendingDocumentsBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = chancery.PendingDocuments
            };
            pendingDocumentsBox.SetBounds(590, 220, 250, 30);

            if (chancery.MainSecretary == null)
            {
                AddInitMainSecretaryForms();
            }
            else
            {
                AddMainSecretaryInfoForms();
            }

            Controls.Add(secretaries);
            Controls.Add(mainSecretary);
            Controls.Add(secretariesBox);
            Controls.Add(personsToSecretaryBox);
            Controls.Add(addSecretaryButton);
            Controls.Add(removeSecretaryButton);
            Controls.Add(selectSecretaryButton);
            Controls.Add(archive);
            Controls.Add(selectArchivedDocumentButton);
            Controls.Add(archiveBox);
            Controls.Add(pendingDocuments);
            Controls.Add(selectPendingDocumentButton);
            Controls.Add(pendingDocumentsBox);

        }

        private void AddInitMainSecretaryForms()
        {
            Button addMainSecretaryButton = new Button
            {
                Name = "addMainSecretaryButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Выбрать управляющего"
            };
            addMainSecretaryButton.SetBounds(250, 55, 250, 30);
            addMainSecretaryButton.Click += new EventHandler(AddMainSecretaryEvent);

            ComboBox personsToMainSecretaryBox = new ComboBox
            {
                Name = "personsToMainSecretaryBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = SqlPersonDAO.GetAllPersons().Where(p => !p.Working).ToList()
            };
            personsToMainSecretaryBox.SetBounds(250, 90, 250, 30);
            Controls.Add(addMainSecretaryButton);
            Controls.Add(personsToMainSecretaryBox);
        }

        private void AddMainSecretaryInfoForms()
        {
            Label mainSecretaryInfo = new Label
            {
                Name = "mainSecretaryInfo",
                Text = chancery.MainSecretary.ToString()
            };
            mainSecretaryInfo.SetBounds(250, 55, 250, 30);

            Button editMainSecretaryButton = new Button
            {
                Name = "editMainSecretaryButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Редактировать"
            };
            editMainSecretaryButton.SetBounds(250, 150, 250, 30);
            editMainSecretaryButton.Click += new EventHandler(EditMainSecretaryEvent);

            Controls.Add(mainSecretaryInfo);
            Controls.Add(editMainSecretaryButton);
        }

        private void AddMainSecretaryEditForms()
        {
            Button changeMainSecretaryButton = new Button
            {
                Name = "changeMainSecretaryButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Выбрать управляющего"
            };
            changeMainSecretaryButton.SetBounds(250, 145, 250, 30);
            changeMainSecretaryButton.Click += new EventHandler(ChangeMainSecretaryEvent);

            ComboBox personsToMainSecretaryBox = new ComboBox
            {
                Name = "personsToMainSecretaryBox",
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = SqlPersonDAO.GetAllPersons().Where(p => !p.Working).ToList()
            };
            personsToMainSecretaryBox.SetBounds(250, 180, 250, 30);

            Button commitEditMainSecretaryButton = new Button
            {
                Name = "commitEditMainSecretaryButton",
                BackColor = Color.Tomato,
                Font = new Font("Monotype Corsiva", 14.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204))),
                Text = "Подтвердить"
            };
            commitEditMainSecretaryButton.SetBounds(250, 250, 250, 30);
            commitEditMainSecretaryButton.Click += new EventHandler(CommitEditMainSecretaryEvent);

            Controls.Add(changeMainSecretaryButton);
            Controls.Add(personsToMainSecretaryBox);
            Controls.Add(commitEditMainSecretaryButton);
        }

        private void AddSecretaryEvent(object sender, EventArgs e)
        {
            ComboBox personsBox = (ComboBox)Utils.FindControl(this, "personsToSecretaryBox");
            if (personsBox.SelectedItem == null)
            {
                MessageBox.Show("Персона не выбрана!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {  Person person = (Person)personsBox.SelectedItem;
                EmployeeForm employeeForm = new EmployeeForm(person);
                employeeForm.Disposed += new EventHandler(AddSecretaryDisposeEvent);
                employeeForm.Activate();
                employeeForm.ShowDialog();
            }
        }

        private void AddMainSecretaryEvent(object sender, EventArgs e)
        {
            ComboBox personsBox = (ComboBox)Utils.FindControl(this, "personsToMainSecretaryBox");
            if (personsBox.SelectedItem == null)
            {
                MessageBox.Show("Персона не выбрана!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Person person = (Person)personsBox.SelectedItem;
                EmployeeForm employeeForm = new EmployeeForm(person);
                employeeForm.Disposed += new EventHandler(AddMainSecretaryDisposeEvent);
                employeeForm.Activate();
                employeeForm.ShowDialog();
            }
        }

        private void EditMainSecretaryEvent(object sender, EventArgs e)
        {
            Control mainSecretaryInfo = Utils.FindControl(this, "mainSecretaryInfo");
            Controls.Remove(mainSecretaryInfo);
            Controls.Remove((Control)sender);
            AddMainSecretaryEditForms();
        }

        private void CommitEditMainSecretaryEvent(object sender, EventArgs e)
        {
            bool correct = true;
            Control changeMainSecretaryButton = Utils.FindControl(this, "changeMainSecretaryButton");
            Control personsToMainSecretaryBox = Utils.FindControl(this, "personsToMainSecretaryBox");
            try
            {
                if (mainSecretaryChanged)
                {
                    MainSecretary oldMainSecretary = chancery.MainSecretary;
                    oldMainSecretary.Working = false;
                    SqlPersonDAO.UpdatePerson(oldMainSecretary);
                    chancery.MainSecretary = null;
                    SqlChanceryDAO.UpdateChancery(chancery);
                    SqlMainSecretaryDAO.DeleteMainSecretary(oldMainSecretary.EmployeeId);
                    int mainSecretaryId = SqlMainSecretaryDAO.AddMainSecretary(mainSecretary);
                    mainSecretary.EmployeeId = mainSecretaryId;
                    SqlPersonDAO.UpdatePerson(mainSecretary);
                    chancery.MainSecretary = mainSecretary;
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Неправильно введенные данные!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                correct = false;
            }
            if (correct)
            {
                Controls.Remove(changeMainSecretaryButton);
                Controls.Remove(personsToMainSecretaryBox);
                Controls.Remove((Control)sender);
                mainSecretaryChanged = false;
                UpdateSecretariesBox();
                UpdatePersonsToSecretaryBox();
                AddMainSecretaryInfoForms();
            }
        }

        private void ChangeMainSecretaryEvent(object sender, EventArgs e)
        {
            ComboBox personsBox = (ComboBox)Utils.FindControl(this, "personsToMainSecretaryBox");
            if (personsBox.SelectedItem == null)
            {
                MessageBox.Show("Персона не выбрана!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Person person = (Person)personsBox.SelectedItem;
                EmployeeForm employeeForm = new EmployeeForm(person);
                employeeForm.Disposed += new EventHandler(ChangeMainSecretaryDisposeEvent);
                employeeForm.Activate();
                employeeForm.Show();
            }
        }

        private void RemoveSecretaryEvent(object sender, EventArgs e)
        {
            ComboBox secretariesBox = (ComboBox)Utils.FindControl(this, "secretariesBox");
            Secretary secretary = (Secretary)secretariesBox.SelectedValue;
            if (secretary == null)
            {
                MessageBox.Show("Персона не выбрана!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                chancery.Secretaries.Remove(secretary);
                secretary.Working = false;
                SqlPersonDAO.UpdatePerson(secretary);
            }
        }

        private void SelectSecretaryEvent(object sender, EventArgs e)
        {
            ComboBox secretariesBox = (ComboBox)Utils.FindControl(this, "secretariesBox");
            if (secretariesBox.SelectedItem == null)
            {
                MessageBox.Show("Персона не выбрана!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Secretary secretary = (Secretary)secretariesBox.SelectedValue;
                SecretaryInfoForm secretaryInfoForm = new SecretaryInfoForm(secretary);
                secretaryInfoForm.Activate();
                secretaryInfoForm.Show();
            }
        }

        private void SelectArchivedDocumentEvent(object sender, EventArgs e)
        {
            ComboBox archiveBox = (ComboBox)Utils.FindControl(this, "archiveBox");
            if (archiveBox.SelectedItem == null)
            {
                MessageBox.Show("Документ не выбран!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Document document = (Document)archiveBox.SelectedValue;
                DocumentInfoForm documentInfoForm = new DocumentInfoForm(document);
                documentInfoForm.Activate();
                documentInfoForm.Show();
            }
        }

        private void SelectPendingDocumentEvent(object sender, EventArgs e)
        {
            ComboBox pendingDocumentBox = (ComboBox)Utils.FindControl(this, "pendingDocumentBox");
            if (pendingDocumentBox.SelectedItem == null)
            {
                MessageBox.Show("Документ не выбран!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Document document = (Document)pendingDocumentBox.SelectedValue;
                DocumentInfoForm documentInfoForm = new DocumentInfoForm(document);
                documentInfoForm.Activate();
                documentInfoForm.Show();
            }
        }

        private void AddSecretaryDisposeEvent(object sender, EventArgs e)
        {
            //Создаем секретаря
            EmployeeForm form = (EmployeeForm)sender;
            Person person = form.Person;
            Secretary secretary = new Secretary(person, chancery.ChanceryCompany, form.Salary);
            Marker marker = new Marker(Marker.Color.BLUE);
            int markerId = SqlMarkerDAO.AddMarker(marker);
            marker.Id = markerId;
            secretary.Marker = marker;
            int secretaryId = SqlSecretaryDAO.AddSecretary(secretary);
            secretary.EmployeeId = secretaryId;
            SqlPersonDAO.UpdatePerson(secretary);
            //Добавляем секретаря в секретариат
            chancery.Secretaries.Add(secretary);
            UpdateSecretariesBox();
            UpdatePersonsToSecretaryBox();
        }

        private void AddMainSecretaryDisposeEvent(object sender, EventArgs e)
        {
            bool correct = true;
            Control addMainSecretaryButton = Utils.FindControl(this, "addMainSecretaryButton");
            Control personsToMainSecretaryBox = Utils.FindControl(this, "personsToMainSecretaryBox");
            //Создаем управляющего секретариатом
            EmployeeForm form = (EmployeeForm)sender;
            try
            {
                if (form.CorrectOnClose)
                {
                    Person person = form.Person;
                    MainSecretary mainSecretary = new MainSecretary(person, chancery.ChanceryCompany, form.Salary);
                    int mainSecretaryId = SqlMainSecretaryDAO.AddMainSecretary(mainSecretary);
                    mainSecretary.EmployeeId = mainSecretaryId;
                    SqlPersonDAO.UpdatePerson(mainSecretary);
                    //Добавляем управляющего в секретариат
                    chancery.MainSecretary = mainSecretary;
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Неправильно введенные данные!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                correct = false;
            }
            if (correct)
            {
                Controls.Remove(addMainSecretaryButton);
                Controls.Remove(personsToMainSecretaryBox);
                Controls.Remove((Control)sender);
                UpdateSecretariesBox();
                UpdatePersonsToSecretaryBox();
                AddMainSecretaryInfoForms();
            }
        }

        private void ChangeMainSecretaryDisposeEvent(object sender, EventArgs e)
        {
            EmployeeForm form = (EmployeeForm)sender;
            if (form.CorrectOnClose)
            {
                Person person = form.Person;
                mainSecretary = new MainSecretary(person, chancery.ChanceryCompany, form.Salary);
                mainSecretaryChanged = true;
            }
        }

        public void UpdateSecretariesBox()
        {
            ComboBox secretariesBox = (ComboBox)Utils.FindControl(this, "secretariesBox");
            secretariesBox.DataSource = null;
            secretariesBox.Items.Clear();
            secretariesBox.DataSource = chancery.Secretaries;
        }

        public void UpdatePersonsToMainSecretaryBox()
        {
            ComboBox personsToMainSecretaryBox = (ComboBox)Utils.FindControl(this, "personsToMainSecretaryBox");
            personsToMainSecretaryBox.DataSource = null;
            personsToMainSecretaryBox.Items.Clear();
            personsToMainSecretaryBox.DataSource = SqlPersonDAO.GetAllPersons().Where(p => !p.Working).ToList();
        }

        public void UpdatePersonsToSecretaryBox()
        {
            ComboBox personsToSecretaryBox = (ComboBox)Utils.FindControl(this, "personsToSecretaryBox");
            personsToSecretaryBox.DataSource = null;
            personsToSecretaryBox.Items.Clear();
            personsToSecretaryBox.DataSource = SqlPersonDAO.GetAllPersons().Where(p => !p.Working).ToList();
        }

        public void UpdateArchiveBox()
        {
            ComboBox archiveBox = (ComboBox)Utils.FindControl(this, "archiveBox");
            archiveBox.DataSource = null;
            archiveBox.Items.Clear();
            archiveBox.DataSource = chancery.Archive;
        }

        public void UpdatePendingDocumentsBox()
        {
            ComboBox pendingDocumentsBox = (ComboBox)Utils.FindControl(this, "pendingDocumentsBox");
            pendingDocumentsBox.DataSource = null;
            pendingDocumentsBox.Items.Clear();
            pendingDocumentsBox.DataSource = chancery.PendingDocuments;
        }
    }
}
