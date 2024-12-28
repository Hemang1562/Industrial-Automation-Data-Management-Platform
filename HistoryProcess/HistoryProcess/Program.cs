using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.IO.Pipes;
using System.Text;

namespace HistoryProcess
{
    public class Program
    {
        private static NamedPipeClientStream pipeClient;
        private static DataTable virtualDataTable = new DataTable();
        private static List<string> IntervalTagsList;
        private static DataTable dataTableCopy;
        private static Queue<DataTable> dataTableQueue = new Queue<DataTable>();
        private static string connectionString = "Data Source=HEMANG;Initial Catalog=PlcThreadTable;Integrated Security=True;Trust Server Certificate=True";

        static void Main(string[] args)
        {
            SetupVirtualDataTable();
            StartDatabaseUpdateThread();
            NamedPipeClient();
        }
        static void SetupVirtualDataTable()
        {
            virtualDataTable.Columns.Add("Timestamp", typeof(DateTime)); // Timestamp column
            virtualDataTable.Columns.Add("PlcName", typeof(string));     // PLC name column
            virtualDataTable.Columns.Add("Tag", typeof(string));         // Tag column
            virtualDataTable.Columns.Add("Value", typeof(int));
        }
        private static void EnqueueDataTable(DataTable virtualDataTable)
        {
            lock (dataTableQueue)
            {
                dataTableQueue.Enqueue(virtualDataTable);
            }
        }
        private static void StartDatabaseUpdateThread()
        {
            Thread dbThread = new Thread(() =>
            {
                while (true) // Continuously check the queue
                {
                    //DataTable dataTableToProcess = null;

                    lock (dataTableQueue)
                    {
                        if (dataTableQueue.Count > 0)
                        {
                            InsertDataIntoDatabase(dataTableQueue.Dequeue());
                        }
                    }
                    Thread.Sleep(100); // Sleep for a second before checking again
                }
            });

            dbThread.IsBackground = true; // Ensure the thread runs in the background
            dbThread.Start();
        }
        static void NamedPipeClient()
        {
            try
            {
                using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "LiveProcessPipe", PipeDirection.InOut))
                {
                    pipeClient.Connect(); // Connect to the named pipe server once
                    Console.WriteLine("Connected to named pipe server.");

                    //using (NamedPipeServerStream pipeServer = new NamedPipeServerStream("LiveProcessPipe", PipeDirection.InOut))
                    //{
                    //    Console.WriteLine("Waiting for client connection...");
                    //    pipeServer.WaitForConnection(); // Wait for the client to connect once
                    //    Console.WriteLine("Client connected.");

                    using (StreamReader reader = new StreamReader(pipeClient))
                    using (StreamWriter writer = new StreamWriter(pipeClient) { AutoFlush = true })
                    {
                        while (true)
                        {
                            writer.WriteLine("RequestIntervalTags"); // Send request for interval tags data

                            ParseAndStoreResponse(reader);
                            Thread.Sleep(5000); // Wait for 5 seconds before sending the next request
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Named pipe client error: {ex.Message}");
                Console.ReadKey();
            }
        }
        static void ParseAndStoreResponse(StreamReader reader)
        {
            string response;
            DataRow currentRow = null;

            while ((response = reader.ReadLine()) != null)
            {
                if (response == "END")
                {
                    break; // End of response, break the loop
                }

                // Example response format: "Tag: Tag1, Value: 123, Timestamp: 2024-10-04 12:00:00"
                string[] parts = response.Split(new[] { ", " }, StringSplitOptions.None);
                string plcName = "Plc1";
                // Extract the values
                string tag = parts[0].Split('*')[1].Trim();
                int value = int.Parse(parts[1].Split('*')[1].Trim());
                DateTime timestamp = DateTime.Parse(parts[2].Split('*')[1].Trim());

                // If first row, create a new row and assign timestamp
                currentRow = virtualDataTable.NewRow();
                currentRow["Timestamp"] = timestamp;
                currentRow["PlcName"] = plcName;
                currentRow["Tag"] = tag;
                currentRow["Value"] = value;

                // Add the completed row to the DataTable
                virtualDataTable.Rows.Add(currentRow);
            }
            dataTableCopy = virtualDataTable.Copy();
            EnqueueDataTable(dataTableCopy);
            virtualDataTable.Clear();
            Console.WriteLine("Done!!");
        }
        private static void InsertDataIntoDatabase(DataTable virtualDataTable)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = $"IntervalHistoryTable";

                    try
                    {
                        // Write data from DataTable to the live data table in the database
                        bulkCopy.WriteToServer(virtualDataTable);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error inserting data into database: {ex.Message}");
                    }
                }
            }
        }
    }
}

// Read and display the response from the server
//string response;
//while ((response = reader.ReadLine()) != null)
//{
//    if (response == "END")
//    {
//        Console.WriteLine("Received all data.");
//        break; // Exit the loop after receiving all data
//    }
//    Console.WriteLine(response);
//}



//string tableName = $"IntervalHistoryTable";
//using (SqlConnection connection = new SqlConnection(connectionString))
//{
//    connection.Open();
//    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
//    {
//        bulkCopy.DestinationTableName = $"IntervalHistoryTable";

//        // Manually map columns if necessary (this is optional if the names match exactly)
//        foreach (DataColumn column in virtualDataTable.Columns)
//        {
//            bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
//        }
//        try
//        {
//            // Write data from DataTable to the table in the database
//            bulkCopy.WriteToServer(virtualDataTable);
//            Console.WriteLine("Data inserted successfully.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error inserting data into database: {ex.Message}");
//        }
//    }
//}