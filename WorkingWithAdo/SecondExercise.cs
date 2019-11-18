using System;
using System.Data.SqlClient;

namespace ProblemTwo
{
    class SecondExercise
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection(@"Server=myServerAddress;Database=MinionsDB;Integrated Security=True;");

            var commandText = @"SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
                                  FROM Villains AS v 
                                  JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
                              GROUP BY v.Id, v.Name 
                                HAVING COUNT(mv.VillainId) > 3 
                              ORDER BY COUNT(mv.VillainId)";

            var okMessage = @"The command was executed successfully!";
            var errorMessage = @"The command was not executed successfully.";
            var nullMessage = @"There are no such villains.";

            // Executing the command
            ReaderCommandExecution(connection,
                                   commandText,
                                   okMessage,
                                   errorMessage,
                                   nullMessage);
        }

        private static void ReaderCommandExecution(SqlConnection connection,
                                                     string commandText,
                                                     string okMessage,
                                                     string errorMessage,
                                                     string nullMessage)
        {
            connection.Open();

            using (connection)
            {
                try
                {
                    var command = new SqlCommand($"{commandText}", connection);
                    var reader = command.ExecuteReader();
                    Console.WriteLine($"{okMessage}");

                    using (reader)
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader[0]} - {reader[1]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{nullMessage}");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{errorMessage}");
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
