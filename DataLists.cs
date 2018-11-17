using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DocumentManagement.Base;

namespace DocumentManagement
{
    public class DataLists
    {
        private static Dictionary<PersistedClass, int> _lastIdDictionary = new Dictionary<PersistedClass, int>();

        public List<Person> Persons { get; private set; }
        public List<Company> Companies { get; private set; }
        public List<Chancery> Chanceries { get; private set; }
        public List<Director> Directors { get; private set; }
        public List<Secretary> Secretaries { get; private set; }
        public List<MainSecretary> MainSecretaries { get; private set; }
        public List<Document> Documents { get; private set; }
        public List<CompanyType> CompanyTypes { get; private set; }
        public List<DocumentType> DocumentTypes { get; private set; }
        public List<Marker> Markers { get; private set; }


        static DataLists()
        {
            _lastIdDictionary.Add(PersistedClass.Person, 0);
            _lastIdDictionary.Add(PersistedClass.Company, 0);
            _lastIdDictionary.Add(PersistedClass.Chancery, 0);
            _lastIdDictionary.Add(PersistedClass.Director, 0);
            _lastIdDictionary.Add(PersistedClass.Secretary, 0);
            _lastIdDictionary.Add(PersistedClass.MainSecretary, 0);
            _lastIdDictionary.Add(PersistedClass.Document, 0);
            _lastIdDictionary.Add(PersistedClass.CompanyType, 0);
            _lastIdDictionary.Add(PersistedClass.DocumentType, 0);
            _lastIdDictionary.Add(PersistedClass.Marker, 0);
        }

        public void Initialize()
        {
            Persons = SqlPerson.GetAllPersons();
            Companies = SqlCompany.GetAllCompanies();
            Companies.ForEach((c) => c.InitState = InitializationState.INITIALIZATION_NEEDED);
            Chanceries = SqlChancery.GetAllChanceries();
            Chanceries.ForEach((c) => c.InitState = InitializationState.INITIALIZATION_NEEDED);
            Directors = SqlDirector.GetAllDirectors();
            Directors.ForEach((d) => d.InitState = InitializationState.INITIALIZATION_NEEDED);
            Secretaries = SqlSecretary.GetAllSecretaries();
            Secretaries.ForEach((s) => s.InitState = InitializationState.INITIALIZATION_NEEDED);
            MainSecretaries = SqlMainSecretary.GetAllMainSecretaries();
            MainSecretaries.ForEach((ms) => ms.InitState = InitializationState.INITIALIZATION_NEEDED);
            Documents = SqlDocument.GetAllDocuments();
            Documents.ForEach((d) => d.InitState = InitializationState.INITIALIZATION_NEEDED);
            CompanyTypes = SqlCompanyType.GetAllCompanyTypes();
            DocumentTypes = SqlDocumentType.GetAllDocumentTypes();
            Markers = SqlMarker.GetAllMarkers();
        }

        public static int GeneratePersonId()
        {
            if (_lastIdDictionary[PersistedClass.Person] == 0)
            {
                _lastIdDictionary[PersistedClass.Person] = SqlPerson.GetMaxId();
            }
            _lastIdDictionary[PersistedClass.Person]++;
            return _lastIdDictionary[PersistedClass.Person];
        }

        public static int GenerateCompanyId()
        {
            if (_lastIdDictionary[PersistedClass.Company] == 0)
            {
                _lastIdDictionary[PersistedClass.Company] = SqlCompany.GetMaxId();
            }
            _lastIdDictionary[PersistedClass.Company]++;
            return _lastIdDictionary[PersistedClass.Company];
        }

        public static int GenerateChanceryId()
        {
            if (_lastIdDictionary[PersistedClass.Chancery] == 0)
            {
                _lastIdDictionary[PersistedClass.Chancery] = SqlChancery.GetMaxId();
            }
            _lastIdDictionary[PersistedClass.Chancery]++;
            return _lastIdDictionary[PersistedClass.Chancery];
        }

        public static int GenerateDirectorId()
        {
            if (_lastIdDictionary[PersistedClass.Director] == 0)
            {
                _lastIdDictionary[PersistedClass.Director] = SqlDirector.GetMaxId();
            }
            _lastIdDictionary[PersistedClass.Director]++;
            return _lastIdDictionary[PersistedClass.Director];
        }

        public static int GenerateSecretaryId()
        {
            if (_lastIdDictionary[PersistedClass.Secretary] == 0)
            {
                _lastIdDictionary[PersistedClass.Secretary] = SqlSecretary.GetMaxId();
            }
            _lastIdDictionary[PersistedClass.Secretary]++;
            return _lastIdDictionary[PersistedClass.Secretary];
        }

        public static int GenerateMainSecretaryId()
        {
            if (_lastIdDictionary[PersistedClass.MainSecretary] == 0)
            {
                _lastIdDictionary[PersistedClass.MainSecretary] = SqlMainSecretary.GetMaxId();
            }
            _lastIdDictionary[PersistedClass.MainSecretary]++;
            return _lastIdDictionary[PersistedClass.MainSecretary];
        }

        public static int GenerateDocumentId()
        {
            if (_lastIdDictionary[PersistedClass.Document] == 0)
            {
                _lastIdDictionary[PersistedClass.Document] = SqlDocument.GetMaxId();
            }
            _lastIdDictionary[PersistedClass.Document]++;
            return _lastIdDictionary[PersistedClass.Document];
        }

        public static int GenerateCompanyTypeId()
        {
            if (_lastIdDictionary[PersistedClass.CompanyType] == 0)
            {
                _lastIdDictionary[PersistedClass.CompanyType] = SqlCompanyType.GetMaxId();
            }
            _lastIdDictionary[PersistedClass.CompanyType]++;
            return _lastIdDictionary[PersistedClass.CompanyType];
        }

        public static int GenerateDocumentTypeId()
        {
            if (_lastIdDictionary[PersistedClass.DocumentType] == 0)
            {
                _lastIdDictionary[PersistedClass.DocumentType] = SqlDocumentType.GetMaxId();
            }
            _lastIdDictionary[PersistedClass.DocumentType]++;
            return _lastIdDictionary[PersistedClass.DocumentType];
        }

        public static int GenerateMarkerId()
        {
            if (_lastIdDictionary[PersistedClass.Marker] == 0)
            {
                _lastIdDictionary[PersistedClass.Marker] = SqlMarker.GetMaxId();
            }
            _lastIdDictionary[PersistedClass.Marker]++;
            return _lastIdDictionary[PersistedClass.Marker];
        }
    }
}
