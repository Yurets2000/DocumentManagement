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
    public class SqlPerson
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<Person> GetAllPersons()
        {
            List<Person> persons = new List<Person>();
            string sqlExpression = "SELECT * FROM Person WHERE Deleted = 0";
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
                        string surname = (string)reader["Surname"];
                        int age = (int)reader["Age"];
                        bool working = (bool)reader["Working"];
                        Person person = new Person(name, surname, age)
                        {
                            Id = id,
                            Working = working
                        };
                        persons.Add(person);
                    }
                }
            }
            return persons;
        }

        public static Person GetPerson(int id)
        {
            Person person = null;
            string sqlExpression = "SELECT * FROM Person WHERE Id = @id AND Deleted = 0";
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
                        string surname = (string)reader["Surname"];
                        int age = (int)reader["Age"];
                        bool working = (bool)reader["Working"];
                        person = new Person(name, surname, age)
                        {
                            Id = id,
                            Working = working
                        };
                    }
                }
            }
            return person;
        }

        public static void UpdatePerson(Person person)
        {
            string sqlExpression = "UPDATE Person SET Name = @name, Surname = @surname, Age = @age, Working = @working WHERE Id = @id";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@name", SqlDbType.Text);
                command.Parameters.Add("@surname", SqlDbType.Text);
                command.Parameters.Add("@age", SqlDbType.Int);
                command.Parameters.Add("@working", SqlDbType.Bit);
                command.Parameters.Add("@id", SqlDbType.Int);

                command.Parameters["@name"].Value = person.Name;
                command.Parameters["@surname"].Value = person.Surname;
                command.Parameters["@age"].Value = person.Age;      
                command.Parameters["@working"].Value = person.Working;
                command.Parameters["@id"].Value = person.Id;
                command.ExecuteNonQuery();
            }
        }

        public static void AddPerson(Person person)
        {
            string sqlExpression = "INSERT INTO Person(Id, Name, Surname, Age, Working) VALUES (@id, @name, @surname, @age, @working)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters.Add("@name", SqlDbType.Text);
                command.Parameters.Add("@surname", SqlDbType.Text);
                command.Parameters.Add("@age", SqlDbType.Int);
                command.Parameters.Add("@working", SqlDbType.Bit);

                command.Parameters["@id"].Value = person.Id;
                command.Parameters["@name"].Value = person.Name;
                command.Parameters["@surname"].Value = person.Surname;
                command.Parameters["@age"].Value = person.Age;
                command.Parameters["@working"].Value = person.Working;
                command.ExecuteNonQuery();
            }
        }

        public static void DeletePerson(int id)
        {
            string sqlExpression = "UPDATE Person SET Deleted = 1 WHERE Id = @id";
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
            string sqlExpression = "SELECT COALESCE(MAX(Id), 0) FROM Person";
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
