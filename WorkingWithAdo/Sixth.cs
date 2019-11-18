using System;
using System.Data.SqlClient;

namespace SixthSolution
{
    class Sixth
    {
        static void Main(string[] args)
        {
            int villianId = int.Parse(Console.ReadLine());

            // sql commands
            var connection = new SqlConnection(@"Server=myAddress;Database=MinionsDB;Integrated Security=True;");
            var selectText = @"SELECT Name FROM Villains WHERE Id = @villainId";

            var delFromMinVil = @"DELETE FROM MinionsVillains
                                   WHERE VillainId = @villainId";

            var delFromVillains = @"DELETE FROM Villains
                                WHERE Id = @villainId";

            connection.Open();

            using (connection)
            {
                var comSelect = new SqlCommand(selectText, connection);
                comSelect.Parameters.AddWithValue("@villainId", villianId);
                var result = comSelect.ExecuteScalar();
                if (result == null)
                {
                    Console.WriteLine("No such villain was found.");
                }
                else
                {
                    var comDeleteFromMinVil = new SqlCommand(delFromMinVil, connection);
                    comDeleteFromMinVil.Parameters.AddWithValue("@villainId", villianId);
                    int countMinions = (int)comDeleteFromMinVil.ExecuteNonQuery();
                    var comDeleteFromVil = new SqlCommand(delFromVillains, connection);
                    Console.WriteLine($"{result} was deleted.");
                    Console.WriteLine($"{countMinions} minions were released.");
                }
            }
        }
    }
}
