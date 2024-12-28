using Microsoft.Data.SqlClient;
using System;
using System.Data;
using EasyModbus;
using System.IO.MemoryMappedFiles;
using Azure;
using System.IO.Pipes;
using System.Text;
namespace LiveProcesss
{
    public class Program
    {
        private static string connectionString = "Data Source=HEMANG;Initial Catalog=PlcThreadTable;Integrated Security=True;Trust Server Certificate=True";
        private static DataTable virtualDataTable = new DataTable();
        private static string plcName;
        private static string ipAddress;
        private static int startAddress;
        private static int numberOfRegisters;
        private static int interval;
        private static List<string> tagNames;
        private static List<string> IntervalTagsList;
        private static ModbusClient modbusClient;
        private static NamedPipeServerStream pipeServer;
        private static Queue<DataTable> dataTableQueue = new Queue<DataTable>();
        private static DataTable dataTableCopy;
        static void Main(string[] args)
        {
            try
            {
                // Check if we have the correct number of arguments
                if (args.Length < 6)
                {
                    Console.WriteLine("Invalid arguments");
                    return;
                }
                plcName = args[0];
                ipAddress = args[1];
                startAddress = int.Parse(args[2]);
                numberOfRegisters = int.Parse(args[3]);
                tagNames = new List<string>(args[4].Split(','));
                IntervalTagsList = new List<string>(args[5].Split(','));
                interval = 1000;
                foreach (var tag in tagNames)
                {
                    Console.WriteLine($"- {tag}");
                }
                foreach (var tag in IntervalTagsList)
                {
                    Console.WriteLine($"Interval Tag- {tag}");
                }
                modbusClient = new ModbusClient(ipAddress, 502);
                modbusClient.Connect();

                SetupLiveDataTable();
                StartDatabaseUpdateThread();
                StartNamedPipeServer();

                Timer timer = new Timer(ReadData, null, 0, interval);
                Console.WriteLine("Reading live data... Press any key to exit.");
                Console.ReadKey();
                modbusClient.Disconnect();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
        private static void StartNamedPipeServer()
        {
            Thread serverThread = new Thread(() =>
            {
                using (NamedPipeServerStream pipeServer = new NamedPipeServerStream("LiveProcessPipe", PipeDirection.InOut))
                {
                    Console.WriteLine("Waiting for client connection...");
                    pipeServer.WaitForConnection(); // Wait for the client to connect once
                    Console.WriteLine("Client connected.");
                    //using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "LiveProcessPipe", PipeDirection.InOut))
                    //{
                    //    pipeClient.Connect(); // Connect to the named pipe server once
                    //    Console.WriteLine("Connected to named pipe server.");
                    using (StreamReader reader = new StreamReader(pipeServer))
                    using (StreamWriter writer = new StreamWriter(pipeServer) { AutoFlush = true }) // Enable AutoFlush
                    {
                        while (true)
                        {
                            string request = reader.ReadLine(); // Read client request
                            if (request == "RequestIntervalTags")
                            {
                                Console.WriteLine("Received request for interval tags.");
                                lock (dataTableCopy) // Ensure thread-safe access to the DataTable
                                {
                                    foreach (string tag in IntervalTagsList)
                                    {
                                        DataRow[] rows = dataTableCopy.Select($"Tag = '{tag}'");
                                        foreach (var row in rows)
                                        {
                                            string response = $"Tag* {row["Tag"]}, Value* {row["Value"]}, Timestamp* {row["Timestamp"]}";
                                            writer.WriteLine(response); // Send data back to client
                                        }
                                    }
                                    writer.WriteLine("END"); // Indicate end of data
                                }
                            }
                        }
                    }
                }
            });

            serverThread.IsBackground = true;
            serverThread.Start();
        }
        //private static void NamedPipeServer()
        //{
        //    try
        //    {
        //        using (pipeServer = new NamedPipeServerStream("LiveProcessPipe", PipeDirection.InOut, 1))
        //        {
        //            Console.WriteLine("Named pipe server waiting for client connection...");
        //            pipeServer.WaitForConnection();
        //            Console.WriteLine("Client connected to named pipe server.");

        //            using (StreamReader reader = new StreamReader(pipeServer))
        //            using (StreamWriter writer = new StreamWriter(pipeServer) { AutoFlush = true })
        //            {
        //                while (pipeServer.IsConnected)
        //                {
        //                    string clientRequest = reader.ReadLine();

        //                    if (clientRequest == "RequestIntervalTags")
        //                    {
        //                        // Send interval tag values from virtualDataTable
        //                        string response = GetIntervalTagsData();
        //                        writer.WriteLine(response); // Send the data to the client
        //                        writer.Flush();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Named pipe server error: {ex.Message}");
        //    }
        //}
        //private static string GetIntervalTagsData()
        //{
        //    StringBuilder dataResponse = new StringBuilder();

        //    lock (virtualDataTable)
        //    {
        //        foreach (string tag in IntervalTagsList)
        //        {
        //            DataRow[] rows = virtualDataTable.Select($"Tag = '{tag}'");
        //            foreach (DataRow row in rows)
        //            {
        //                dataResponse.AppendLine($"Tag: {row["Tag"]}, Value: {row["Value"]}, Timestamp: {row["Timestamp"]}");
        //            }
        //        }
        //    }

        //    return dataResponse.ToString();
        //}
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

                    //if (dataTableToProcess != null)
                    //{
                    //    // Process the datatable (Insert or Update)
                    //    InsertDataIntoDatabase(dataTableToProcess);
                    //}

                    Thread.Sleep(100); // Sleep for a second before checking again
                }
            });

            dbThread.IsBackground = true; // Ensure the thread runs in the background
            dbThread.Start();
        }
        private static void ReadData(object state)
        {
            try
            {
                //int x= startAddress;
                DateTime timestamp = DateTime.Now;
                if (!modbusClient.Connected)
                    modbusClient.Connect();
                int maxRegistersPerRead = 100; // Modbus limit is typically 125 registers per read
                int totalRegisters = numberOfRegisters + 1;
                int currentAddress = startAddress;
                List<int> allRegisterValues = new List<int>();

                while (totalRegisters > 0)
                {
                    int registersToRead = Math.Min(totalRegisters, maxRegistersPerRead);

                    int[] registerValues = modbusClient.ReadHoldingRegisters(currentAddress, registersToRead);

                    allRegisterValues.AddRange(registerValues);

                    totalRegisters -= registersToRead;
                    currentAddress += registersToRead;
                }

                lock (virtualDataTable)
                {
                    int registerIndex = 0;
                    foreach (string tag in tagNames)
                    {
                        if (registerIndex < allRegisterValues.Count)
                        {
                            DataRow row = virtualDataTable.NewRow();
                            row["Timestamp"] = timestamp;
                            row["Tag"] = tag;
                            row["Value"] = allRegisterValues[registerIndex];
                            virtualDataTable.Rows.Add(row);
                            registerIndex++;
                        }
                        else
                        {
                            Console.WriteLine($"Error: Not enough register values for tag {tag}");
                        }
                    }
                    dataTableCopy = virtualDataTable.Copy();
                    // Insert the dataTable into the Queue
                    EnqueueDataTable(dataTableCopy);
                    // InsertDataIntoDatabase(virtualDataTable);
                    // Clear the live data table for the next read cycle
                    virtualDataTable.Clear();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading data: {ex.Message}");
            }

        }


        private static void EnqueueDataTable(DataTable virtualDataTable)
        {
            lock (dataTableQueue)
            {
                dataTableQueue.Enqueue(virtualDataTable);
            }
        }

        private static void InsertDataIntoDatabase(DataTable virtualDataTable)
        {
            DataTable dataTableCopy;

            // Lock while copying the DataTable to avoid modification issues
            lock (virtualDataTable)
            {
                dataTableCopy = virtualDataTable.Copy();
            }
            string tableName = $"{plcName}Live"; // The live data table name based on PLC name
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Check if the row for this tag exists
                string selectQuery = $"SELECT COUNT(1) FROM {plcName}Live WHERE Tag = @Tag";
                foreach (DataRow row in dataTableCopy.Rows)
                {
                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@Tag", row["Tag"]);
                        int rowExists = (int)selectCommand.ExecuteScalar();

                        if (rowExists == 0)
                        {
                            // If the row does not exist, insert the initial values
                            string insertQuery = $"INSERT INTO {plcName}Live (Timestamp, Tag, Value) VALUES (@Timestamp, @Tag, @Value)";
                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@Timestamp", row["Timestamp"]);
                                insertCommand.Parameters.AddWithValue("@Tag", row["Tag"]);
                                insertCommand.Parameters.AddWithValue("@Value", row["Value"]);
                                insertCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // If the row exists, update the value and timestamp
                            string updateQuery = $"UPDATE {plcName}Live SET Timestamp = @Timestamp, Value = @Value WHERE Tag = @Tag";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@Timestamp", row["Timestamp"]);
                                updateCommand.Parameters.AddWithValue("@Value", row["Value"]);
                                updateCommand.Parameters.AddWithValue("@Tag", row["Tag"]);
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }

            }
        }

        private static void SetupLiveDataTable()
        {
            virtualDataTable = new DataTable();
            virtualDataTable.Columns.Add("Timestamp", typeof(DateTime)); // Timestamp column
            virtualDataTable.Columns.Add("Tag", typeof(string));         // Tag name column
            virtualDataTable.Columns.Add("Value", typeof(int));          // Value column (integer)

        }


    }
}


// Read Modbus registers based on start address and number of registers
//int[] registerValues = modbusClient.ReadHoldingRegisters(startAddress, numberOfRegisters);

//// Create a new row for the live data table
//DataRow row = virtualDataTable.NewRow();
//row["Timestamp"] = DateTime.Now;

//// Fill the row with the values read from Modbus registers
//for (int i = 0; i < registerValues.Length && i < tagNames.Count; i++)
//{
//    row[tagNames[i]] = registerValues[i];
//}

//// Add the new row to the live data table
//virtualDataTable.Rows.Add(row);



//using (SqlConnection connection = new SqlConnection(connectionString))
//{
//    connection.Open();
//    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
//    {
//        bulkCopy.DestinationTableName = tableName;

//        try
//        {
//            // Write data from DataTable to the live data table in the database
//            bulkCopy.WriteToServer(virtualDataTable);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error inserting data into database: {ex.Message}");
//        }
//    }
//}

//foreach (string tag in tagsToRead)
//{
//    // Get the start address of the tag from the tagAddressMap


//    // Read the value from the holding register
//    int[] registerValues = modbusClient.ReadHoldingRegisters(x, 1); // Read 1 register
//    int value = registerValues[0]; // Assuming a single value is read

//    // Add the data to the virtual DataTable
//    DataRow row = virtualDataTable.NewRow();
//    row["Timestamp"] = timestamp;
//    row["Tag"] = tag;
//    row["Value"] = value;

//    virtualDataTable.Rows.Add(row);
//    x++;
//}
