using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class Chancery
    {
        public InitializationState InitState { get; set; } = InitializationState.NOT_INITIALIZED;
        public UpdateState UpdateState { get; set; } = UpdateState.UPDATE_NEEDED;
        public int Id { get; set; }
        public List<Document> Archive { get; set; }
        public List<Document> PendingDocuments { get; set; }
        public List<Secretary> Secretaries { get; set; }
        public Company Company { get; set; }
        public MainSecretary MainSecretary { get; set; }

        public Chancery() { }

        public Chancery(Company chanceryCompany)
        {
            Company = chanceryCompany;
        }

        public void Persist()
        {
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            Id = DataLists.GenerateChanceryId();
            dataLists.Chanceries.Add(this);
        }

        public void Delete()
        {
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            //Удаляем всех секретарей из секретариата
            foreach (Secretary secretary in Secretaries)
            {
                secretary.Quit();
            }
            Secretaries = null;
            //Удаляем главного секретаря
            if (MainSecretary != null)
            {
                MainSecretary.Quit();
                MainSecretary = null;
            }
            //Удаляем необработанные документы
            foreach (Document document in PendingDocuments)
            {     
                document.Delete();
            }
            PendingDocuments = null;
            //Удаляем архив
            foreach (Document document in Archive)
            {
                document.Delete();
            }
            Archive = null;
            dataLists.Chanceries.Remove(this);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }
            Chancery chancery = (Chancery)obj;
            if (chancery.Id <= 0 || Id <= 0)
            {
                return false;
            }
            return Id == chancery.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
