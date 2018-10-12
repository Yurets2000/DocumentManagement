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


    public class SqlMarkerDAO
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static Marker GetMarker(int id)
        {
            Marker marker = null;
            string sqlExpression = "SELECT * FROM Marker WHERE Id = @id";
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
                        int ColorCode = (int)reader["ColorCode"];
                        marker = new Marker((Marker.Color)ColorCode)
                        {
                            Id = id
                        };                        
                    }
                }
            }
            return marker;
        }

        public static int AddMarker(Marker marker)
        {
            int id = -1;
            string sqlExpression = "INSERT INTO Marker(ColorCode) output INSERTED.Id VALUES (@colorCode)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@colorCode", SqlDbType.Int);
                command.Parameters["@colorCode"].Value = (int)marker.markerColor;
                id = (int)command.ExecuteScalar();
            }
            return id;
        }

        public static void DeleteMarker(int id)
        {
            string sqlExpression = "DELETE FROM Marker WHERE Id = @id";
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
