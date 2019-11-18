using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Fifth
{
    class Five
    {
        static void Main(string[] args)
        {
            string country = Console.ReadLine();

            // sql commands
            var connection = new SqlConnection(@"Server=myAddress;Database=MinionsDB;Integrated Security=True;");
            var updateText = @"UPDATE Towns
                                  SET Name = UPPER(Name)
                                WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";

            var selectTowns = @" SELECT t.Name 
                                   FROM Towns as t
                                   JOIN Countries AS c ON c.Id = t.CountryCode
                                  WHERE c.Name = @countryName";

            connection.Open();

            using (connection)
            {
                var comUpdate = new SqlCommand(updateText, connection);
                comUpdate.Parameters.AddWithValue("@countryName", country);
                int countTowns = (int)comUpdate.ExecuteNonQuery();
                Console.WriteLine(countTowns);
                if (countTowns != 0)
                {
                    var comSelectTowns = new SqlCommand(selectTowns, connection);
                    comSelectTowns.Parameters.AddWithValue("@countryName", country);
                    var reader = comSelectTowns.ExecuteReader();

                    using (reader)
                    {
                        List<string> towns = new List<string>();
                        while (reader.Read())
                        {
                            towns.Add(reader["Name"].ToString());
                        }
                        Console.WriteLine($"{string.Join(", ", towns)}");
                    }
                }
                else
                {
                    Console.WriteLine("No town names were affected.");
                }



            }
        }
    }
}
