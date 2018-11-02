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
    public class SqlDirectorPendingDocuments
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<Document> GetPendingDocuments(int directorId)
        {
            List<Document> documents = new List<Document>();
            string sqlExpression = "SELECT * FROM DirectorPendingDocuments WHERE DirectorId = @directorId AND Deleted = 0";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@directorId", SqlDbType.Int);
                command.Parameters["@directorId"].Value = directorId;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int documentId = (int)reader["DocumentId"];
                        Document document = SqlDocument.GetDocument(documentId);
                        documents.Add(document);
                    }
                }
            }
            return documents;
        }

        public static void AddPendingDocument(int directorId, int documentId)
        {
            string sqlExpression = "INSERT INTO DirectorPendingDocuments(DirectorId, DocumentId) VALUES (@directorId, @documentId)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@directorId", SqlDbType.Int);
                command.Parameters.Add("@documentId", SqlDbType.Int);
                command.Parameters["@directorId"].Value = directorId;
                command.Parameters["@documentId"].Value = documentId;
                command.ExecuteNonQuery();
            }
        }

        public static void DeletePendingDocument(int directorId, int documentId)
        {
            string sqlExpression = "UPDATE DirectorPendingDocuments SET Deleted = 1 WHERE DirectorId = @directorId AND DocumentId = @documentId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@directorId", SqlDbType.Int);
                command.Parameters.Add("@documentId", SqlDbType.Int);
                command.Parameters["@directorId"].Value = directorId;
                command.Parameters["@documentId"].Value = documentId;
                command.ExecuteNonQuery();
            }
        }
    }
}
