using System;
using System.Threading;
using MySqlConnector;

namespace MariaDB
{
    internal class Program
    {
        private MySqlConnection Database = new MySqlConnection("Server=162.19.139.137; Port=3306; Database=s39872_happyplant; Uid=u39872_4ZMr4ARAKk; Pwd=lXZ6Yh2Vard1vkG5l5RIQB30;");

        static void Main(string[] args)
        {
            Program p = new Program();
            p.ReadDataPeriodically();
        }

        private void ReadDataPeriodically()
        {
            try
            {
                int Sleep = 1000;
                Database.Open();
                Console.WriteLine("Connected to the database.");
                Thread.Sleep(Sleep);

                while (true)
                {
                    int Getdatadelay = 10000;
                    ReadDataFromDatabase();
                    Console.WriteLine(new string('-', 50));
                    Thread.Sleep(Getdatadelay);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error connecting to the database: " + ex.Message);
            }
            finally
            {
                Database.Close();
                Console.WriteLine("Database connection closed.");
            }
        }

        private void ReadDataFromDatabase()
        {
            try
            {
                string query = "SELECT * FROM sensordata";

                using (MySqlCommand command = new MySqlCommand(query, Database))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                object valueFromDatabase = reader[columnName];

                                if (valueFromDatabase != DBNull.Value && valueFromDatabase != null)
                                {
                                    Console.WriteLine($"{columnName}: {valueFromDatabase}");
                                }
                                else
                                {
                                    Console.WriteLine($"{columnName}: De waarde uit de database is NULL.");
                                }
                            }

                            Console.WriteLine();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error retrieving data from the database: " + ex.Message);
            }
        }
    }
}