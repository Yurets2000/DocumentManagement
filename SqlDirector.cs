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
    public class SqlDirector
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<Director> GetAllDirectors()
        {
            List<Director> directors = new List<Director>();
            string sqlExpression = "SELECT * FROM Director WHERE Deleted = 0";
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
                        Person person = null;
                        if(reader["PersonId"] != DBNull.Value)
                        {
                            int personId = (int)reader["PersonId"];
                            person = SqlPerson.GetPerson(personId);
                        }
                        Company company = new Company
                        {
                                Id = SqlCompany_Director.GetCompanyByDirector(id),
                                InitState = InitializationState.INITIALIZATION_NEEDED
                        };
                        byte[] signature = (byte[])reader["DirectorSignature"];
                        int salary = (int)reader["Salary"];
                        List<Document> pendingDocuments = SqlDirectorPendingDocuments.GetPendingDocuments(id);
                        Director director = new Director(person, company, signature, salary)
                        {
                            EmployeeId = id,
                            PendingDocuments = pendingDocuments
                        };
                        directors.Add(director);
                    }
                }
            }
            return directors;
        }

        public static Director GetDirector(int id)
        {
            Director director = null; 
            string sqlExpression = "SELECT * FROM Director WHERE Id = @id AND Deleted = 0";
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
                        Person person = null;
                        byte[] signature = (byte[])reader["DirectorSignature"];
                        int salary = (int)reader["Salary"];
                        if (reader["PersonId"] != DBNull.Value)
                        {
                            int personId = (int)reader["PersonId"];
                            person = SqlPerson.GetPerson(personId);
                        }
                        Company company = new Company
                        {
                            Id = SqlCompany_Director.GetCompanyByDirector(id),
                            InitState = InitializationState.INITIALIZATION_NEEDED
                        };
                        List<Document> pendingDocuments = SqlDirectorPendingDocuments.GetPendingDocuments(id);
                        director = new Director(person, company, signature, salary)
                        {
                            EmployeeId = id,
                            PendingDocuments = pendingDocuments
                        };
                    }
                }
            }
            return director;
        }

        public static Director GetDirectorByPerson(int personId)
        {
            Director director = null;
            string sqlExpression = "SELECT * FROM Director WHERE PersonId = @personId AND Deleted = 0";
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
                        director = new Director
                        {
                            EmployeeId = id,
                            InitState = InitializationState.INITIALIZATION_NEEDED
                        };
                    }
                }
            }
            return director;
        }

        public static void AddDirector(Director director)
        {
            string sqlExpression = "INSERT INTO Director(Id, PersonId, DirectorSignature, Salary) VALUES(@id, @personId, @signature, @salary)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters.Add("@personId", SqlDbType.Int);
                command.Parameters.Add("@signature", SqlDbType.VarBinary);
                command.Parameters.Add("@salary", SqlDbType.Int);

                command.Parameters["@id"].Value = director.EmployeeId;
                command.Parameters["@personId"].Value = director.Id;
                command.Parameters["@signature"].Value = director.Signature;
                command.Parameters["@salary"].Value = director.Salary;
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateDirector(Director director)
        {
            string sqlExpression = "UPDATE Director SET PersonId = @personId, DirectorSignature = @signature, Salary = @salary WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@personId", SqlDbType.Int);
                command.Parameters.Add("@signature", SqlDbType.VarBinary);
                command.Parameters.Add("@salary", SqlDbType.Int);
                command.Parameters.Add("@id", SqlDbType.Int);

                command.Parameters["@personId"].Value = director.Id;
                command.Parameters["@signature"].Value = director.Signature;
                command.Parameters["@salary"].Value = director.Salary;
                command.Parameters["@id"].Value = director.EmployeeId;
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteDirector(int id)
        {
            string sqlExpression = "UPDATE Director SET Deleted = 1 WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;
                SqlCompany_Director.DeleteFromDirector(id);
                command.ExecuteNonQuery();
            }
        }

        public static int GetMaxId()
        {
            int max = 0;
            string sqlExpression = "SELECT COALESCE(MAX(Id), 0) FROM Director";
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
