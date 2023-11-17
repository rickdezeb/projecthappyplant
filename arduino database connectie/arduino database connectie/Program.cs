using System;
using System.IO.Ports;
using MySqlConnector;
using System.Threading;

class Program
{
    static SerialPort serialPort;

    private MySqlConnection Database = new MySqlConnection("Server=162.19.139.137; Port=3306; Database=s39872_happyplant; Uid=u39872_4ZMr4ARAKk; Pwd=lXZ6Yh2Vard1vkG5l5RIQB30;");

    static void Main(string[] args)
    {
        serialPort = new SerialPort("COM4", 9600);
        serialPort.Open();
        Program p = new Program();

        int Delay = 5000;

        while (true)
        {
            Console.Clear();

            string response = ReceiveFromArduino();
            Console.WriteLine(response);

            p.UpdateDatabase(response);

            Thread.Sleep(Delay);
        }
    }

    static string ReceiveFromArduino()
    {
        string receivedData = "";
        while (serialPort.BytesToRead > 0)
        {
            receivedData += serialPort.ReadLine();
        }
        return receivedData;
    }

    private void UpdateDatabase(string dataFromArduino)
    {
        try
        {

            if (Database.State == System.Data.ConnectionState.Closed)
            {
                Console.WriteLine("Database connection is closed. Connecting...");
                Database.Open();
            }

            string[] dataParts = dataFromArduino.Split(',');
            foreach (var part in dataParts)
            {
                string[] keyValue = part.Split(':');
                if (keyValue.Length == 2)
                {
                    string sensorType = keyValue[0].ToLower();
                    string dataValue = keyValue[1];

                   
                    string updateQuery = $"UPDATE sensordata SET data = '{dataValue}' WHERE sensor = '{sensorType}'";
                    using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, Database))
                    {
                        updateCommand.ExecuteNonQuery();
                    }
                }
            }

            Console.WriteLine("Database updated successfully.");
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Error updating the database: " + ex.Message);
        }
    }

}