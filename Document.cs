using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class Document
    {
        public bool InitDocument { get; set; }

        public int Id { get; set; }
        public DocumentType Type { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int DocumentCode { get; set; }
        public Company _sender, _receiver;
        
        public Company Sender
        {
            get
            {
                if (_sender != null && _sender.InitCompany)
                {
                    if (_sender.Id <= 0)
                    {
                        throw new Exception("Company Id <= 0");
                    }
                    else
                    {
                        _sender = SqlCompanyDAO.GetCompany(_sender.Id);
                        _sender.InitCompany = false;
                    }
                }
                return _sender;
            }
            set
            {
                _sender = value;
            }
        }
        public Company Receiver
        {
            get
            {
                if (_receiver != null && _receiver.InitCompany)
                {
                    if (_receiver.Id <= 0)
                    {
                        throw new Exception("Company Id <= 0");
                    }
                    else
                    {
                        _receiver = SqlCompanyDAO.GetCompany(_receiver.Id);
                        _receiver.InitCompany = false;
                    }
                }
                return _receiver;
            }
            set
            {
                _receiver = value;
            }
        }

        public Document() { }

        public Document(DocumentType type, int documentCode, string title)
        {
            Type = type;
            DocumentCode = documentCode;
            Title = title;
        }

        public override string ToString()
        {
            return Title + "(" + DocumentCode + ")";
        }
    }
}
