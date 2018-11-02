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
    public class SqlChancery
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<Chancery> GetAllChanceries()
        {
            List<Chancery> chanceries = new List<Chancery>();
            string sqlExpression = "SELECT * FROM Chancery WHERE Deleted = 0";
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
                        int companyId = (int)reader["CompanyId"];
                        Company chanceryCompany = new Company()
                        {
                            Id = companyId,
                            InitCompany = true
                        };
                        List<Document> archive = SqlArchive.GetArchivedDocuments(id);
                        List<Document> pendingDocuments = SqlPendingDocuments.GetPendingDocuments(id);
                        List<Secretary> secretaries = SqlSecretary.GetCompanySecretaries(companyId);
                        MainSecretary mainSecretary = SqlMainSecretary.GetCompanyMainSecretary(companyId);
                        Chancery chancery = new Chancery(chanceryCompany)
                        {
                            Id = id,
                            Archive = archive,
                            PendingDocuments = pendingDocuments,
                            Secretaries = secretaries,
                            MainSecretary = mainSecretary
                        };
                        chanceries.Add(chancery);
                    }
                }
            }
            return chanceries;
        }

        public static Chancery GetChancery(int id)
        {
            Chancery chancery = null;
            string sqlExpression = "SELECT * FROM Chancery WHERE Id = @id AND Deleted = 0";
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
                        int companyId = (int)reader["CompanyId"];
                        Company chanceryCompany = new Company()
                        {
                            Id = companyId,
                            InitCompany = true
                        };
                        List<Document> archive = SqlArchive.GetArchivedDocuments(id);
                        List<Document> pendingDocuments = SqlPendingDocuments.GetPendingDocuments(id);
                        List<Secretary> secretaries = SqlSecretary.GetCompanySecretaries(companyId);
                        MainSecretary mainSecretary = SqlMainSecretary.GetCompanyMainSecretary(companyId);
                        chancery = new Chancery(chanceryCompany)
                        {
                            Id = id,
                            Archive = archive,
                            PendingDocuments = pendingDocuments,
                            Secretaries = secretaries,
                            MainSecretary = mainSecretary
                        };
                    }
                }
            }
            return chancery;
        }

        public static int AddChancery(Chancery chancery)
        {
            int id = -1;
            string sqlExpression = "INSERT INTO Chancery(CompanyId) output INSERTED.Id VALUES (@companyId)";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@companyId", SqlDbType.Int);
                if(chancery.ChanceryCompany == null)
                {
                    command.Parameters["@companyId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@companyId"].Value = chancery.ChanceryCompany.Id;
                }
                id = (int) command.ExecuteScalar();
            }
            return id;
        }

        public static void UpdateChancery(Chancery chancery)
        {       
            string sqlExpression = "UPDATE Chancery SET CompanyId = @companyId WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@companyId", SqlDbType.Int);
                command.Parameters.Add("@id", SqlDbType.Int);
                if (chancery.ChanceryCompany == null)
                {
                    command.Parameters["@companyId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@companyId"].Value = chancery.ChanceryCompany.Id;
                }
                command.Parameters["@id"].Value = chancery.Id;
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteChancery(int id)
        {
            string sqlExpression = "UPDATE Chancery SET Deleted = 1 WHERE Id = @id";
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
