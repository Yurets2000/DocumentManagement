using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using static DocumentManagement.Base;

namespace DocumentManagement
{

    public class SqlSecretaryPendingDocuments
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<Document> GetPendingDocuments(int secretaryId)
        {
            List<Document> documents = new List<Document>();
            string sqlExpression = "SELECT * FROM SecretaryPendingDocuments WHERE SecretaryId = @secretaryId AND Deleted = 0";
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

        public static void AddPendingDocument(int secretaryId, int documentId)
        {
            string sqlExpression = "INSERT INTO SecretaryPendingDocuments(SecretaryId, DocumentId) VALUES (@secretaryId, @documentId)";
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

        public static void DeletePendingDocument(int secretaryId, int documentId)
        {
            string sqlExpression = "UPDATE SecretaryPendingDocuments SET Deleted = 1 WHERE SecretaryId = @chanceryId AND DocumentId = @documentId";
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
