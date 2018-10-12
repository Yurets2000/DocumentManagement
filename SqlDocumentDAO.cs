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
    public class SqlDocumentDAO
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<Document> GetAllDocuments()
        {
            List<Document> documents = new List<Document>();
            string sqlExpression = "SELECT * FROM Document";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        DocumentType type = new DocumentType((string)reader["DocumentTypeName"]);
                        int documentCode = (int)reader["DocumentCode"];
                        string title = (string)reader["Title"];
                        string content = (string)reader["Content"];
                        DateTime creationDate = (DateTime)reader["CreationDate"];
                        Company receiver = SqlCompanyDAO.GetCompany((int)reader["ReceiverId"]);
                        Company sender = SqlCompanyDAO.GetCompany((int)reader["SenderId"]);
                        Document document = new Document(type, documentCode, title)
                        {
                            Id = id,
                            Text = content,
                            CreationDate = creationDate,
                            Receiver = receiver,
                            Sender = sender
                        };
                        documents.Add(document);
                    }
                }
            }
            return documents;
        }

        public static Document GetDocument(int id)
        {
            Document document = null;
            string sqlExpression = "SELECT * FROM Document WHERE Id = @id";
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
                        DocumentType type = new DocumentType((string)reader["DocumentTypeName"]);
                        int documentCode = (int)reader["DocumentCode"];
                        string title = (string)reader["Title"];
                        string content = (string)reader["Content"];
                        DateTime creationDate = (DateTime)reader["CreationDate"];
                        Company receiver = SqlCompanyDAO.GetCompany((int)reader["ReceiverId"]);
                        Company sender = SqlCompanyDAO.GetCompany((int)reader["SenderId"]);
                        document = new Document(type, documentCode, title)
                        {
                            Id = id,
                            Text = content,
                            CreationDate = creationDate,
                            Receiver = receiver,
                            Sender = sender
                        };
                    }
                }
            }
            return document;
        }

        public static int AddDocument(Document document)
        {
            int id = -1;
            string sqlExpression = "INSERT INTO Document(DocumentTypeId, DocumentCode, Title, Content, CreationDate, ReceiverId, SenderId) output INSERTED.Id VALUES (@documentTypeId, @documentCode, @title, @content, @creationDate, @receiverId, @senderId)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@documentTypeId", SqlDbType.Int);
                command.Parameters.Add("@documentCode", SqlDbType.Int);
                command.Parameters.Add("@title", SqlDbType.Text);
                command.Parameters.Add("@content", SqlDbType.Text);
                command.Parameters.Add("@creationDate", SqlDbType.Date);
                command.Parameters.Add("@receiverId", SqlDbType.Int);
                command.Parameters.Add("@senderId", SqlDbType.Int);

                command.Parameters["@documentTypeId"].Value = SqlDocumentTypeDAO.GetDocumentType(document.Type.Id);
                command.Parameters["@documentCode"].Value = document.DocumentCode;
                command.Parameters["@title"].Value = document.Title;
                command.Parameters["@content"].Value = document.Text;
                command.Parameters["@creationDate"].Value = document.CreationDate;
                if(document.Receiver == null)
                {
                    command.Parameters["@receiverId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@receiverId"].Value = document.Receiver.Id;
                }
                if (document.Sender == null)
                {
                    command.Parameters["@senderId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@senderId"].Value = document.Sender.Id;
                }
                id = (int) command.ExecuteScalar();
            }
            return id;
        }

        public static void UpdateDocument(Document document)
        {
            string sqlExpression = "UPDATE Document SET DocumentTypeName = @documentTypeName, DocumentCode = @documentCode, Title = @title, Content = @content, CreationDate = @creationDate, ReceiverId = @receiverId, SenderId = @senderId WHERE Id = @id)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@documentTypeId", SqlDbType.Int);
                command.Parameters.Add("@documentCode", SqlDbType.Int);
                command.Parameters.Add("@title", SqlDbType.Text);
                command.Parameters.Add("@content", SqlDbType.Text);
                command.Parameters.Add("@creationDate", SqlDbType.Date);
                command.Parameters.Add("@receiverId", SqlDbType.Int);
                command.Parameters.Add("@senderId", SqlDbType.Int);
                command.Parameters.Add("@id", SqlDbType.Int);

                command.Parameters["@documentTypeId"].Value = SqlDocumentTypeDAO.GetDocumentType(document.Type.Id);
                command.Parameters["@documentCode"].Value = document.DocumentCode;
                command.Parameters["@title"].Value = document.Title;
                command.Parameters["@content"].Value = document.Text;
                command.Parameters["@creationDate"].Value = document.CreationDate;
                if (document.Receiver == null)
                {
                    command.Parameters["@receiverId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@receiverId"].Value = document.Receiver.Id;
                }
                if (document.Sender == null)
                {
                    command.Parameters["@senderId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@senderId"].Value = document.Sender.Id;
                }
                command.Parameters["@id"].Value = document.Id;
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteDocument(int id)
        {
            string sqlExpression = "DELETE FROM Document WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;
                command.ExecuteNonQuery();
            }
        }
    }
}
