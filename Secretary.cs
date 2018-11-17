using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class Secretary : Employee
    {
        public List<Document> PendingDocuments { get; set; }
        public List<Document> CreatedDocuments { get; set; }
        public Marker Marker { get; set; }
        public int state = 0;
        public static int counter = 0;

        public Secretary() : base() {
            counter++;
        }

        public Secretary(Person person, Company company, int salary) : base(person, company, salary) {
            counter++;
        }

        public Document CreateDocument(DocumentType type, int documentCode, string title, string content, Company receiver)
        {
            Document document = new Document(type, documentCode, title)
            {
                Text = content,
                Receiver = receiver,
                Sender = Company
            };
            document.Persist();
            CreatedDocuments.Add(document);
            state = 1;
            return document;
        }

        public void SendCreatedDocument(Document document)
        {
            CreatedDocuments.Remove(document);
            Director director = Company.Director;
            director.PendingDocuments.Add(document);
        }

        public void SendPendingDocument(Document document)
        {
            PendingDocuments.Remove(document);
            Director director = Company.Director;
            director.PendingDocuments.Add(document);
        }

        public void RemoveCreatedDocument(Document document)
        { 
            CreatedDocuments.Remove(document);
            document.Delete();
            document = null;
        }

        public new void Persist()
        {
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            Marker marker = new Marker(Marker.Color.GREEN);
            marker.Persist();
            Marker = marker;
            EmployeeId = DataLists.GenerateSecretaryId();
            dataLists.Secretaries.Add(this);
            Person person = dataLists.Persons.Find((p) => p.Id == Id);
            person = this;
        }

        public override void Quit()
        {
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            //Удаляем созданные неотправленные документы
            foreach (Document document in CreatedDocuments)
            {
                document.Delete();
            }
            CreatedDocuments = null;
            //Направляем необработанные документы обратно в секретариат
            Chancery chancery = Company.Chancery;
            foreach (Document document in PendingDocuments)
            {
                chancery.PendingDocuments.Add(document);
            }
            PendingDocuments = null;
            //Удаляем маркер
            Marker.Delete();
            Marker = null;
            Working = false;
            dataLists.Secretaries.Remove(this);
            Person person = dataLists.Persons.Find((p) => p.Id == Id);
            person = this;
        }

        //Получить кол. всех созданных документов
        public int GetCreatedDocumentsInfo()
        {
            List<DocumentInfo> createdDocumentsInfo = SqlSecretaryCreatedDocuments.GetCreatedDocumentsInfo(EmployeeId);
            return createdDocumentsInfo.Count;
        }

        //Получить кол. всех созданных документов начиная от даты time
        public int GetCreatedDocumentsInfo(DateTime time)
        {
            int count = 0;
            List<DocumentInfo> createdDocumentsInfo = SqlSecretaryCreatedDocuments.GetCreatedDocumentsInfo(EmployeeId);
            foreach(DocumentInfo documentInfo in createdDocumentsInfo)
            {
                if(documentInfo.Time.CompareTo(time) > 0)
                {
                    count++;
                }
            }
            return count;
        }

        //Получить кол. всех созданных документов данного типа
        public int GetCreatedDocumentsInfo(DocumentType type)
        {
            int count = 0;
            List<DocumentInfo> createdDocumentsInfo = SqlSecretaryCreatedDocuments.GetCreatedDocumentsInfo(EmployeeId);
            foreach (DocumentInfo documentInfo in createdDocumentsInfo)
            {
                if (documentInfo.Type.Equals(type))
                {
                    count++;
                }
            }
            return count;
        }

        //Получить кол. всех созданных документов данного типа начиная от даты time
        public int GetCreatedDocumentsInfo(DocumentType type, DateTime time)
        {
            int count = 0;
            List<DocumentInfo> createdDocumentsInfo = SqlSecretaryCreatedDocuments.GetCreatedDocumentsInfo(EmployeeId);
            foreach (DocumentInfo documentInfo in createdDocumentsInfo)
            {
                if (documentInfo.Type.Equals(type) && documentInfo.Time.CompareTo(time) > 0)
                {
                    count++;
                }
            }
            return count;
        }

    }
}
