using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class Director : Employee
    {
        public byte[] Signature { get; set; }
        public List<Document> PendingDocuments { get; set; }

        public Director() : base() { }

        public Director(Person person, Company company, byte[] signature, int salary) : base(person, company, salary)
        {
            Signature = signature;
        }

        public void ConfirmDocument(Document document)
        {
            if(document.Sender.Equals(Company))
            {
                if (document.Receiver.Equals(Company))
                {
                    SendCopyToArchive(document);
                }
                else
                {
                    Director receiverDirector = document.Receiver.Director;
                    receiverDirector.PendingDocuments.Add(document);
                }
            }
            else
            {
                if(document.SenderConfirm == true)
                {
                    SendCopyToArchive(document);
                }
            }
            PendingDocuments.Remove(document);
        }

        public void RejectDocument(Document document)
        {
            if (document.Sender.Equals(Company))
            {
                Chancery chancery = Company.Chancery;
                chancery.PendingDocuments.Add(document);
            }
            PendingDocuments.Remove(document);
        }

        public void SendCopyToArchive(Document document)
        {
            Document copy = (Document) document.Clone();
            Chancery chancery = Company.Chancery;
            chancery.Archive.Add(copy);
        }

        public new void Persist()
        {
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            EmployeeId = DataLists.GenerateDirectorId();
            Person person = dataLists.Persons.Find((p) => p.Id == Id);
            person = this;
            dataLists.Directors.Add(this);
        }

        public override void Quit()
        {
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            foreach (Document document in PendingDocuments)
            {
                document.Delete();
            }
            PendingDocuments = null;
            Working = false;
            dataLists.Directors.Remove(this);
            Person person = dataLists.Persons.Find((p) => p.Id == Id);
            person = this;
            Company.Director = null;
        }
    }
}
