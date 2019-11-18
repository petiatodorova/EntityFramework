using System;
using System.Data.SqlClient;
using System.Linq;

namespace FourthExercise
{
    class FourthSolution
    {
        static void Main(string[] args)
        {
            string[] line = Console.ReadLine()
           .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
           .ToArray();

            string[] lineSecond = Console.ReadLine()
           .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
           .ToArray();

            // param values
            string minionName = line[1];
            int minionAge = int.Parse(line[2]);
            string minionTown = line[3];
            string villainName = lineSecond[1];
            int townId = 0;
            int minionId = 0;
            int villainId = 0;

            // sql commands
            var connection = new SqlConnection(@"Server=myServerAddress;Database=MinionsDB;Integrated Security=True;");
            var textSelectTowns = @"SELECT Id FROM Towns WHERE Name = @townName";
            var textSelectVillains = @"SELECT Id FROM Villains WHERE Name = @Name";
            var textSelectMinions = @"SELECT Id FROM Minions WHERE Name = @Name";
            var textInsertTowns = @"INSERT INTO Towns ([Name]) VALUES (@townName)";
            var textInsertVillains = @"INSERT INTO Villains (Name, EvilnessFactorId) VALUES (@villainName, 4)";
            var textInsertMinion = @"INSERT INTO Minions (Name, Age, TownId) VALUES (@nam, @age, @townId)";
            var textInsertMinionVillain = @"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@minionId, @villainId)";

            connection.Open();

            using (connection)
            {
                // town
                var commSelectTown = new SqlCommand(textSelectTowns, connection);
                commSelectTown.Parameters.AddWithValue("@townName", minionTown);
                var result = commSelectTown.ExecuteScalar();
                if (result == null)
                {
                    var commInsertTown = new SqlCommand(textInsertTowns, connection);
                    commInsertTown.Parameters.AddWithValue("@townName", minionTown);
                    commInsertTown.ExecuteNonQuery();
                    Console.WriteLine($"Town {minionTown} was added to the database.");

                    // take the townId
                    result = commSelectTown.ExecuteScalar();
                    townId = (int)result;
                }
                else
                {
                    townId = (int)result;
                }


                // villain
                var commSelectVillain = new SqlCommand(textSelectVillains, connection);
                commSelectVillain.Parameters.AddWithValue("@Name", villainName);
                result = commSelectVillain.ExecuteScalar();
                if (result == null)
                {
                    var commInsertVillain = new SqlCommand(textInsertVillains, connection);
                    commInsertVillain.Parameters.AddWithValue("@villainName", villainName);
                    commInsertVillain.ExecuteNonQuery();
                    Console.WriteLine($"Villain {villainName} was added to the database.");

                    // take the villainId
                    result = commSelectVillain.ExecuteScalar();
                    villainId = (int)result;
                }
                else
                {
                    villainId = (int)result;
                }



                // minion
                var commSelectMinion = new SqlCommand(textSelectMinions, connection);
                commSelectMinion.Parameters.AddWithValue("@Name", minionName);
                result = commSelectMinion.ExecuteScalar();
                if (result == null)
                {
                    // insert into Minions
                    var commInsertMinIntoTable = new SqlCommand(textInsertMinion, connection);
                    commInsertMinIntoTable.Parameters.AddWithValue("@nam", minionName);
                    commInsertMinIntoTable.Parameters.AddWithValue("@age", minionAge);
                    commInsertMinIntoTable.Parameters.AddWithValue("@townId", townId);
                    commInsertMinIntoTable.ExecuteNonQuery();

                    // take the minionId
                    result = commSelectMinion.ExecuteScalar();
                    minionId = (int)result;

                    // insert into MinionsVillains
                    var commInsertIntoMinVill = new SqlCommand(textInsertMinionVillain, connection);
                    commInsertIntoMinVill.Parameters.AddWithValue("@minionId", minionId);
                    commInsertIntoMinVill.Parameters.AddWithValue("@villainId", villainId);
                    commInsertIntoMinVill.ExecuteNonQuery();
                    Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
                    return;
                }
                else
                {
                    Console.WriteLine($"The minion {minionName} is already minion of {villainName}.");
                }
            }
        }
    }
}
