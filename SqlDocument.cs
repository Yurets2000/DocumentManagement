﻿using System;
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
    public class SqlDocument
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<Document> GetAllDocuments()
        {
            List<Document> documents = new List<Document>();
            string sqlExpression = "SELECT * FROM Document WHERE Deleted = 0";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Company receiver = null;
                        Company sender = null;                     
                        int id = (int)reader["Id"];
                        int documentTypeId = (int)reader["DocumentTypeId"];
                        DocumentType type = SqlDocumentType.GetDocumentType(documentTypeId);
                        int documentCode = (int)reader["DocumentCode"];
                        string title = (string)reader["Title"];
                        string content = (string)reader["Content"];
                        DateTime creationDate = (DateTime)reader["CreationDate"];
                        bool senderConfirm = (bool)reader["SenderConfirm"];
                        bool receiverConfirm = (bool)reader["ReceiverConfirm"];
                        if(reader["ReceiverId"] != DBNull.Value)
                        {
                            int receiverId = (int)reader["ReceiverId"];
                            receiver = new Company()
                            {
                                Id = receiverId,
                                InitState = InitializationState.INITIALIZATION_NEEDED
                            };
                        }
                        if (reader["SenderId"] != DBNull.Value)
                        {
                            int senderId = (int)reader["SenderId"];
                            sender = new Company()
                            {
                                Id = senderId,
                                InitState = InitializationState.INITIALIZATION_NEEDED
                            };
                        }
                        Document document = new Document(type, documentCode, title)
                        {
                            Id = id,
                            Text = content,
                            CreationDate = creationDate,
                            ReceiverConfirm = receiverConfirm,
                            SenderConfirm = senderConfirm,
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
            string sqlExpression = "SELECT * FROM Document WHERE Id = @id AND Deleted = 0";
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
                        Company receiver = null;
                        Company sender = null;
                        int documentTypeId = (int)reader["DocumentTypeId"];
                        DocumentType type = SqlDocumentType.GetDocumentType(documentTypeId);
                        int documentCode = (int)reader["DocumentCode"];
                        string title = (string)reader["Title"];
                        string content = reader["Content"].GetType() == typeof(DBNull) ? null : (string)reader["Content"];
                        DateTime creationDate = (DateTime)reader["CreationDate"];
                        bool senderConfirm = (bool)reader["SenderConfirm"];
                        bool receiverConfirm = (bool)reader["ReceiverConfirm"];
                        if (reader["ReceiverId"] != DBNull.Value)
                        {
                            int receiverId = (int)reader["ReceiverId"];
                            receiver = new Company()
                            {
                                Id = receiverId,
                                InitState = InitializationState.INITIALIZATION_NEEDED
                            };
                        }
                        if (reader["SenderId"] != DBNull.Value)
                        {
                            int senderId = (int)reader["SenderId"];
                            sender = new Company()
                            {
                                Id = senderId,
                                InitState = InitializationState.INITIALIZATION_NEEDED
                            };
                        }
                        document = new Document(type, documentCode, title)
                        {
                            Id = id,
                            Text = content,
                            CreationDate = creationDate,
                            ReceiverConfirm = receiverConfirm,
                            SenderConfirm = senderConfirm,
                            Receiver = receiver,
                            Sender = sender
                        };
                    }
                }
            }
            return document;
        }

        public static void AddDocument(Document document)
        {
            string sqlExpression = "INSERT INTO Document(Id, DocumentTypeId, DocumentCode, Title, Content, CreationDate, ReceiverId, SenderId) VALUES (@id, @documentTypeId, @documentCode, @title, @content, @creationDate, @receiverId, @senderId)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters.Add("@documentTypeId", SqlDbType.Int);
                command.Parameters.Add("@documentCode", SqlDbType.Int);
                command.Parameters.Add("@title", SqlDbType.Text);
                command.Parameters.Add("@content", SqlDbType.Text);
                command.Parameters.Add("@creationDate", SqlDbType.Date);
                command.Parameters.Add("@receiverId", SqlDbType.Int);
                command.Parameters.Add("@senderId", SqlDbType.Int);

                command.Parameters["@id"].Value = document.Id;
                command.Parameters["@documentCode"].Value = document.DocumentCode;
                command.Parameters["@title"].Value = document.Title;
                command.Parameters["@content"].Value = document.Text;
                command.Parameters["@creationDate"].Value = document.CreationDate;
                if(document.Type == null)
                {
                    command.Parameters["@documentTypeId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@documentTypeId"].Value = document.Type.Id;
                }
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
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateDocument(Document document)
        {
            string sqlExpression = "UPDATE Document SET DocumentTypeId = @documentTypeId, DocumentCode = @documentCode, Title = @title, Content = @content, CreationDate = @creationDate, SenderConfirm = @senderConfirm, ReceiverConfirm = @receiverConfirm, ReceiverId = @receiverId, SenderId = @senderId WHERE Id = @id";
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
                command.Parameters.Add("@senderConfirm", SqlDbType.Bit);
                command.Parameters.Add("@receiverConfirm", SqlDbType.Bit);
                command.Parameters.Add("@senderId", SqlDbType.Int);
                command.Parameters.Add("@id", SqlDbType.Int);

                command.Parameters["@documentCode"].Value = document.DocumentCode;
                command.Parameters["@title"].Value = document.Title;
                command.Parameters["@content"].Value = document.Text;
                command.Parameters["@creationDate"].Value = document.CreationDate;
                command.Parameters["@senderConfirm"].Value = document.SenderConfirm;
                command.Parameters["@receiverConfirm"].Value = document.ReceiverConfirm;
                if (document.Type == null)
                {
                    command.Parameters["@documentTypeId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@documentTypeId"].Value = document.Type.Id;
                }
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
            string sqlExpression = "UPDATE Document SET Deleted = 1 WHERE Id = @id";
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
            string sqlExpression = "SELECT COALESCE(MAX(Id), 0) FROM Document";
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
