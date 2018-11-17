using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class MainSecretary : Employee
    {
        public MainSecretary() : base() { }

        public MainSecretary(Person person, Company company, int salary) : base(person, company, salary) { }

        public void RedirectPendingDocument(Secretary secretary, Document document)
        {
            Chancery chancery = Company.Chancery;
            chancery.PendingDocuments.Remove(document);
            secretary.PendingDocuments.Add(document);    
        }

        public void RedirectPendingDocument(Document document)
        {
            Chancery chancery = Company.Chancery;
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
                    secretary.PendingDocuments.Add(document);
                    break;
                }
            }
        }

        public new void Persist()
        {
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            EmployeeId = DataLists.GenerateMainSecretaryId();
            Person person = dataLists.Persons.Find((p) => p.Id == Id);
            person = this;
            //Добавляем управляющего в секретариат
            Company.Chancery.MainSecretary = this;
            dataLists.MainSecretaries.Add(this);
        }

        public override void Quit()
        {
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            Working = false;
            dataLists.MainSecretaries.Remove(this);
            Person person = dataLists.Persons.Find((p) => p.Id == Id);
            person = this;
            Company.Chancery.MainSecretary = null;
        }
    }
}
