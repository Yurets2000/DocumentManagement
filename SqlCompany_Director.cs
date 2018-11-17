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
    public class SqlCompany_Director
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static int GetDirectorFromCompany(int companyId)
        {
            int directorId = -1;
            string sqlExpression = "SELECT * FROM Company_Director WHERE CompanyId = @companyId  AND Deleted = 0";
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
                        directorId = reader["DirectorId"] as int? ?? default(int);
                    }
                }
            }
            return directorId;
        }

        public static int GetCompanyByDirector(int directorId)
        {
            int companyId = -1;
            string sqlExpression = "SELECT * FROM Company_Director WHERE DirectorId = @directorId AND Deleted = 0";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@directorId", SqlDbType.Int);
                command.Parameters["@directorId"].Value = directorId;
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

        public static void AddRecord(int? companyId, int? directorId)
        {
            string sqlExpression = "INSERT INTO Company_Director(CompanyId, DirectorId) VALUES (@companyId, @directorId)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@companyId", SqlDbType.Int);
                command.Parameters.Add("@directorId", SqlDbType.Int);
                if(companyId == null)
                {
                    command.Parameters["@companyId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@companyId"].Value = companyId;
                }
                if(directorId == null)
                {
                    command.Parameters["@directorId"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters["@directorId"].Value = directorId;
                }            
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteFromCompany(int companyId)
        {
            string sqlExpression = "UPDATE Company_Director SET Deleted = 1 WHERE companyId = @companyId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@companyId", SqlDbType.Int);
                command.Parameters["@companyId"].Value = companyId;
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteFromDirector(int directorId)
        {
            string sqlExpression = "UPDATE Company_Director SET Deleted = 1 WHERE directorId = @directorId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@directorId", SqlDbType.Int);
                command.Parameters["@directorId"].Value = directorId;
                command.ExecuteNonQuery();
            }
        }
    }
}
