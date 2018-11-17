using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DocumentManagement.Base;

namespace DocumentManagement
{
    [Serializable]
    public class Document : ICloneable
    {
        public InitializationState InitState { get; set; } = InitializationState.NOT_INITIALIZED;
        public UpdateState UpdateState { get; set; } = UpdateState.UPDATE_NEEDED;
        public int Id { get; set; }
        public DocumentType Type { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int DocumentCode { get; set; }
        public bool SenderConfirm { get; set; }
        public bool ReceiverConfirm { get; set; }
        public Company Sender { get; set; }
        public Company Receiver { get; set; }
        public Employee Creator { get; set; }

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
            if (obj == null || obj == DBNull.Value)
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
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            Id = DataLists.GenerateDocumentId();
            dataLists.Documents.Add(this);
        }

        public void Delete()
        {
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            dataLists.Documents.Remove(this);
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
                Creator = this.Creator,
                InitState = this.InitState,
                UpdateState = this.UpdateState
            };
            return clone;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
