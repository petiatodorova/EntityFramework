using System;
using System.Data.SqlClient;

namespace InitialSetup
{
    class StartUp
    {
        static void Main(string[] args)
        {
            // Creating the database
            var connection = new SqlConnection(@"Server=myServerAddress;Database=master;Integrated Security=True;");
            var commandText = @"CREATE DATABASE MinionsDB";
            string okMessage = @"The database is created successfully!";
            string errorMessage = @"The database was not created.";

            // Executing the command
            NonQueryCommandExecution(connection, commandText, okMessage, errorMessage);


            // Creating the tables
            connection = new SqlConnection(@"Server=myServerAddress;Database=MinionsDB;Integrated Security=True;");

            commandText = @"CREATE TABLE Countries(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50))

                            CREATE TABLE Towns(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), CountryCode INT FOREIGN KEY REFERENCES Countries(Id))
                            
                            
                            CREATE TABLE Minions(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(30), Age INT, TownId INT FOREIGN KEY REFERENCES Towns(Id))
                            
                            
                            CREATE TABLE EvilnessFactors(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50))
                            
                            
                            CREATE TABLE Villains(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id))
                            
                            
                            CREATE TABLE MinionsVillains(MinionId INT FOREIGN KEY REFERENCES Minions(Id), VillainId INT FOREIGN KEY REFERENCES Villains(Id), CONSTRAINT PK_MinionsVillains PRIMARY KEY(MinionId, VillainId))";
            okMessage = @"The tables were created successfully!";
            errorMessage = @"The tables were not created.";

            // Executing the command
            NonQueryCommandExecution(connection, commandText, okMessage, errorMessage);


            // Insert into tables
            connection = new SqlConnection(@"Server=myServerAddress;Database=MinionsDB;Integrated Security=True;");

            commandText = @"INSERT INTO Countries ([Name]) VALUES ('Bulgaria'),('England'),('Cyprus'),('Germany'),('Norway')

                            INSERT INTO Towns ([Name], CountryCode) VALUES ('Plovdiv', 1),('Varna', 1),('Burgas', 1),('Sofia', 1),('London', 2),('Southampton', 2),('Bath', 2),('Liverpool', 2),('Berlin', 3),('Frankfurt', 3),('Oslo', 4)
                            
                            INSERT INTO Minions (Name,Age, TownId) VALUES('Bob', 42, 3),('Kevin', 1, 1),('Bob ', 32, 6),('Simon', 45, 3),('Cathleen', 11, 2),('Carry ', 50, 10),('Becky', 125, 5),('Mars', 21, 1),('Misho', 5, 10),('Zoe', 125, 5),('Json', 21, 1)
                            
                            INSERT INTO EvilnessFactors (Name) VALUES ('Super good'),('Good'),('Bad'), ('Evil'),('Super evil')
                            
                            INSERT INTO Villains (Name, EvilnessFactorId) VALUES ('Gru',2),('Victor',1),('Jilly',3),('Miro',4),('Rosen',5),('Dimityr',1),('Dobromir',2)
                            
                            INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (4,2),(1,1),(5,7),(3,5),(2,6),(11,5),(8,4),(9,7),(7,1),(1,3),(7,3),(5,3),(4,3),(1,2),(2,1),(2,7)";

            okMessage = @"The data were inserted successfully!";
            errorMessage = @"There was an error inserting the data.";

            // Executing the command
            NonQueryCommandExecution(connection, commandText, okMessage, errorMessage);
        }

        private static void NonQueryCommandExecution(SqlConnection connection, 
                                                     string commandText, 
                                                     string okMessage, 
                                                     string errorMessage)
        {
            connection.Open();

            using (connection)
            {
                var command = new SqlCommand($"{commandText}", connection);
                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"{okMessage}");

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
