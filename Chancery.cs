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
        /*
        private List<Document> _archive;
        private List<Document> _pendingDocuments;
        private List<Secretary> _secretaries;
        
        public List<Document> Archive
        {
            get
            {
                if (Id <= 0)
                {
                    throw new Exception("Chancery Id <= 0");
                }
                else
                {
                    _archive = SqlArchiveDAO.GetArchivedDocuments(Id); 
                }
                return _archive;
            }
            set
            {
                _archive = value;
            }
        }

        public List<Document> PendingDocuments
        {
            get
            {
                if (Id <= 0)
                {
                    throw new Exception("Chancery Id <= 0");
                }
                else
                {
                    _pendingDocuments = SqlPendingDocumentsDAO.GetPendingDocuments(Id);
                }
                return _pendingDocuments;
            }
            set
            {
                _pendingDocuments = value;
            }
        }

        public List<Secretary> Secretaries
        {
            get
            {
                if(_chanceryCompany != null && _chanceryCompany.Id <= 0)
                {
                    throw new Exception("Chancery Id <= 0");
                }
                else
                {
                    _secretaries = SqlSecretaryDAO.GetCompanySecretaries(_chanceryCompany.Id);
                }
                return _secretaries;
            }
            set
            {
                _secretaries = value;
            }
        }
        */
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
                        _chanceryCompany = SqlCompanyDAO.GetCompany(_chanceryCompany.Id);
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
                        _mainSecretary = SqlMainSecretaryDAO.GetMainSecretary(_mainSecretary.EmployeeId);
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
    }
}
