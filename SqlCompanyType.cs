using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DocumentManagement
{
    public class SqlCompanyType
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<CompanyType> GetAllCompanyTypes()
        {
            List<CompanyType> companyTypes = new List<CompanyType>();
            string sqlExpression = "SELECT * FROM CompanyType WHERE Deleted = 0";
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
                        string name = (string)reader["Name"];
                        CompanyType companyType = new CompanyType(name)
                        {
                            Id = id
                        };
                        companyTypes.Add(companyType);
                    }
                }
            }
            return companyTypes;
        }

        public static CompanyType GetCompanyType(int id)
        {
            CompanyType companyType = null;
            string sqlExpression = "SELECT * FROM CompanyType WHERE Id = @id AND Deleted = 0";
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
                        string name = (string)reader["Name"];
                        companyType = new CompanyType(name)
                        {
                            Id = id
                        };
                    }
                }
            }
            return companyType;
        }

        public static void AddCompanyType(CompanyType companyType)
        {
            string sqlExpression = "INSERT INTO CompanyType(Id, Name) VALUES (@id, @name)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters.Add("@name", SqlDbType.Text);
                command.Parameters["@id"].Value = companyType.Id;
                command.Parameters["@name"].Value = companyType.type;
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateCompanyType(CompanyType companyType)
        {
            string sqlExpression = "UPDATE CompanyType SET Name = @name WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@name", SqlDbType.VarChar);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@name"].Value = companyType.type;
                command.Parameters["@id"].Value = companyType.Id;
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteCompanyType(int id)
        {
            string sqlExpression = "DELETE FROM CompanyType WHERE Id = @id";
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
            string sqlExpression = "SELECT COALESCE(MAX(Id), 0) FROM CompanyType";
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
