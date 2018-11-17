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
    public class SqlCompany_Chancery
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static int GetChanceryFromCompany(int companyId)
        {
            int chanceryId = -1;
            string sqlExpression = "SELECT * FROM Company_Chancery WHERE CompanyId = @companyId AND Deleted = 0";
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
                        chanceryId = reader["ChanceryId"] as int? ?? default(int);
                    }
                }
            }
            return chanceryId;
        }

        public static int GetCompanyByChancery(int chanceryId)
        {
            int companyId = -1;
            string sqlExpression = "SELECT * FROM Company_Chancery WHERE ChanceryId = @chanceryId AND Deleted = 0";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@chanceryId", SqlDbType.Int);
                command.Parameters["@chanceryId"].Value = chanceryId;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        companyId = reader["CompanyId"] as int? ?? default(int);
                    }
                }
            }
            return companyId;
        }

        public static void AddRecord(int? companyId, int? chanceryId)
        {
            string sqlExpression = "INSERT INTO Company_Chancery(CompanyId, ChanceryId) VALUES (@companyId, @chanceryId)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@companyId", SqlDbType.Int);
                command.Parameters.Add("@chanceryId", SqlDbType.Int);
                if (companyId == null)
                {
                    command.Parameters["@companyId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@companyId"].Value = companyId;
                }
                if(chanceryId == null)
                {
                    command.Parameters["@chanceryId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@chanceryId"].Value = chanceryId;
                }
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteFromCompany(int companyId)
        {
            string sqlExpression = "UPDATE Company_Chancery SET Deleted = 1 WHERE companyId = @companyId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@companyId", SqlDbType.Int);
                command.Parameters["@companyId"].Value = companyId;
                command.ExecuteNonQuery();
            }
        }
    }
}
