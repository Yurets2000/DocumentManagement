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
    public class SqlPendingDocuments
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<Document> GetPendingDocuments(int chanceryId)
        {
            List<Document> documents = new List<Document>();
            string sqlExpression = "SELECT * FROM PendingDocuments WHERE ChanceryId = @chanceryId AND Deleted = 0";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@chanceryId", SqlDbType.Int);
                command.Parameters["@chanceryId"].Value = chanceryId;
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

        public static void AddPendingDocument(int chanceryId, int documentId)
        {
            string sqlExpression = "INSERT INTO PendingDocuments(ChanceryId, DocumentId) VALUES (@chanceryId, @documentId)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@chanceryId", SqlDbType.Int);
                command.Parameters.Add("@documentId", SqlDbType.Int);
                command.Parameters["@chanceryId"].Value = chanceryId;
                command.Parameters["@documentId"].Value = documentId;
                command.ExecuteNonQuery();
            }
        }

        public static void DeletePendingDocument(int chanceryId, int documentId)
        {
            string sqlExpression = "UPDATE PendingDocuments SET Deleted = 1 WHERE ChanceryId = @chanceryId AND DocumentId = @documentId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@chanceryId", SqlDbType.Int);
                command.Parameters.Add("@documentId", SqlDbType.Int);
                command.Parameters["@chanceryId"].Value = chanceryId;
                command.Parameters["@documentId"].Value = documentId;
                command.ExecuteNonQuery();
            }
        }
    }
}
