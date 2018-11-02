using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class Document : ICloneable
    {
        public bool InitDocument { get; set; }

        public int Id { get; set; }
        public DocumentType Type { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int DocumentCode { get; set; }
        public bool SenderConfirm { get; set; }
        public bool ReceiverConfirm { get; set; }
        public Secretary _creator { get; set; }
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
                        _sender = SqlCompany.GetCompany(_sender.Id);
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
                        _receiver = SqlCompany.GetCompany(_receiver.Id);
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
        public Secretary Creator
        {
            get
            {
                if (_creator != null && _creator.InitSecretary)
                {
                    if (_creator.EmployeeId <= 0)
                    {
                        throw new Exception("Creator Id <= 0");
                    }
                    else
                    {
                        _creator = SqlSecretary.GetSecretary(_creator.EmployeeId);
                        _creator.InitSecretary = false;
                    }
                }
                return _creator;
            }
            set
            {
                _creator = value;
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

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Document document = (Document)obj;
            if (document.Id <= 0 || Id <= 0)
            {
                return false;
            }
            return Id == document.Id;
        }

        public void Persist()
        {
            Id = SqlDocument.AddDocument(this);
        }

        public static Document GetDocument(int Id)
        {
            return SqlDocument.GetDocument(Id);
        }

        public void Update()
        {
            SqlDocument.UpdateDocument(this);
        }

        public void Delete()
        {
            SqlDocument.DeleteDocument(Id);
        }

        public object Clone()
        {
            Document clone = new Document(Type, DocumentCode, Title)
            {
                Id = this.Id,
                Text = this.Text,
                CreationDate = this.CreationDate,
                Receiver = this.Receiver,
                Sender = this.Sender,
                SenderConfirm = this.SenderConfirm,
                ReceiverConfirm = this.ReceiverConfirm,
                Creator = this.Creator
            };
            return clone;
        }
    }
}
