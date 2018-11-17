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
                        Company company = new Company
                        {
                            Id = SqlCompany_Chancery.GetCompanyByChancery(id),
                            InitState = InitializationState.INITIALIZATION_NEEDED
                        };
                        List<Document> archive = SqlArchive.GetArchivedDocuments(id);
                        List<Document> pendingDocuments = SqlPendingDocuments.GetPendingDocuments(id);
                        List<Secretary> secretaries = SqlSecretary.GetCompanySecretaries(company.Id);
                        MainSecretary mainSecretary = null;
                        if (SqlMainSecretary.GetCompanyMainSecretary(company.Id) != null)
                        {
                            mainSecretary = new MainSecretary
                            {
                                EmployeeId = SqlMainSecretary.GetCompanyMainSecretary(company.Id).EmployeeId,
                                InitState = InitializationState.INITIALIZATION_NEEDED
                            };
                        }
                        Chancery chancery = new Chancery(company)
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
                        Company company = new Company
                        {
                            Id = SqlCompany_Chancery.GetCompanyByChancery(id),
                            InitState = InitializationState.INITIALIZATION_NEEDED
                        };
                        List<Document> archive = SqlArchive.GetArchivedDocuments(id);
                        List<Document> pendingDocuments = SqlPendingDocuments.GetPendingDocuments(id);
                        List<Secretary> secretaries = SqlSecretary.GetCompanySecretaries(company.Id);
                        MainSecretary mainSecretary = SqlMainSecretary.GetCompanyMainSecretary(company.Id);
                        chancery = new Chancery(company)
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

        public static void AddChancery(Chancery chancery)
        {
            string sqlExpression = "INSERT INTO Chancery(Id) VALUES (@id)";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@id", SqlDbType.Int);
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

        public static int GetMaxId()
        {
            int max = 0;
            string sqlExpression = "SELECT COALESCE(MAX(Id), 0) FROM Chancery";
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
