using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class DataStorage
    {
        private static DataStorage storage;
        //Объекты для сравнения
        private DataLists _oldDataLists;
        //Объекты для инициализации
        public DataLists DataLists { get; private set; }

        private DataStorage()
        {
            _oldDataLists = new DataLists();
            _oldDataLists.Initialize();
            DataLists = new DataLists();
            DataLists.Initialize();
            InitializeData(_oldDataLists);
            InitializeData(DataLists);
            foreach (Secretary s in DataLists.Secretaries)
            {
                s.state = 2;
            }
            foreach (Secretary s in _oldDataLists.Secretaries)
            {
                s.state = 3;
            }
        }

        public static DataStorage GetInstance()
        {
            if(storage == null)
            {
                storage = new DataStorage();
            }
            return storage;
        }

        public void UpdateCollections()
        {
            UpdateCollections(_oldDataLists, DataLists);
        }

        public void UpdateCollections(DataLists oldDataLists, DataLists dataLists)
        {
            List<Person> oldPersons = oldDataLists.Persons;
            List<Person> persons = dataLists.Persons;
            foreach (Person person in persons)
            {
                //Если не было этой персоны то добавить
                if (oldPersons.Contains(person) == false)
                {
                    SqlPerson.AddPerson(person);
                }
            }
            foreach (Person oldPerson in oldPersons)
            {
                //Если персона была удалена то удалить
                if (persons.Contains(oldPerson) == false)
                {
                    SqlPerson.DeletePerson(oldPerson.Id);
                }
            }

            List<Company> oldCompanies = oldDataLists.Companies;
            List<Company> companies = dataLists.Companies;
            foreach (Company company in companies)
            {
                if (oldCompanies.Contains(company) == false)
                {
                    SqlCompany.AddCompany(company);
                }
            }
            foreach (Company oldCompany in oldCompanies)
            {
                if (companies.Contains(oldCompany) == false)
                {
                    SqlCompany_Director.DeleteFromCompany(oldCompany.Id);
                    SqlCompany_Chancery.DeleteFromCompany(oldCompany.Id);
                    SqlCompany.DeleteCompany(oldCompany.Id);
                }
            }

            List<Chancery> oldChanceries = oldDataLists.Chanceries;
            List<Chancery> chanceries = dataLists.Chanceries;
            foreach (Chancery chancery in chanceries)
            {
                if (oldChanceries.Contains(chancery) == false)
                {
                    SqlChancery.AddChancery(chancery);
                    AddCompanyChanceryRecord(chancery.Company, chancery);
                    foreach (Document document in chancery.PendingDocuments)
                    {
                        SqlPendingDocuments.AddPendingDocument(chancery.Id, document.Id);
                    }
                    foreach (Document document in chancery.Archive)
                    {
                        SqlArchive.AddArchivedDocument(chancery.Id, document.Id);
                    }
                }
                else
                {
                    Chancery oldChancery = oldChanceries.Find((oc) => oc.Id == chancery.Id);
                    foreach (Document document in chancery.PendingDocuments)
                    {
                        if (oldChancery.PendingDocuments.Contains(document) == false)
                        {
                            SqlPendingDocuments.AddPendingDocument(chancery.Id, document.Id);
                        }
                    }
                    foreach (Document oldDocument in oldChancery.PendingDocuments)
                    {
                        if (chancery.PendingDocuments.Contains(oldDocument) == false)
                        {
                            SqlPendingDocuments.DeletePendingDocument(chancery.Id, oldDocument.Id);
                        }
                    }
                    foreach (Document document in chancery.Archive)
                    {
                        if (oldChancery.Archive.Contains(document) == false)
                        {
                            SqlArchive.AddArchivedDocument(chancery.Id, document.Id);
                        }
                    }
                    foreach (Document oldDocument in oldChancery.Archive)
                    {
                        if (chancery.Archive.Contains(oldDocument) == false)
                        {
                            SqlArchive.DeleteArchivedDocument(chancery.Id, oldDocument.Id);
                        }
                    }
                }
            }
            foreach (Chancery oldChancery in oldChanceries)
            {
                if (chanceries.Contains(oldChancery) == false)
                {
                    SqlCompany_Chancery.GetCompanyByChancery(oldChancery.Id);
                    SqlChancery.DeleteChancery(oldChancery.Id);
                }
            }
            List<Director> oldDirectors = oldDataLists.Directors;
            List<Director> directors = dataLists.Directors;
            foreach (Director director in directors)
            {
                if (oldDirectors.Contains(director) == false)
                {
                    SqlDirector.AddDirector(director);
                    AddCompanyDirectorRecord(director.Company, director);
                    foreach (Document document in director.PendingDocuments)
                    {
                        SqlDirectorPendingDocuments.AddPendingDocument(director.EmployeeId, document.Id);
                    }
                }
                else
                {
                    Director oldDirector = oldDirectors.Find((od) => od.EmployeeId == director.EmployeeId);
                    foreach (Document document in director.PendingDocuments)
                    {
                        if (oldDirector.PendingDocuments.Contains(document) == false)
                        {
                            SqlDirectorPendingDocuments.AddPendingDocument(director.EmployeeId, document.Id);
                        }
                    }
                    foreach (Document oldDocument in oldDirector.PendingDocuments)
                    {
                        if (director.PendingDocuments.Contains(oldDocument) == false)
                        {
                            SqlDirectorPendingDocuments.DeletePendingDocument(director.EmployeeId, oldDocument.Id);
                        }
                    }
                }
            }
            foreach (Director oldDirector in oldDirectors)
            {
                if (directors.Contains(oldDirector) == false)
                {
                    foreach (Document oldDocument in oldDirector.PendingDocuments)
                    {
                        SqlDirectorPendingDocuments.DeletePendingDocument(oldDirector.EmployeeId, oldDocument.Id);
                    }
                    SqlCompany_Director.GetCompanyByDirector(oldDirector.EmployeeId);
                    SqlDirector.DeleteDirector(oldDirector.EmployeeId);
                }
            }
            List<Secretary> oldSecretaries = oldDataLists.Secretaries;
            List<Secretary> secretaries = dataLists.Secretaries;
            //документи не додаються
            foreach (Secretary secretary in secretaries)
            {
                if (oldSecretaries.Contains(secretary) == false)
                {
                    SqlSecretary.AddSecretary(secretary);
                    foreach (Document document in secretary.PendingDocuments)
                    {
                        SqlSecretaryPendingDocuments.AddPendingDocument(secretary.EmployeeId, document.Id);
                    }
                    foreach (Document document in secretary.CreatedDocuments)
                    {
                        SqlSecretaryCreatedDocuments.AddCreatedDocument(secretary.EmployeeId, document.Id);
                    }
                }
                else
                {
                    Secretary oldSecretary = oldSecretaries.Find((os) => os.EmployeeId == secretary.EmployeeId);
                    foreach (Document document in secretary.PendingDocuments)
                    {
                        if (oldSecretary.PendingDocuments.Contains(document) == false)
                        {
                            SqlSecretaryPendingDocuments.AddPendingDocument(secretary.EmployeeId, document.Id);
                        }
                    }
                    foreach (Document oldDocument in oldSecretary.PendingDocuments)
                    {
                        if (secretary.PendingDocuments.Contains(oldDocument) == false)
                        {
                            SqlSecretaryPendingDocuments.DeletePendingDocument(secretary.EmployeeId, oldDocument.Id);
                        }
                    }
                    foreach (Document document in secretary.CreatedDocuments)
                    {
                        if (oldSecretary.CreatedDocuments.Contains(document) == false)
                        {
                            SqlSecretaryCreatedDocuments.AddCreatedDocument(secretary.EmployeeId, document.Id);
                        }
                    }
                    foreach (Document oldDocument in oldSecretary.CreatedDocuments)
                    {
                        if (secretary.PendingDocuments.Contains(oldDocument) == false)
                        {
                            SqlSecretaryCreatedDocuments.DeleteCreatedDocument(secretary.EmployeeId, oldDocument.Id);
                        }
                    }
                }
            }
            foreach (Secretary oldSecretary in oldSecretaries)
            {
                if (secretaries.Contains(oldSecretary) == false)
                {
                    foreach (Document oldDocument in oldSecretary.PendingDocuments)
                    {
                        SqlSecretaryPendingDocuments.DeletePendingDocument(oldSecretary.EmployeeId, oldDocument.Id);
                    }
                    foreach (Document oldDocument in oldSecretary.CreatedDocuments)
                    {
                        SqlSecretaryCreatedDocuments.DeleteCreatedDocument(oldSecretary.EmployeeId, oldDocument.Id);
                    }
                    SqlSecretary.DeleteSecretary(oldSecretary.EmployeeId);
                }
            }
            List<MainSecretary> oldMainSecretaries = oldDataLists.MainSecretaries;
            List<MainSecretary> mainSecretaries = dataLists.MainSecretaries;
            foreach (MainSecretary mainSecretary in mainSecretaries)
            {
                if (oldMainSecretaries.Contains(mainSecretary) == false)
                {
                    SqlMainSecretary.AddMainSecretary(mainSecretary);
                }
            }
            foreach (MainSecretary oldMainSecretary in oldMainSecretaries)
            {
                if (mainSecretaries.Contains(oldMainSecretary) == false)
                {
                    SqlMainSecretary.DeleteMainSecretary(oldMainSecretary.EmployeeId);
                }
            }
            List<Document> oldDocuments = oldDataLists.Documents;
            List<Document> documents = dataLists.Documents;
            foreach (Document document in documents)
            {
                if (oldDocuments.Contains(document) == false)
                {
                    SqlDocument.AddDocument(document);
                }
            }
            foreach (Document oldDocument in oldDocuments)
            {
                if (documents.Contains(oldDocument) == false)
                {
                    SqlDocument.DeleteDocument(oldDocument.Id);
                }
            }
            List<Marker> oldMarkers = oldDataLists.Markers;
            List<Marker> markers = dataLists.Markers;
            foreach (Marker marker in markers)
            {
                if (oldMarkers.Contains(marker) == false)
                {
                    SqlMarker.AddMarker(marker);
                }
            }
            foreach (Marker oldMarker in oldMarkers)
            {
                if (markers.Contains(oldMarker) == false)
                {
                    SqlMarker.DeleteMarker(oldMarker.Id);
                }
            }
        }

        public void PersistDataChanges()
        {
            PersistDataChanges(DataLists);
        }

        public void PersistDataChanges(DataLists dataLists)
        {
            foreach (Person person in dataLists.Persons)
            {
                if (person.UpdateState == UpdateState.UPDATE_NEEDED)
                {
                    person.UpdateState = UpdateState.IN_UPDATE;
                    PersistPersonChanges(person);
                    person.UpdateState = UpdateState.UPDATE_UNNECESSARY;
                }
            }
            foreach (Company company in dataLists.Companies)
            {
                if (company.UpdateState == UpdateState.UPDATE_NEEDED)
                {
                    company.UpdateState = UpdateState.IN_UPDATE;
                    PersistCompanyChanges(company);
                    company.UpdateState = UpdateState.UPDATE_UNNECESSARY;
                }
            }
        }

        public void PersistCompanyChanges(Company company)
        {
            SqlCompany.UpdateCompany(company);
            if(company.Director != null && company.Director.UpdateState == UpdateState.UPDATE_NEEDED)
            {
                company.Director.UpdateState = UpdateState.IN_UPDATE;
                PersistDirectorChanges(company.Director);
                company.Director.UpdateState = UpdateState.UPDATE_UNNECESSARY;
            }
            if(company.Chancery != null && company.Chancery.UpdateState == UpdateState.UPDATE_NEEDED)
            {
                company.Chancery.UpdateState = UpdateState.IN_UPDATE;
                PersistChanceryChanges(company.Chancery);
                company.Chancery.UpdateState = UpdateState.UPDATE_UNNECESSARY;
            }
        }

        public void PersistChanceryChanges(Chancery chancery)
        {
            if (chancery.Archive != null)
            {
                foreach (Document document in chancery.Archive)
                {
                    if (document.UpdateState == UpdateState.UPDATE_NEEDED)
                    {
                        document.UpdateState = UpdateState.IN_UPDATE;
                        PersistDocumentChanges(document);
                        document.UpdateState = UpdateState.UPDATE_UNNECESSARY;
                    }
                }
            }
            if(chancery.PendingDocuments != null)
            {
                foreach (Document document in chancery.PendingDocuments)
                {
                    if (document.UpdateState == UpdateState.UPDATE_NEEDED)
                    {
                        document.UpdateState = UpdateState.IN_UPDATE;
                        PersistDocumentChanges(document);
                        document.UpdateState = UpdateState.UPDATE_UNNECESSARY;
                    }
                }
            }
            if(chancery.Secretaries != null)
            {
                foreach(Secretary secretary in chancery.Secretaries)
                {
                    if(secretary.UpdateState == UpdateState.UPDATE_NEEDED)
                    {
                        secretary.UpdateState = UpdateState.IN_UPDATE;
                        PersistSecretaryChanges(secretary);
                        secretary.UpdateState = UpdateState.UPDATE_UNNECESSARY;
                    }
                }
            }
            if(chancery.MainSecretary != null && chancery.MainSecretary.UpdateState == UpdateState.UPDATE_NEEDED)
            {
                chancery.MainSecretary.UpdateState = UpdateState.IN_UPDATE;
                PersistMainSecretaryChanges(chancery.MainSecretary);
                chancery.MainSecretary.UpdateState = UpdateState.UPDATE_UNNECESSARY;
            }
        }

        public void PersistDirectorChanges(Director director)
        {
            SqlDirector.UpdateDirector(director);
            if(director.Company != null && director.Company.UpdateState == UpdateState.UPDATE_NEEDED)
            {
                director.Company.UpdateState = UpdateState.IN_UPDATE;
                PersistCompanyChanges(director.Company);
                director.Company.UpdateState = UpdateState.UPDATE_UNNECESSARY;
            }
        }

        public void PersistMainSecretaryChanges(MainSecretary mainSecretary)
        {
            SqlMainSecretary.UpdateMainSecretary(mainSecretary);
            if (mainSecretary.Company != null && mainSecretary.Company.UpdateState == UpdateState.UPDATE_NEEDED)
            {
                mainSecretary.Company.UpdateState = UpdateState.IN_UPDATE;
                PersistCompanyChanges(mainSecretary.Company);
                mainSecretary.Company.UpdateState = UpdateState.UPDATE_UNNECESSARY;
            }
        }

        public void PersistSecretaryChanges(Secretary secretary)
        {
            SqlSecretary.UpdateSecretary(secretary);
            if (secretary.Company != null && secretary.Company.UpdateState == UpdateState.UPDATE_NEEDED)
            {
                secretary.Company.UpdateState = UpdateState.IN_UPDATE;
                PersistCompanyChanges(secretary.Company);
                secretary.Company.UpdateState = UpdateState.UPDATE_UNNECESSARY;
            }
            foreach(Document document in secretary.CreatedDocuments)
            {
                if(document.UpdateState == UpdateState.UPDATE_NEEDED)
                {
                    document.UpdateState = UpdateState.IN_UPDATE;
                    PersistDocumentChanges(document);
                    document.UpdateState = UpdateState.UPDATE_UNNECESSARY;
                }
            }
            foreach (Document document in secretary.PendingDocuments)
            {
                if (document.UpdateState == UpdateState.UPDATE_NEEDED)
                {
                    document.UpdateState = UpdateState.IN_UPDATE;
                    PersistDocumentChanges(document);
                    document.UpdateState = UpdateState.UPDATE_UNNECESSARY;
                }
            }
        }

        public void PersistDocumentChanges(Document document)
        {
            if(document.Sender != null && document.Sender.UpdateState == UpdateState.UPDATE_NEEDED)
            {
                document.Sender.UpdateState = UpdateState.IN_UPDATE;
                PersistCompanyChanges(document.Sender);
                document.Sender.UpdateState = UpdateState.UPDATE_UNNECESSARY;
            }
            if(document.Receiver != null && document.Receiver.UpdateState == UpdateState.UPDATE_NEEDED)
            {
                document.Receiver.UpdateState = UpdateState.IN_UPDATE;
                PersistCompanyChanges(document.Receiver);
                document.Receiver.UpdateState = UpdateState.UPDATE_UNNECESSARY;
            }
            /*
            if (document.Creator != null && document.Receiver.UpdateState == UpdateState.UPDATE_NEEDED)
            {
                document.Creator.UpdateState = UpdateState.IN_UPDATE;
                PersistSecretaryChanges(document.Creator);
                document.Creator.UpdateState = UpdateState.UPDATE_UNNECESSARY;
            }
            */
        }

        public void PersistPersonChanges(Person person)
        {
            SqlPerson.UpdatePerson(person);
        }

        public void InitializeData(DataLists lists)
        {
            foreach(Company company in lists.Companies)
            {
                if (company.InitState == InitializationState.INITIALIZATION_NEEDED)
                {
                    company.InitState = InitializationState.IN_INITIALIZATION;
                    InitializeCompany(company, lists);
                    company.InitState = InitializationState.INITIALIZED;
                }
            }
        }

        private void InitializeCompany(Company company, DataLists lists)
        {
            if(company.Director != null)
            {
                if (company.Director.InitState == InitializationState.INITIALIZATION_NEEDED)
                {
                    int directorId = SqlCompany_Director.GetDirectorFromCompany(company.Id);
                    Director director = lists.Directors.Find((d) => d.EmployeeId == directorId);
                    if (director != null && director.InitState == InitializationState.INITIALIZATION_NEEDED)
                    {
                        director.InitState = InitializationState.IN_INITIALIZATION;
                        company.Director = director;
                        director.Company = company;
                        InitializeDirector(director, lists);
                        company.Director.InitState = InitializationState.INITIALIZED;
                    }
                }
            }
            if(company.Chancery != null && company.Chancery.InitState == InitializationState.INITIALIZATION_NEEDED)
            {
                int chanceryId = SqlCompany_Chancery.GetChanceryFromCompany(company.Id);
                Chancery chancery = lists.Chanceries.Find((c) => c.Id == chanceryId);
                if(chancery != null && chancery.InitState == InitializationState.INITIALIZATION_NEEDED)
                {
                    chancery.InitState = InitializationState.IN_INITIALIZATION;
                    company.Chancery = chancery;
                    chancery.Company = company;
                    InitializeChancery(chancery, lists);
                    company.Chancery.InitState = InitializationState.INITIALIZED;
                }
            }
        }

        private void InitializeChancery(Chancery chancery, DataLists lists)
        {
            if (chancery.Company != null && chancery.Company.InitState == InitializationState.INITIALIZATION_NEEDED)
            {
                chancery.Company.InitState = InitializationState.IN_INITIALIZATION;
                InitializeCompany(chancery.Company, lists);
                chancery.Company.InitState = InitializationState.INITIALIZED;
            }
            if(chancery.Archive != null)
            {
                List<Document> archiveTemporal = new List<Document>();
                foreach(Document document in chancery.Archive)
                {
                    archiveTemporal.Add(lists.Documents.Find((d) => d.Id == document.Id));
                }
                chancery.Archive = archiveTemporal;
                foreach (Document document in chancery.Archive)
                {
                    if (document.InitState == InitializationState.INITIALIZATION_NEEDED)
                    {
                        document.InitState = InitializationState.IN_INITIALIZATION;
                        InitializeDocument(document, lists);
                        document.InitState = InitializationState.INITIALIZED;
                    }
                }
            }
            if(chancery.PendingDocuments != null)
            {
                List<Document> pendingDocumentsTemporal = new List<Document>();
                foreach (Document document in chancery.PendingDocuments)
                {
                    pendingDocumentsTemporal.Add(lists.Documents.Find((d) => d.Id == document.Id));
                }
                chancery.PendingDocuments = pendingDocumentsTemporal;
                foreach (Document document in chancery.PendingDocuments)
                {
                    if (document.InitState == InitializationState.INITIALIZATION_NEEDED)
                    {
                        document.InitState = InitializationState.IN_INITIALIZATION;
                        InitializeDocument(document, lists);
                        document.InitState = InitializationState.INITIALIZED;
                    }
                }
            }
            if(chancery.Secretaries != null)
            {
                List<Secretary> secretariesTemporal = new List<Secretary>();
                foreach (Secretary secretary in chancery.Secretaries)
                {
                    secretariesTemporal.Add(lists.Secretaries.Find((s) => s.EmployeeId == secretary.EmployeeId));
                }
                chancery.Secretaries = secretariesTemporal;
                foreach (Secretary secretary in chancery.Secretaries)
                {
                    if (secretary.InitState == InitializationState.INITIALIZATION_NEEDED)
                    {
                        secretary.InitState = InitializationState.IN_INITIALIZATION;
                        InitializeSecretary(secretary, lists);
                        secretary.InitState = InitializationState.INITIALIZED;
                    }
                }
            }
            if(chancery.MainSecretary != null && chancery.MainSecretary.InitState == InitializationState.INITIALIZATION_NEEDED)
            {
                chancery.MainSecretary.InitState = InitializationState.IN_INITIALIZATION;
                chancery.MainSecretary = lists.MainSecretaries.Find((ms) => ms.EmployeeId == chancery.MainSecretary.EmployeeId);
                InitializeMainSecretary(chancery.MainSecretary, lists);
                chancery.MainSecretary.InitState = InitializationState.INITIALIZED;
            }
        }

        private void InitializeMainSecretary(MainSecretary mainSecretary, DataLists lists)
        {
            if (mainSecretary.Company != null && mainSecretary.Company.InitState == InitializationState.INITIALIZATION_NEEDED)
            {
                mainSecretary.Company.InitState = InitializationState.IN_INITIALIZATION;
                mainSecretary.Company = lists.Companies.Find((c) => c.Id == mainSecretary.Company.Id);
                InitializeCompany(mainSecretary.Company, lists);
                mainSecretary.Company.InitState = InitializationState.INITIALIZED;
            }
        }

        private void InitializeSecretary(Secretary secretary, DataLists lists)
        {
            if (secretary.Company != null && secretary.Company.InitState == InitializationState.INITIALIZATION_NEEDED)
            {
                secretary.Company.InitState = InitializationState.IN_INITIALIZATION;
                secretary.Company = lists.Companies.Find((c) => c.Id == secretary.Company.Id);
                InitializeCompany(secretary.Company, lists);
                secretary.Company.InitState = InitializationState.INITIALIZED;
            }
            List<Document> pendingDocumentsTemporal = new List<Document>();
            foreach (Document document in secretary.PendingDocuments)
            {
                pendingDocumentsTemporal.Add(lists.Documents.Find((d) => d.Id == document.Id));
            }
            secretary.PendingDocuments = pendingDocumentsTemporal;
            foreach (Document document in secretary.PendingDocuments)
            {
                if (document.InitState == InitializationState.INITIALIZATION_NEEDED)
                {
                    document.InitState = InitializationState.IN_INITIALIZATION;
                    InitializeDocument(document, lists);
                    document.InitState = InitializationState.INITIALIZED;
                }
            }
            List<Document> createdDocumentsTemporal = new List<Document>();
            foreach (Document document in secretary.CreatedDocuments)
            {
                createdDocumentsTemporal.Add(lists.Documents.Find((d) => d.Id == document.Id));
            }
            secretary.CreatedDocuments = createdDocumentsTemporal;
            foreach (Document document in secretary.CreatedDocuments)
            {
                if (document.InitState == InitializationState.INITIALIZATION_NEEDED)
                {
                    document.InitState = InitializationState.IN_INITIALIZATION;
                    InitializeDocument(document, lists);
                    document.InitState = InitializationState.INITIALIZED;
                }
            }
        }

        private void InitializeDirector(Director director, DataLists lists)
        {
            if (director.Company != null && director.Company.InitState == InitializationState.INITIALIZATION_NEEDED)
            {
                director.Company.InitState = InitializationState.IN_INITIALIZATION;
                director.Company = lists.Companies.Find((c) => c.Id == director.Company.Id);
                InitializeCompany(director.Company, lists);
                director.Company.InitState = InitializationState.INITIALIZED;
            }
            List<Document> pendingDocumentsTemporal = new List<Document>();
            foreach (Document document in director.PendingDocuments)
            {
                pendingDocumentsTemporal.Add(lists.Documents.Find((d) => d.Id == document.Id));
            }
            director.PendingDocuments = pendingDocumentsTemporal;
            foreach (Document document in director.PendingDocuments)
            {
                if (document.InitState == InitializationState.INITIALIZATION_NEEDED)
                {
                    document.InitState = InitializationState.IN_INITIALIZATION;
                    InitializeDocument(document, lists);
                    document.InitState = InitializationState.INITIALIZED;
                }
            }
        }

        private void InitializeDocument(Document document, DataLists lists)
        {
            if (document.Sender != null && document.Sender.InitState == InitializationState.INITIALIZATION_NEEDED)
            {
                document.Sender.InitState = InitializationState.IN_INITIALIZATION;
                document.Sender = lists.Companies.Find((company) => company.Id == document.Sender.Id);
                InitializeCompany(document.Sender, lists);
                document.Sender.InitState = InitializationState.INITIALIZED;
            }
            if (document.Receiver != null && document.Receiver.InitState == InitializationState.INITIALIZATION_NEEDED)
            {
                document.Receiver.InitState = InitializationState.IN_INITIALIZATION;
                document.Receiver = lists.Companies.Find((company) => company.Id == document.Receiver.Id);
                InitializeCompany(document.Receiver, lists);
                document.Receiver.InitState = InitializationState.INITIALIZED;
            }
            /*
            if (document.Creator != null && document.Creator.InitState == InitializationState.INITIALIZATION_NEEDED)
            {
                document.Creator.InitState = InitializationState.IN_INITIALIZATION;
                document.Creator = SqlSecretary.GetSecretary(document.Creator.EmployeeId);
                InitializeSecretary(document.Creator, lists);
                document.Creator.InitState = InitializationState.INITIALIZED;
            }
            */
        }

        private void AddCompanyDirectorRecord(Company company, Director director)
        {
            if (director.Id != 0)
            {
                SqlCompany_Director.AddRecord(company.Id, director.EmployeeId);
            }
            else
            {
                SqlCompany_Director.AddRecord(company.Id, null);
            }
        }

        private void AddCompanyChanceryRecord(Company company, Chancery chancery)
        {
            if (chancery.Id != 0)
            {
                SqlCompany_Chancery.AddRecord(company.Id, chancery.Id);
            }
            else
            {
                SqlCompany_Chancery.AddRecord(company.Id, null);
            }
        }
    }
}
