using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class Chancery
    {
        public bool InitChancery { get; set; }

        public int Id { get; set; }
        public List<Document> Archive { get; set; } = new List<Document>();
        public List<Document> PendingDocuments { get; set; } = new List<Document>();
        public List<Secretary> Secretaries { get; set; } = new List<Secretary>();

        private Company _chanceryCompany;
        private MainSecretary _mainSecretary;
        
        public Company ChanceryCompany
        {
            get
            {
                if(_chanceryCompany != null && _chanceryCompany.InitCompany)
                {
                    if(_chanceryCompany.Id <= 0)
                    {
                        throw new Exception("Company Id <= 0");
                    }
                    else
                    {
                        _chanceryCompany = SqlCompany.GetCompany(_chanceryCompany.Id);
                    }
                }
                return _chanceryCompany;
            }
            set
            {
                _chanceryCompany = value;
            }
        }
        public MainSecretary MainSecretary
        {
            get
            {
                if(_mainSecretary != null && _mainSecretary.InitMainSecretary)
                {
                    if(_mainSecretary.EmployeeId <= 0)
                    {
                        throw new Exception("MainSecretary Id <= 0");
                    }
                    else
                    {
                        _mainSecretary = SqlMainSecretary.GetMainSecretary(_mainSecretary.EmployeeId);
                    }
                }
                return _mainSecretary;
            }
            set
            {
                _mainSecretary = value;
            }
        }

        public Chancery() { }

        public Chancery(Company chanceryCompany)
        {
            ChanceryCompany = chanceryCompany;
        }

        public void Persist()
        {
            Id = SqlChancery.AddChancery(this);
        }

        public static Chancery GetChancery(int Id)
        {
            return SqlChancery.GetChancery(Id);
        }

        public void Update()
        {
            SqlChancery.UpdateChancery(this);
        }

        public void Delete()
        {
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
                SqlPendingDocuments.DeletePendingDocument(Id, document.Id);
                document.Delete();
            }
            PendingDocuments = null;
            //Удаляем архив
            foreach (Document document in Archive)
            {
                SqlArchive.DeleteArchivedDocument(Id, document.Id);
                document.Delete();
            }
            Archive = null;
            SqlChancery.DeleteChancery(Id);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
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
    }
}
