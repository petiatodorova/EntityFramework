using System;
using System.Data.SqlClient;

namespace ThirdSolution
{
    class ThirdExercise
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection(@"Server=myServerAddress;Database=MinionsDB;Integrated Security=True;");
            var commandTextOne = @"SELECT Name FROM Villains WHERE Id = @Id";
            var commandTextTwo = @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                         m.Name, 
                                         m.Age
                                    FROM MinionsVillains AS mv
                                    JOIN Minions As m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = @Id
                                ORDER BY m.Name";

            int villianId = int.Parse(Console.ReadLine());

            connection.Open();

            using (connection)
            {
                var command = new SqlCommand(commandTextOne, connection);
                command.Parameters.AddWithValue("@Id", villianId);
                var result = (string)command.ExecuteScalar();
                if (String.IsNullOrEmpty(result))
                {
                    Console.WriteLine($"No villain with ID {villianId} exists in the database.");
                    return;
                }
                else
                {
                    Console.WriteLine($"Villain: {result}");
                }

                command = new SqlCommand(commandTextTwo, connection);
                command.Parameters.AddWithValue("@Id", villianId);
                var reader = command.ExecuteReader();

                using (reader)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]}. {reader[1]} {reader[2]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("(no minions)");
                    }
                }
            }
        }
    }
}
