using System;
using System.Data;
using System.Data.SqlClient;

namespace NinthSolution
{
    class Ninth
    {
        static void Main(string[] args)
        {
            int minionId = int.Parse(Console.ReadLine());

            var connection = new SqlConnection(@"Server=myAddress;Database=MinionsDB;Integrated Security=True;");
            var textSelect = @"SELECT Name, Age FROM Minions WHERE Id = @Id";

            connection.Open();

            using (connection)
            {
                SqlCommand StoredProcedureCommand = new SqlCommand("usp_GetOlder", connection);
                StoredProcedureCommand.CommandType = CommandType.StoredProcedure;
                StoredProcedureCommand.Parameters.AddWithValue("@id", minionId);
                StoredProcedureCommand.ExecuteNonQuery();

                var comSelectNameAge = new SqlCommand(textSelect, connection);
                comSelectNameAge.Parameters.AddWithValue("@id", minionId);
                var reader = comSelectNameAge.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} – {reader["Age"]} years old");
                    }
                }
            }
        }
    }
}
