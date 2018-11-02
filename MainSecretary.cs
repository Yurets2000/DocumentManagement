using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class MainSecretary : Employee
    {
        public bool InitMainSecretary { get; set; }

        public MainSecretary() : base() { }

        public MainSecretary(Person person, Company company, int salary) : base(person, company, salary) { }

        public void RedirectPendingDocument(Secretary secretary, Document document)
        {
            Chancery chancery = Company.CompanyChancery;
            chancery.PendingDocuments.Remove(document);
            SqlPendingDocuments.DeletePendingDocument(chancery.Id, document.Id);
            secretary.PendingDocuments.Add(document);
            SqlSecretaryPendingDocuments.AddPendingDocument(secretary.EmployeeId, document.Id);         
        }

        public void RedirectPendingDocument(Document document)
        {
            Chancery chancery = Company.CompanyChancery;
            int maxDocuments = 0;
            foreach (Secretary secretary in chancery.Secretaries)
            {
                if(secretary.PendingDocuments.Count > maxDocuments)
                {
                    maxDocuments = secretary.PendingDocuments.Count;
                }
            }
            foreach(Secretary secretary in chancery.Secretaries)
            {
                if(secretary.PendingDocuments.Count == maxDocuments)
                {
                    chancery.PendingDocuments.Remove(document);
                    SqlPendingDocuments.DeletePendingDocument(chancery.Id, document.Id);
                    secretary.PendingDocuments.Add(document);
                    SqlSecretaryPendingDocuments.AddPendingDocument(secretary.EmployeeId, document.Id);
                    break;
                }
            }
        }

        public static MainSecretary GetMainSecretary(int id)
        {
            return SqlMainSecretary.GetMainSecretary(id);
        }

        public new void Persist()
        {
            EmployeeId = SqlMainSecretary.AddMainSecretary(this);
            SqlPerson.UpdatePerson(this);
            //Добавляем управляющего в секретариат
            Company.CompanyChancery.MainSecretary = this;
        }

        public new void Update()
        {
            SqlMainSecretary.UpdateMainSecretary(this);
        }

        public override void Quit()
        {
            Working = false;
            SqlMainSecretary.DeleteMainSecretary(EmployeeId);
            SqlPerson.UpdatePerson(this);
        }
    }
}
