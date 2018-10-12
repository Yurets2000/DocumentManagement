using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class Secretary : Employee
    {
        public bool InitSecretary { get; set; }

        public List<Document> PendingDocuments { get; set; } = new List<Document>();
        public Marker Marker { get; set; }

        public Secretary() : base() { }

        public Secretary(Person person, Company company, int salary) : base(person, company, salary) { }

        public Document CreateDocument(DocumentType type, int documentCode, string title, string text, Company receiver)
        {
            Document document = new Document(type, documentCode, title)
            {
                Sender = Company,
                Receiver = receiver,
                Text = text
            };
            int documentId = SqlDocumentDAO.AddDocument(document);
            document.Id = documentId;
            return document;
        }

        public void SendDocument(Document document)
        {
            Company receiver = document.Receiver;
            SqlPendingDocumentsDAO.AddPendingDocument(receiver.CompanyChancery.Id, document.Id);
            receiver.CompanyChancery.PendingDocuments.Add(document);
        }

        public void SendDocumentToArchive(Document document)
        {
            SqlArchiveDAO.AddArchivedDocument(Company.CompanyChancery.Id, document.Id);
            Company.CompanyChancery.Archive.Add(document);
        }

        public void RemoveDocument(Document document)
        {
            SqlSecretaryPendingDocumentsDAO.DeletePendingDocument(EmployeeId, document.Id);
            SqlDocumentDAO.DeleteDocument(document.Id);
            PendingDocuments.Remove(document);
        }

        public void UnderlineText(Document document, int offset, int length) { }
    }
}
