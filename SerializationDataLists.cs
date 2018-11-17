using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using static DocumentManagement.Base;
using System.Runtime.Serialization.Formatters.Binary;

namespace DocumentManagement
{
    public class SerializationDataLists
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

        static SerializationDataLists()
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
         

        
        private static int MaxPersonId()
        {
             int id = 0;
             string filename = ConfigurationManager.AppSettings["PeopleSerializationFileName"];
             BinaryFormatter formatter = new BinaryFormatter();
             if (File.Exists(filename))
             {
                 using (FileStream fs = new FileStream(filename, FileMode.Open))
                 {
                     List<Person> people = (List<Person>)formatter.Deserialize(fs);
                     int maxId = 0;
                     foreach (Person person in people)
                     {
                         if (person.Id > maxId)
                         {
                            maxId = person.Id;
                         }
                     }
                     id = maxId;
                 }
             }
             return id;
        }

        private static int MaxCompanyId()
        {
            int id = 0;
            string filename = ConfigurationManager.AppSettings["CompaniesSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    List<Company> companies = (List<Company>)formatter.Deserialize(fs);
                    int maxId = 0;
                    foreach (Company company in companies)
                    {
                        if (company.Id > maxId)
                        {
                            maxId = company.Id;
                        }
                    }
                    id = maxId;
                }
            }
            return id;
        }

        private static int MaxChanceryId()
        {
            int id = 0;
            string filename = ConfigurationManager.AppSettings["ChanceriesSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    List<Chancery> chanceries = (List<Chancery>)formatter.Deserialize(fs);
                    int maxId = 0;
                    foreach (Chancery chancery in chanceries)
                    {
                        if (chancery.Id > maxId)
                        {
                            maxId = chancery.Id;
                        }
                    }
                    id = maxId;
                }
            }
            return id;
        }

        private static int MaxDirectorId()
        {
            int id = 0;
            string filename = ConfigurationManager.AppSettings["DirectorsSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    List<Director> directors = (List<Director>)formatter.Deserialize(fs);
                    int maxId = 0;
                    foreach (Director director in directors)
                    {
                        if (director.Id > maxId)
                        {
                            maxId = director.Id;
                        }
                    }
                    id = maxId;
                }
            }
            return id;
        }

        private static int MaxSecretaryId()
        {
            int id = 0;
            string filename = ConfigurationManager.AppSettings["SecretariesSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    List<Secretary> secretaries = (List<Secretary>)formatter.Deserialize(fs);
                    int maxId = 0;
                    foreach (Secretary secretary in secretaries)
                    {
                        if (secretary.Id > maxId)
                        {
                            maxId = secretary.Id;
                        }
                    }
                    id = maxId;
                }
            }
            return id;
        }

        private static int MaxMainSecretaryId()
        {
            int id = 0;
            string filename = ConfigurationManager.AppSettings["MainSecretariesSerializationFileName"];
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    List<MainSecretary> mainSecretaries = (List<MainSecretary>)formatter.Deserialize(fs);
                    int maxId = 0;
                    foreach (MainSecretary mainSecretary in mainSecretaries)
                    {
                        if (mainSecretary.Id > maxId)
                        {
                            maxId = mainSecretary.Id;
                        }
                    }
                    id = maxId;
                }
            }
            return id;
        }

    }
}
