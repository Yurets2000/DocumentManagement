using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class CrudChancery
    {
        /*
        public static Chancery CreateChancery(Chancery chancery)
        {
            int id = SqlChancery.AddChancery(chancery);
            chancery.Id = id;
            return chancery;
        }

        public static Chancery ConstructChancery()
        {
            Chancery chancery = new Chancery();
            return chancery;
        }

        public static Chancery ReadChancery(int chanceryId)
        {
            return SqlChancery.GetChancery(chanceryId);
        }

        public static void UpdateChancery(Chancery chancery)
        {
            SqlChancery.UpdateChancery(chancery);
        }

        public static void DeleteChancery(Chancery chancery)
        {
            //Удаляем всех секретарей из секретариата
            foreach(Secretary secretary in chancery.Secretaries)
            {
                CrudSecretary.DeleteSecretary(secretary);
            }
            chancery.Secretaries = null;
            //Удаляем главного секретаря
            if(chancery.MainSecretary != null)
            {
                CrudMainSecretary.DeleteMainSecretary(chancery.MainSecretary);
                chancery.MainSecretary = null;
            }
            //Удаляем необработанные документы
            foreach(Document document in chancery.PendingDocuments)
            {
                SqlPendingDocuments.DeletePendingDocument(chancery.Id, document.Id);
                CrudDocument.DeleteDocument(document);
            }
            //Удаляем архив
            foreach(Document document in chancery.Archive)
            {
                SqlArchive.DeleteArchivedDocument(chancery.Id, document.Id);
                CrudDocument.DeleteDocument(document);
            }

            SqlChancery.DeleteChancery(chancery.Id);
            chancery = null;
        }
        */
    }
}
