using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DocumentManagement
{
    public class SqlSecretaryCreatedDocuments
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<Document> GetCurrentDocuments(int secretaryId)
        {
            List<Document> documents = new List<Document>();
            string sqlExpression = "SELECT * FROM SecretaryCreatedDocuments WHERE SecretaryId = @secretaryId AND Deleted = 0";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@secretaryId", SqlDbType.Int);
                command.Parameters["@secretaryId"].Value = secretaryId;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int documentId = (int)reader["DocumentId"];
                        Document document = new Document
                        {
                            Id = documentId,
                            InitState = InitializationState.INITIALIZATION_NEEDED
                        };
                        documents.Add(document);
                    }
                }
            }
            return documents;
        }

        public static List<DocumentInfo> GetCreatedDocumentsInfo(int secretaryId)
        {
            //Some stuff
            return null;
        }

        public static void AddCreatedDocument(int secretaryId, int documentId)
        {
            string sqlExpression = "INSERT INTO SecretaryCreatedDocuments(SecretaryId, DocumentId) VALUES (@secretaryId, @documentId)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@secretaryId", SqlDbType.Int);
                command.Parameters.Add("@documentId", SqlDbType.Int);
                command.Parameters["@secretaryId"].Value = secretaryId;
                command.Parameters["@documentId"].Value = documentId;
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteCreatedDocument(int secretaryId, int documentId)
        {
            string sqlExpression = "UPDATE SecretaryCreatedDocuments SET Deleted = 1 WHERE SecretaryId = @secretaryId AND DocumentId = @documentId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@secretaryId", SqlDbType.Int);
                command.Parameters.Add("@documentId", SqlDbType.Int);
                command.Parameters["@secretaryId"].Value = secretaryId;
                command.Parameters["@documentId"].Value = documentId;
                command.ExecuteNonQuery();
            }
        }
    }
}
