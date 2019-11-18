using System;
using System.Data.SqlClient;
using System.Linq;

namespace EightSolution
{
    class Eight
    {
        static void Main(string[] args)
        {
            int[] ids = Console.ReadLine()
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

            var connection = new SqlConnection(@"Server=myAddress;Database=MinionsDB;Integrated Security=True;");
            var updateAge = @"UPDATE Minions
                                 SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1
                               WHERE Id = @Id";

            var selectMinions = @"SELECT Name, Age FROM Minions";

            connection.Open();

            using (connection)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    int id = ids[i];
                    var comUpdate = new SqlCommand(updateAge, connection);
                    comUpdate.Parameters.AddWithValue("@Id", id);
                    comUpdate.ExecuteNonQuery();
                }

                var comSelect = new SqlCommand(selectMinions, connection);
                var reader = comSelect.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} {reader["Age"]}");
                    }
                }
            }
        }
    }
}
