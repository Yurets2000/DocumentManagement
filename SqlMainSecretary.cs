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
    public class SqlMainSecretary
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<MainSecretary> GetAllMainSecretaries()
        {
            List<MainSecretary> mainSecretaries = new List<MainSecretary>();
            string sqlExpression = "SELECT * FROM MainSecretary WHERE Deleted = 0";
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
                        int salary = (int)reader["Salary"];
                        Person person = SqlPerson.GetPerson(personId);
                        Company company = new Company()
                        {
                            Id = companyId,
                            InitCompany = true
                        };
                        MainSecretary mainSecretary = new MainSecretary(person, company, salary)
                        {
                            EmployeeId = id
                        };
                        mainSecretaries.Add(mainSecretary);
                    }
                }
            }
            return mainSecretaries;
        }

        public static MainSecretary GetMainSecretary(int id)
        {
            MainSecretary mainSecretary = null;
            string sqlExpression = "SELECT * FROM MainSecretary WHERE Id = @id AND Deleted = 0";
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
                        int salary = (int)reader["Salary"];
                        Person person = SqlPerson.GetPerson(personId);
                        Company company = new Company()
                        {
                            Id = companyId,
                            InitCompany = true
                        };
                        mainSecretary = new MainSecretary(person, company, salary)
                        {
                            EmployeeId = id
                        };
                    }
                }
            }
            return mainSecretary;
        }

        public static MainSecretary GetCompanyMainSecretary(int companyId)
        {
            MainSecretary mainSecretary = null;
            string sqlExpression = "SELECT * FROM MainSecretary WHERE CompanyId = @companyId AND Deleted = 0";
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
                        int salary = (int)reader["Salary"];
                        Person person = SqlPerson.GetPerson(personId);
                        Company company = new Company()
                        {
                            Id = companyId,
                            InitCompany = true
                        };
                        mainSecretary = new MainSecretary(person, company, salary)
                        {
                            EmployeeId = id
                        };
                    }
                }
            }
            return mainSecretary;
        }

        public static MainSecretary GetMainSecretaryByPerson(int personId)
        {
            MainSecretary mainSecretary = null;
            string sqlExpression = "SELECT * FROM MainSecretary WHERE PersonId = @personId AND Deleted = 0";
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
                        int salary = (int)reader["Salary"];
                        Person person = SqlPerson.GetPerson(personId);
                        Company company = new Company()
                        {
                            Id = companyId,
                            InitCompany = true
                        };
                        mainSecretary = new MainSecretary(person, company, salary)
                        {
                            EmployeeId = id
                        };
                    }
                }
            }
            return mainSecretary;
        }

        public static int AddMainSecretary(MainSecretary mainSecretary)
        {
            int id = -1;
            string sqlExpression = "INSERT INTO MainSecretary(PersonId, CompanyId, Salary) output INSERTED.Id VALUES(@personId, @companyId, @salary)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@personId", SqlDbType.Int);
                command.Parameters.Add("@companyId", SqlDbType.Int);
                command.Parameters.Add("@salary", SqlDbType.Int);
                command.Parameters["@personId"].Value = mainSecretary.Id;
                if (mainSecretary.Company == null)
                {
                    command.Parameters["@companyId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@companyId"].Value = mainSecretary.Company.Id;
                }
                command.Parameters["@salary"].Value = mainSecretary.Salary;
                id = (int)command.ExecuteScalar();
            }
            return id;
        }

        public static void UpdateMainSecretary(MainSecretary mainSecretary)
        {
            string sqlExpression = "UPDATE MainSecretary SET PersonId = @personId, CompanyId = @companyId, Salary = @salary WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@personId", SqlDbType.Int);
                command.Parameters.Add("@companyId", SqlDbType.Int);
                command.Parameters.Add("@salary", SqlDbType.Int);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@personId"].Value = mainSecretary.Id;             
                if (mainSecretary.Company == null)
                {
                    command.Parameters["@companyId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@companyId"].Value = mainSecretary.Company.Id;
                }
                command.Parameters["@salary"].Value = mainSecretary.Salary;
                command.Parameters["@id"].Value = mainSecretary.EmployeeId;
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteMainSecretary(int id)
        {
            string sqlExpression = "UPDATE MainSecretary SET Deleted = 1 WHERE Id = @id";
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
