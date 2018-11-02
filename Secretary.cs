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

        public List<Document> PendingDocuments { get; set; }
        public List<Document> CreatedDocuments { get; set; }
        public Marker Marker { get; set; }

        public Secretary() : base() { }

        public Secretary(Person person, Company company, int salary) : base(person, company, salary) { }

        public Document CreateDocument(DocumentType type, int documentCode, string title, string content, Company receiver)
        {
            Document document = new Document(type, documentCode, title)
            {
                Text = content,
                Receiver = receiver,
                Sender = Company
            };
            document.Persist();
            SqlSecretaryCreatedDocuments.AddCreatedDocument(EmployeeId, document.Id);
            CreatedDocuments.Add(document);
            return document;
        }

        public void SendCreatedDocument(Document document)
        {
            CreatedDocuments.Remove(document);
            SqlSecretaryCreatedDocuments.DeleteCreatedDocument(EmployeeId, document.Id);
            Director director = Company.Director;
            SqlDirectorPendingDocuments.AddPendingDocument(director.EmployeeId, document.Id);
            director.PendingDocuments.Add(document);
        }

        public void SendPendingDocument(Document document)
        {
            PendingDocuments.Remove(document);
            SqlSecretaryPendingDocuments.DeletePendingDocument(EmployeeId, document.Id);
            Director director = Company.Director;
            SqlDirectorPendingDocuments.AddPendingDocument(director.EmployeeId, document.Id);
            director.PendingDocuments.Add(document);
        }

        public void RemoveCreatedDocument(Document document)
        {
            SqlSecretaryCreatedDocuments.DeleteCreatedDocument(EmployeeId, document.Id);
            CreatedDocuments.Remove(document);
            document.Delete();
            document = null;
        }

        public new void Persist()
        {
            int markerId = SqlMarker.AddMarker(Marker);
            Marker.Id = markerId;
            EmployeeId = SqlSecretary.AddSecretary(this);
            SqlPerson.UpdatePerson(this);
        }

        public new void Update()
        {
            SqlSecretary.UpdateSecretary(this);
        }

        public static Secretary GetSecretary(int id)
        {
            return SqlSecretary.GetSecretary(id);
        }

        public void EditDocument(Document document)
        {
            document.Update();
        }

        public override void Quit()
        {
            //Удаляем созданные неотправленные документы
            foreach (Document document in CreatedDocuments)
            {
                SqlSecretaryCreatedDocuments.DeleteCreatedDocument(EmployeeId, document.Id);
                document.Delete();
            }
            CreatedDocuments = null;
            //Направляем необработанные документы обратно в секретариат
            Chancery chancery = Company.CompanyChancery;
            foreach (Document document in PendingDocuments)
            {
                SqlSecretaryPendingDocuments.DeletePendingDocument(EmployeeId, document.Id);
                SqlPendingDocuments.AddPendingDocument(chancery.Id, document.Id);
            }
            PendingDocuments = null;
            //Удаляем маркер
            SqlMarker.DeleteMarker(Marker.Id);
            Marker = null;
            Working = false;
            SqlSecretary.DeleteSecretary(EmployeeId);
            SqlPerson.UpdatePerson(this);
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
