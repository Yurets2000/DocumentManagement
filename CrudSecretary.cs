using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class CrudSecretary
    {
        /*
        public static Secretary CreateSecretary(Secretary secretary)
        {
            Marker marker = secretary.Marker;
            int markerId = SqlMarker.AddMarker(marker);
            marker.Id = markerId;

            int id = SqlSecretary.AddSecretary(secretary);
            secretary.EmployeeId = id;
            SqlPerson.UpdatePerson(secretary);
            return secretary;
        }

        public static Secretary ConstructSecretary(Person person, Company company, int salary)
        {
            Marker marker = new Marker(Marker.Color.BLUE);
            Secretary secretary = new Secretary(person, company, salary)
            {
                Marker = marker
            };
            return secretary;
        }

        public static Secretary ReadSecretary(int id)
        {
            return SqlSecretary.GetSecretary(id);
        }

        public static void UpdateSecretary(Secretary secretary)
        {
            SqlSecretary.UpdateSecretary(secretary);
        }

        public static void DeleteSecretary(Secretary secretary)
        {
            //Удаляем созданные неотправленные документы
            foreach(Document document in secretary.CreatedDocuments)
            {
                SqlSecretaryCreatedDocuments.DeleteCreatedDocument(secretary.Id, document.Id);
                SqlDocument.DeleteDocument(document.Id);
            }
            //Направляем необработанные документы обратно в секретариат
            Chancery chancery = secretary.Company.CompanyChancery;
            foreach(Document document in secretary.PendingDocuments)
            {
                SqlSecretaryPendingDocuments.DeletePendingDocument(secretary.Id, document.Id);
                SqlPendingDocuments.AddPendingDocument(chancery.Id, document.Id);
            }
            //Удаляем маркер
            SqlMarker.DeleteMarker(secretary.Marker.Id);

            secretary.Working = false;
            SqlSecretary.DeleteSecretary(secretary.EmployeeId);
            SqlPerson.UpdatePerson(secretary);
            secretary = null;
        }
        */
    }
}
