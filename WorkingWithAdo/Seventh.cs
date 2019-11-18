using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Seventh
{
    class Seventh
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection(@"Server=myAddress;Database=MinionsDB;Integrated Security=True;");
            var selectText = @"SELECT Name FROM Minions";

            connection.Open();

            using (connection)
            {
                var comSelect = new SqlCommand(selectText, connection);
                var reader = comSelect.ExecuteReader();

                using (reader)
                {
                    List<string> towns = new List<string>();
                    while (reader.Read())
                    {
                        towns.Add(reader["Name"].ToString());
                    }

                    int length = towns.Count;
                    int half = length / 2;

                    if (length % 2 != 0)
                    {
                        PrintEvenLength(towns, length, half);
                        Console.WriteLine(towns[half]);
                    }
                    else
                    {
                        PrintEvenLength(towns, length, half);
                    }
                }
            }
        }

        private static void PrintEvenLength(List<string> towns, int length, int half)
        {
            for (int i = 0; i < half; i++)
            {
                Console.WriteLine($"{towns[i]}");
                Console.WriteLine($"{towns[length - 1 - i]}");
            }
        }
    }
}
