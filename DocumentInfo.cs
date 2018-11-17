using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    [Serializable]
    public class DocumentInfo
    {
        public DateTime Time { get; set; }
        public string Title { get; set; }
        public DocumentType Type { get; set; }

        public DocumentInfo(string title, DocumentType type, DateTime time)
        {
            Title = title;
            Type = type;
            Time = time;
        }
    }
}
