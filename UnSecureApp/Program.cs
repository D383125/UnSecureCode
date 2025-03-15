using Microsoft.Data.SqlClient;

namespace SecureApp
{
    internal sealed class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                return;

            InjectionThreat(args[0], args[1]);
        }

        private static void InjectionThreat(string userInput, string password)
        {
            string connectionString = "Server=myServer;Database=myDB;User Id=myUser;Password=myPassword;";

            string query = "SELECT * FROM Users WHERE Username = '" + userInput + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection)) // Directly here
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"User: {reader["Username"]}");
                        }
                    }
                }
            }
        }
    }
}