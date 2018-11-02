using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class CrudDirector
    {
        /*
        public static Director CreateDirector(Director director)
        {
            int id = SqlDirector.AddDirector(director);
            director.EmployeeId = id;
            SqlPerson.UpdatePerson(director);
            return director;
        }

        public static Director ConstructDirector(Person person, Company company, byte[] signature, int salary)
        {
            Director director = new Director(person, company, signature, salary);
            return director;
        }

        public static void UpdateDirector(Director director)
        {
            SqlDirector.UpdateDirector(director);
        }

        public static void DeleteDirector(Director director)
        {
            foreach(Document document in director.PendingDocuments)
            {
                SqlDirectorPendingDocuments.DeletePendingDocument(director.Id, document.Id);
                CrudDocument.DeleteDocument(document);
            }
            director.Working = false;
            SqlDirector.DeleteDirector(director.EmployeeId);
            SqlPerson.UpdatePerson(director);
            director = null;
        }
        */
    }
}
