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


    public class SqlMarker
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<Marker> GetAllMarkers()
        {
            List<Marker> markers = new List<Marker>();
            string sqlExpression = "SELECT * FROM Marker WHERE Deleted = 0";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int Id = (int)reader["Id"];
                        int ColorCode = (int)reader["ColorCode"];
                        Marker marker = new Marker((Marker.Color)ColorCode)
                        {
                            Id = Id
                        };
                        markers.Add(marker);
                    }
                }
            }
            return markers;
        }

        public static Marker GetMarker(int id)
        {
            Marker marker = null;
            string sqlExpression = "SELECT * FROM Marker WHERE Id = @id AND Deleted = 0";
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

        public static void AddMarker(Marker marker)
        {
            string sqlExpression = "INSERT INTO Marker(Id, ColorCode) VALUES (@id, @colorCode)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters.Add("@colorCode", SqlDbType.Int);
                command.Parameters["@id"].Value = marker.Id;
                command.Parameters["@colorCode"].Value = (int)marker.markerColor;
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteMarker(int id)
        {
            string sqlExpression = "UPDATE Marker SET Deleted = 1 WHERE Id = @id";
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
            string sqlExpression = "SELECT COALESCE(MAX(Id), 0) FROM Marker";
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
