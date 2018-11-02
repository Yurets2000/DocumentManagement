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
    public class SqlSecretary
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<Secretary> GetAllSecretaries()
        {
            List<Secretary> secretaries = new List<Secretary>();
            string sqlExpression = "SELECT * FROM Secretary WHERE Deleted = 0";
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
                        int personId = (int)reader["PersonId"];
                        int companyId = (int)reader["CompanyId"];
                        int markerId = (int)reader["MarkerId"];
                        int salary = (int)reader["Salary"];
                        Person person = SqlPerson.GetPerson(personId);
                        Marker marker = SqlMarker.GetMarker(markerId);
                        Company company = new Company()
                        {
                            Id = companyId,
                            InitCompany = true
                        };
                        List<Document> pendingDocuments = SqlSecretaryPendingDocuments.GetPendingDocuments(id);
                        List<Document> createdDocuments = SqlSecretaryCreatedDocuments.GetCurrentDocuments(id);

                        Secretary secretary = new Secretary(person, company, salary)
                        {
                            EmployeeId = id,
                            Marker = marker,
                            PendingDocuments = pendingDocuments,
                            CreatedDocuments = createdDocuments
                        };

                        secretaries.Add(secretary);
                    }
                }
            }
            return secretaries;
        }

        public static List<Secretary> GetCompanySecretaries(int companyId)
        {
            List<Secretary> secretaries = new List<Secretary>();
            string sqlExpression = "SELECT * FROM Secretary WHERE CompanyId = @companyId AND Deleted = 0";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@companyId", SqlDbType.Int);
                command.Parameters["@companyId"].Value = companyId;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        int personId = (int)reader["PersonId"];
                        int markerId = (int)reader["MarkerId"];
                        int salary = (int)reader["Salary"];
                        Person person = SqlPerson.GetPerson(personId);
                        Marker marker = SqlMarker.GetMarker(markerId);
                        Company company = new Company()
                        {
                            Id = companyId,
                            InitCompany = true
                        };
                        List<Document> pendingDocuments = SqlSecretaryPendingDocuments.GetPendingDocuments(id);
                        List<Document> createdDocuments = SqlSecretaryCreatedDocuments.GetCurrentDocuments(id);

                        Secretary secretary = new Secretary(person, company, salary)
                        {
                            EmployeeId = id,
                            Marker = marker,
                            PendingDocuments = pendingDocuments,
                            CreatedDocuments = createdDocuments
                        };

                        secretaries.Add(secretary);
                    }
                }
            }
            return secretaries;
        }

        public static Secretary GetSecretaryByPerson(int personId)
        {
            Secretary secretary = null;
            string sqlExpression = "SELECT * FROM Secretary WHERE PersonId = @personId AND Deleted = 0";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@personId", SqlDbType.Int);
                command.Parameters["@personId"].Value = personId;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        int companyId = (int)reader["CompanyId"];
                        int markerId = (int)reader["MarkerId"];
                        int salary = (int)reader["Salary"];
                        Person person = SqlPerson.GetPerson(personId);
                        Marker marker = SqlMarker.GetMarker(markerId);
                        Company company = new Company()
                        {
                            Id = companyId,
                            InitCompany = true
                        };
                        List<Document> pendingDocuments = SqlSecretaryPendingDocuments.GetPendingDocuments(id);
                        List<Document> createdDocuments = SqlSecretaryCreatedDocuments.GetCurrentDocuments(id);

                        secretary = new Secretary(person, company, salary)
                        {
                            EmployeeId = id,
                            Marker = marker,
                            PendingDocuments = pendingDocuments,
                            CreatedDocuments = createdDocuments
                        };
                    }
                }
            }
            return secretary;
        }

        public static Secretary GetSecretary(int id)
        {
            Secretary secretary = null;
            string sqlExpression = "SELECT * FROM Secretary WHERE Id = @id AND Deleted = 0";
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
                        int personId = (int)reader["PersonId"];
                        int companyId = (int)reader["CompanyId"];
                        int markerId = (int)reader["MarkerId"];
                        int salary = (int)reader["Salary"];
                        Person person = SqlPerson.GetPerson(personId);
                        Marker marker = SqlMarker.GetMarker(markerId);
                        Company company = new Company()
                        {
                            Id = companyId,
                            InitCompany = true
                        };
                        List<Document> pendingDocuments = SqlSecretaryPendingDocuments.GetPendingDocuments(id);
                        List<Document> createdDocuments = SqlSecretaryCreatedDocuments.GetCurrentDocuments(id);

                        secretary = new Secretary(person, company, salary)
                        {
                            EmployeeId = id,
                            Marker = marker,
                            PendingDocuments = pendingDocuments,
                            CreatedDocuments = createdDocuments
                        };
                    }
                }
            }
            return secretary;
        }

        public static int AddSecretary(Secretary secretary)
        {
            int id = -1;
            string sqlExpression = "INSERT INTO Secretary(PersonId, CompanyId, MarkerId, Salary) output INSERTED.Id VALUES (@personId, @companyId, @markerId, @salary)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@personId", SqlDbType.Int);
                command.Parameters.Add("@companyId", SqlDbType.Int);
                command.Parameters.Add("@markerId", SqlDbType.Int);
                command.Parameters.Add("@salary", SqlDbType.Int);
                command.Parameters["@personId"].Value = secretary.Id; 
                if (secretary.Company == null)
                {
                    command.Parameters["@companyId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@companyId"].Value = secretary.Company.Id;
                }
                if(secretary.Marker == null)
                {
                    command.Parameters["@markerId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@markerId"].Value = secretary.Marker.Id;
                }
                command.Parameters["@salary"].Value = secretary.Salary;
                id = (int)command.ExecuteScalar();
            }
            return id;
        }

        public static void UpdateSecretary(Secretary secretary)
        {
            string sqlExpression = "UPDATE Secretary SET PersonId = @personId, CompanyId = @companyId, MarkerId = @markerId, Salary = @salary WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@personId", SqlDbType.Int);
                command.Parameters.Add("@companyId", SqlDbType.Int);
                command.Parameters.Add("@markerId", SqlDbType.Int);
                command.Parameters.Add("@salary", SqlDbType.Int);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@personId"].Value = secretary.Id;     
                if (secretary.Company == null)
                {
                    command.Parameters["@companyId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@companyId"].Value = secretary.Company.Id;
                }
                if (secretary.Marker == null)
                {
                    command.Parameters["@markerId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@markerId"].Value = secretary.Marker.Id;
                }
                command.Parameters["@salary"].Value = secretary.Salary;
                command.Parameters["@id"].Value = secretary.EmployeeId;
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteSecretary(int id)
        {
            string sqlExpression = "UPDATE Secretary SET Deleted = 1 WHERE Id = @id";
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
