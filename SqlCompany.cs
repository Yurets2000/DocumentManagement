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
    public class SqlCompany
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<Company> GetAllCompanies()
        {
            List<Company> companies = new List<Company>();
            string sqlExpression = "SELECT * FROM Company WHERE Deleted = 0";
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
                        int companyTypeId = (int)reader["CompanyTypeId"];
                        string name = (string)reader["CompanyName"];
                        string address = (string)reader["CompanyAddress"];
                        Director director = new Director()
                        {
                            InitState = InitializationState.INITIALIZATION_NEEDED
                        };
                        Chancery chancery = new Chancery()
                        {
                            InitState = InitializationState.INITIALIZATION_NEEDED
                        };
                        CompanyType companyType = SqlCompanyType.GetCompanyType(companyTypeId);
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
            string sqlExpression = "SELECT * FROM Company WHERE Id = @id AND Deleted = 0";
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
                        int companyTypeId = (int)reader["CompanyTypeId"];
                        string name = (string)reader["CompanyName"];
                        string address = (string)reader["CompanyAddress"];
                        Director director = new Director()
                        {
                            InitState = InitializationState.INITIALIZATION_NEEDED
                        };
                        Chancery chancery = new Chancery()
                        {
                            InitState = InitializationState.INITIALIZATION_NEEDED
                        };
                        CompanyType companyType = SqlCompanyType.GetCompanyType(companyTypeId);
                        company = new Company(director, companyType, chancery, name, address)
                        {
                            Id = id
                        };
                    }
                }
            }
            return company;
        }

        public static void AddCompany(Company company)
        {
            string sqlExpression = "INSERT INTO Company(Id, CompanyTypeId, CompanyName, CompanyAddress) VALUES(@id, @companyTypeId, @companyName, @companyAddress)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters.Add("@companyTypeId", SqlDbType.Int);
                command.Parameters.Add("@companyName", SqlDbType.VarChar);
                command.Parameters.Add("@companyAddress", SqlDbType.VarChar);

                command.Parameters["@id"].Value = company.Id;
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
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateCompany(Company company)
        {
            string sqlExpression = "UPDATE Company SET CompanyTypeId = @companyTypeId, CompanyName = @companyName, CompanyAddress = @companyAddress WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@companyTypeId", SqlDbType.Int);
                command.Parameters.Add("@companyName", SqlDbType.VarChar);
                command.Parameters.Add("@companyAddress", SqlDbType.VarChar);
                command.Parameters.Add("@id", SqlDbType.Int);
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
            string sqlExpression = "UPDATE Company SET Deleted = 1 WHERE Id = @id";
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
            string sqlExpression = "SELECT COALESCE(MAX(Id), 0) FROM Company";
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
