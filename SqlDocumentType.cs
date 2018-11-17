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
    public class SqlDocumentType
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<DocumentType> GetAllDocumentTypes()
        {
            List<DocumentType> documentTypes = new List<DocumentType>();
            string sqlExpression = "SELECT * FROM DocumentType WHERE Deleted = 0";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string name = (string)reader["Name"];
                        DocumentType documentType = new DocumentType(name)
                        {
                            Id = id
                        };
                        documentTypes.Add(documentType);
                    }
                }
            }
            return documentTypes;
        }

        public static DocumentType GetDocumentType(int id)
        {
            DocumentType documentType = null;
            string sqlExpression = "SELECT * FROM DocumentType WHERE Id = @id AND Deleted = 0";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string name = (string)reader["Name"];
                        documentType = new DocumentType(name)
                        {
                            Id = id
                        };
                    }
                }
            }
            return documentType;
        }

        public static void AddDocumentType(DocumentType documentType)
        {
            string sqlExpression = "INSERT INTO DocumentType(Id, Name) VALUES (@id, @name)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters.Add("@name", SqlDbType.Text);
                command.Parameters["@id"].Value = documentType.Id;
                command.Parameters["@name"].Value = documentType.type;
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateDocumentType(DocumentType documentType)
        {
            string sqlExpression = "UPDATE DocumentType SET Name = @name WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@name", SqlDbType.VarChar);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@name"].Value = documentType.type;
                command.Parameters["@id"].Value = documentType.Id;
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteDocumentType(int id)
        {
            string sqlExpression = "UPDATE DocumentType SET Deleted = 1 WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;
                command.ExecuteNonQuery();
            }
        }

        public static int GetMaxId()
        {
            int max = 0;
            string sqlExpression = "SELECT COALESCE(MAX(Id), 0) FROM DocumentType";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                max = (int)command.ExecuteScalar();
            }
            return max;
        }
    }
}
