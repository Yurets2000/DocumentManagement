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
    public class SqlDirectorDAO
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<Director> GetAllDirectors()
        {
            List<Director> directors = new List<Director>();
            string sqlExpression = "SELECT * FROM Director";
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
                        Person person = SqlPersonDAO.GetPerson(personId);
                        Company company = new Company()
                        {
                            Id = companyId,
                            InitCompany = true
                        };
                        Director director = new Director(person, company, salary)
                        {
                            EmployeeId = id
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
            string sqlExpression = "SELECT * FROM Director WHERE Id = @id";
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
                        Person person = SqlPersonDAO.GetPerson(personId);
                        Company company = new Company()
                        {
                            Id = companyId,
                            InitCompany = true
                        };
                        director = new Director(person, company, salary)
                        {
                            EmployeeId = id
                        };
                    }
                }
            }
            return director;
        }

        public static Director GetCompanyDirector(int companyId)
        {
            Director director = null;
            string sqlExpression = "SELECT * FROM Director WHERE CompanyId = @companyId";
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
                        Person person = SqlPersonDAO.GetPerson(personId);
                        Company company = new Company()
                        {
                            Id = companyId,
                            InitCompany = true
                        };
                        director = new Director(person, company, salary)
                        {
                            EmployeeId = id
                        };
                    }
                }
            }
            return director;
        }

        public static int AddDirector(Director director)
        {
            int id = -1;
            string sqlExpression = "INSERT INTO Director(PersonId, CompanyId, Salary) output INSERTED.Id VALUES(@personId, @companyId, @salary)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@personId", SqlDbType.Int);
                command.Parameters.Add("@companyId", SqlDbType.Int);
                command.Parameters.Add("@salary", SqlDbType.Int);
                command.Parameters["@personId"].Value = director.Id;
                if (director.Company == null)
                {
                    command.Parameters["@companyId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@companyId"].Value = director.Company.Id;
                }
                command.Parameters["@salary"].Value = director.Salary;
                id = (int)command.ExecuteScalar();
            }
            return id;
        }

        public static void UpdateDirector(Director director)
        {
            string sqlExpression = "UPDATE Director SET PersonId = @personId, CompanyId = @companyId, Salary = @salary WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@personId", SqlDbType.Int);
                command.Parameters.Add("@companyId", SqlDbType.Int);
                command.Parameters.Add("@salary", SqlDbType.Int);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@personId"].Value = director.Id;
                if (director.Company == null)
                {
                    command.Parameters["@companyId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@companyId"].Value = director.Company.Id;
                }
                command.Parameters["@salary"].Value = director.Salary;
                command.Parameters["@id"].Value = director.EmployeeId;
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteDirector(int id)
        {
            string sqlExpression = "DELETE FROM Director WHERE Id = @id";
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
