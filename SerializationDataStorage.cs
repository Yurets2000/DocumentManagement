using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Runtime.Serialization.Formatters.Binary;
using static DocumentManagement.Base;

namespace DocumentManagement
{
    public class SerializationDataStorage
    {
        private static SerializationDataStorage storage;

        public SerializationDataLists DataLists { get; private set; }

        private SerializationDataStorage()
        {
            DataLists = new SerializationDataLists();
            InitializePersons(DataLists);
            InitializeCompanies(DataLists);
            InitializeChanceries(DataLists);
            InitializeDirectors(DataLists);
            InitializeSecretaries(DataLists);
            InitializeMainSecretaries(DataLists);
            InitializeDocuments(DataLists);
            InitializeCompanyTypes(DataLists);
            InitializeDocumentTypes(DataLists);
            InitializeMarkers(DataLists);
        }

        public static SerializationDataStorage GetInstance()
        {
            if (storage == null)
            {
                storage = new SerializationDataStorage();
            }
            return storage;
        }

        public void InitializeData()
        {
            InitializeData(DataLists);
        }
       
        public void InitializeData(SerializationDataLists dataLists)
        {
            InitializePersons(dataLists);
            InitializeCompanies(dataLists);
            InitializeChanceries(dataLists);
            InitializeDirectors(dataLists);
            InitializeSecretaries(dataLists);
            InitializeMainSecretaries(dataLists);
            InitializeDocuments(dataLists);
            InitializeCompanyTypes(dataLists);
            InitializeDocumentTypes(dataLists);
            InitializeMarkers(dataLists);
        }

        public void PersistData()
        {
            PersistData(DataLists);
        }

        public void PersistData(SerializationDataLists dataLists)
        {
            PersistPersons(dataLists);
            PersistCompanies(dataLists);
            PersistChanceries(dataLists);
            PersistDirectors(dataLists);
            PersistSecretaries(dataLists);
            PersistMainSecretaries(dataLists);
            PersistDocuments(dataLists);
            PersistCompanyTypes(dataLists);
            PersistDocumentTypes(dataLists);
            PersistMarkers(dataLists);
        }

        public void InitializePersons(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["PersonsSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                List<Person> persons;
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    persons = (List<Person>)formatter.Deserialize(fs);
                }
                dataLists.Persons = persons;
            }
        }

        public void InitializeCompanies(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["CompaniesSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                List<Company> companies;
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    companies = (List<Company>)formatter.Deserialize(fs);
                }
                dataLists.Companies = companies;
            }
        }

        public void InitializeChanceries(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["ChanceriesSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                List<Chancery> chanceries;
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    chanceries = (List<Chancery>)formatter.Deserialize(fs);
                }
                dataLists.Chanceries = chanceries;
            }
        }

        public void InitializeDirectors(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["DirectorsSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                List<Director> directors;
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    directors = (List<Director>)formatter.Deserialize(fs);
                }
                dataLists.Directors = directors;
            }
        }

        public void InitializeSecretaries(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["SecretariesSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                List<Secretary> secretaries;
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    secretaries = (List<Secretary>)formatter.Deserialize(fs);
                }
                dataLists.Secretaries = secretaries;
            }
        }

        public void InitializeMainSecretaries(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["MainSecretariesSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                List<MainSecretary> mainSecretaries;
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    mainSecretaries = (List<MainSecretary>)formatter.Deserialize(fs);
                }
                dataLists.MainSecretaries = mainSecretaries;
            }
        }

        public void InitializeDocuments(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["DocumentsSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                List<Document> documents;
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    documents = (List<Document>)formatter.Deserialize(fs);
                }
                dataLists.Documents = documents;
            }
        }

        public void InitializeDocumentTypes(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["DocumentTypesSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                List<DocumentType> documentTypes;
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    documentTypes = (List<DocumentType>)formatter.Deserialize(fs);
                }
                dataLists.DocumentTypes = documentTypes;
            }
        }

        public void InitializeCompanyTypes(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["CompanyTypesSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                List<CompanyType> companyTypes;
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    companyTypes = (List<CompanyType>)formatter.Deserialize(fs);
                }
                dataLists.CompanyTypes = companyTypes;
            }
        }

        public void InitializeMarkers(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["MarkersSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                List<Marker> markers;
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    markers = (List<Marker>)formatter.Deserialize(fs);
                }
                dataLists.Markers = markers;
            }
        }

        public void PersistPersons(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["PersonsSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    if (dataLists.Persons != null)
                    {
                        formatter.Serialize(fs, dataLists.Persons);
                    }
                }
            }
        }

        public void PersistCompanies(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["CompaniesSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    if (dataLists.Companies != null)
                    {
                        formatter.Serialize(fs, dataLists.Companies);
                    }
                }
            }
        }

        public void PersistChanceries(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["ChanceriesSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    if (dataLists.Chanceries != null)
                    {
                        formatter.Serialize(fs, dataLists.Chanceries);
                    }
                }
            }
        }

        public void PersistDirectors(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["DirectorsSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    if (dataLists.Directors != null)
                    {
                        formatter.Serialize(fs, dataLists.Directors);
                    }
                }
            }
        }

        public void PersistSecretaries(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["SecretariesSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    if (dataLists.Secretaries != null)
                    {
                        formatter.Serialize(fs, dataLists.Secretaries);
                    }
                }
            }
        }

        public void PersistMainSecretaries(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["MainSecretariesSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    if (dataLists.MainSecretaries != null)
                    {
                        formatter.Serialize(fs, dataLists.MainSecretaries);
                    }
                }
            }
        }

        public void PersistDocuments(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["DocumentsSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    if (dataLists.Documents != null)
                    {
                        formatter.Serialize(fs, dataLists.Documents);
                    }
                }
            }
        }

        public void PersistCompanyTypes(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["CompanyTypesSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    if (dataLists.CompanyTypes != null)
                    {
                        formatter.Serialize(fs, dataLists.CompanyTypes);
                    }
                }
            }
        }

        public void PersistDocumentTypes(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["DocumentTypesSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    if (dataLists.DocumentTypes != null)
                    {
                        formatter.Serialize(fs, dataLists.DocumentTypes);
                    }
                }
            }
        }

        public void PersistMarkers(SerializationDataLists dataLists)
        {
            string filename = ConfigurationManager.AppSettings["MarkersSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    if (dataLists.Markers != null)
                    {
                        formatter.Serialize(fs, dataLists.Markers);
                    }
                }
            }
        }
    }
}
