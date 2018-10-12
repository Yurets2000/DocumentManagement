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
    public class SqlCompanyDAO
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<Company> GetAllCompanies()
        {
            List<Company> companies = new List<Company>();
            string sqlExpression = "SELECT * FROM Company";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand expression = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = expression.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        int directorId = (int)reader["DirectorId"];
                        int chanceryId = (int)reader["ChanceryId"];
                        int companyTypeId = (int)reader["CompanyTypeId"];
                        string name = (string)reader["CompanyName"];
                        string address = (string)reader["CompanyAddress"];
                        Director director = new Director()
                        {
                            EmployeeId = directorId,
                            InitDirector = true
                        };
                        Chancery chancery = new Chancery()
                        {
                            Id = chanceryId,
                            InitChancery = true
                        };
                        CompanyType companyType = SqlCompanyTypeDAO.GetCompanyType(companyTypeId);
                        Company company = new Company(director, companyType, chancery, name, address)
                        {
                            Id = id
                        };
                        companies.Add(company);
                    }
                }
            }
            return companies;
        }

        public static Company GetCompany(int id)
        {
            Company company = null;
            string sqlExpression = "SELECT * FROM Company WHERE Id = @id";
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
                        int directorId = (int)reader["DirectorId"];
                        int chanceryId = (int)reader["ChanceryId"];
                        int companyTypeId = (int)reader["CompanyTypeId"];
                        string name = (string)reader["CompanyName"];
                        string address = (string)reader["CompanyAddress"];
                        Director director = new Director()
                        {
                            EmployeeId = directorId,
                            InitDirector = true
                        };
                        Chancery chancery = new Chancery()
                        {
                            Id = chanceryId,
                            InitChancery = true
                        };
                        CompanyType companyType = SqlCompanyTypeDAO.GetCompanyType(companyTypeId);
                        company = new Company(director, companyType, chancery, name, address)
                        {
                            Id = id
                        };
                    }
                }
            }
            return company;
        }

        public static int AddCompany(Company company)
        {
            int id = -1;
            string sqlExpression = "INSERT INTO Company(DirectorId, ChanceryId, CompanyTypeId, CompanyName, CompanyAddress) output INSERTED.Id VALUES(@directorId, @chanceryId, @companyTypeId, @companyName, @companyAddress)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@directorId", SqlDbType.Int);
                command.Parameters.Add("@chanceryId", SqlDbType.Int);
                command.Parameters.Add("@companyTypeId", SqlDbType.Int);
                command.Parameters.Add("@companyName", SqlDbType.VarChar);
                command.Parameters.Add("@companyAddress", SqlDbType.VarChar);
                if(company.Director == null)
                {
                    command.Parameters["@directorId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@directorId"].Value = company.Director.EmployeeId;
                }
                if(company.CompanyChancery == null)
                {
                    command.Parameters["@chanceryId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@chanceryId"].Value = company.CompanyChancery.Id;
                }
                if (company.Type == null)
                {
                    command.Parameters["@companyTypeId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@companyTypeId"].Value = company.Type.Id;
                }
                command.Parameters["@companyName"].Value = company.Name;
                command.Parameters["@companyAddress"].Value = company.Address;
                id = (int)command.ExecuteScalar();
            }
            return id;
        }

        public static void UpdateCompany(Company company)
        {
            string sqlExpression = "UPDATE Company SET DirectorId = @directorId, ChanceryId = @chanceryId, CompanyTypeId = @companyTypeId, CompanyName = @companyName, CompanyAddress = @companyAddress WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@directorId", SqlDbType.Int);
                command.Parameters.Add("@chanceryId", SqlDbType.Int);
                command.Parameters.Add("@companyTypeId", SqlDbType.Int);
                command.Parameters.Add("@companyName", SqlDbType.VarChar);
                command.Parameters.Add("@companyAddress", SqlDbType.VarChar);
                command.Parameters.Add("@id", SqlDbType.Int);
                if (company.Director == null)
                {
                    command.Parameters["@directorId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@directorId"].Value = company.Director.EmployeeId;
                }
                if (company.CompanyChancery == null)
                {
                    command.Parameters["@chanceryId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@chanceryId"].Value = company.CompanyChancery.Id;
                }
                if (company.Type == null)
                {
                    command.Parameters["@companyTypeId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@companyTypeId"].Value = company.Type.Id;
                }
                command.Parameters["@companyName"].Value = company.Name;
                command.Parameters["@companyAddress"].Value = company.Address;
                command.Parameters["@id"].Value = company.Id;
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteCompany(int id)
        {
            string sqlExpression = "DELETE FROM Company WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;
                command.ExecuteNonQuery();
            }
        }

        public static void UncheckCompanyDirectorConstraint()
        {
            string sqlExpression = "ALTER TABLE Company NOCHECK CONSTRAINT Company_Director";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

        public static void CheckCompanyDirectorConstraint()
        {
            string sqlExpression = "ALTER TABLE Company CHECK CONSTRAINT Company_Director";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
