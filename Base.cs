using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class Base
    {
        public enum PersistedClass
        {
            Person, Company, Chancery, Director, Secretary, MainSecretary, Document, CompanyType, DocumentType, Marker
        }

        public enum InitializationState
        {
            NOT_INITIALIZED, INITIALIZATION_NEEDED, IN_INITIALIZATION, INITIALIZED
        }
    }
}
