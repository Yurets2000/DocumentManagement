using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class Director : Employee
    {
        public bool InitDirector { get; set; }
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
                    SqlDirectorPendingDocuments.AddPendingDocument(receiverDirector.EmployeeId, document.Id);
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
            SqlDirectorPendingDocuments.DeletePendingDocument(EmployeeId, document.Id);
        }

        public void RejectDocument(Document document)
        {
            if (document.Sender.Equals(Company))
            {
                Chancery chancery = Company.CompanyChancery;
                SqlPendingDocuments.AddPendingDocument(chancery.Id, document.Id);
                chancery.PendingDocuments.Add(document);
            }
            PendingDocuments.Remove(document);
            SqlDirectorPendingDocuments.DeletePendingDocument(EmployeeId, document.Id);
        }

        public void SendCopyToArchive(Document document)
        {
            Document copy = (Document) document.Clone();
            Chancery chancery = Company.CompanyChancery;
            SqlArchive.AddArchivedDocument(chancery.Id, copy.Id);
            chancery.Archive.Add(copy);
        }

        public new void Persist()
        {
            EmployeeId = SqlDirector.AddDirector(this);
            SqlPerson.UpdatePerson(this);
        }

        public new void Update()
        {
            SqlDirector.UpdateDirector(this);
        }

        public override void Quit()
        {
            foreach (Document document in PendingDocuments)
            {
                SqlDirectorPendingDocuments.DeletePendingDocument(EmployeeId, document.Id);
                document.Delete();
            }
            PendingDocuments = null;
            Working = false;
            SqlDirector.DeleteDirector(EmployeeId);
            SqlPerson.UpdatePerson(this);
        }
    }
}
