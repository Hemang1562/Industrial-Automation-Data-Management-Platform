using Microsoft.Data.SqlClient;
using System;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using EasyModbus;
using System.Net;
using System.Data;
namespace PlcConfigThreads
{
    public partial class Form1 : Form
    {
        Thread th;
        private List<PlcModel> PlcList = new List<PlcModel>();
        string filePath = @"C:\Users\rakes\Downloads\Here\NewFile.esconfig";
        string encryptionKey = "noway";
        string connectionString = "Data Source=HEMANG;Initial Catalog=PlcThreadTable;Integrated Security=True;Trust Server Certificate=True";
        private static ModbusClient modbusClient;
        private DataTable liveDataTable;
        TagConfigForm configForm;
        public Form1()
        {
            InitializeComponent();
            LoadExistingData();
        }
        private void LoadExistingData()
        {
            if (File.Exists(filePath))
            {
                PlcList = LoadClassCollectionFromFileEncrypted(filePath, encryptionKey);
            }
        }
        private List<PlcModel> LoadClassCollectionFromFileEncrypted(string filePath, string encryptionKey)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] iv = new byte[16];
                fs.Read(iv, 0, iv.Length);

                using (Aes aes = Aes.Create())
                {
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        aes.Key = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(encryptionKey));
                    }
                    aes.IV = iv;
                    using (CryptoStream cs = new CryptoStream(fs, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        object deserializedObject = formatter.Deserialize(cs);

                        //if (deserializedObject is List<PlcModel> deserializedList)
                        if (deserializedObject.GetType() == PlcList.GetType())
                        {
                            return ((List<PlcModel>)deserializedObject);
                        }
                        else if (deserializedObject is PlcModel singleDataClass)
                        {
                            // If the file contains a single DataClass object, wrap it in a list
                            return new List<PlcModel> { singleDataClass };
                        }
                        else
                        {
                            throw new InvalidCastException("The deserialized object is neither a List<DataClass> nor a DataClass object.");
                        }
                    }
                }
            }
        }
        private void PLCBtn_Click(object sender, EventArgs e)
        {
            Openform();
        }
        PlcConfigForm plcConfigForm;
        private void Openform()
        {

            if (plcConfigForm == null || plcConfigForm.IsDisposed)
            {
                plcConfigForm = new PlcConfigForm();
                plcConfigForm.MdiParent = this;
                plcConfigForm.Dock = DockStyle.Fill;
                plcConfigForm.FormBorderStyle = FormBorderStyle.None;
                plcConfigForm.Show();
            }
            else
            {
                if (plcConfigForm.WindowState == FormWindowState.Minimized)
                {
                    plcConfigForm.WindowState = FormWindowState.Normal;
                    plcConfigForm.BringToFront();
                }
                else if (plcConfigForm.WindowState == FormWindowState.Normal)
                {
                    plcConfigForm.BringToFront();
                }
            }
        }

        private void Openform(object? obj)
        {
            Application.Run(new PlcConfigForm());
        }

        private void tagConfigBtn_Click(object sender, EventArgs e)
        {
            openTagForm();
        }
        
        private void openTagForm()
        {
            if (configForm == null || configForm.IsDisposed)
            {
                configForm = new TagConfigForm();
                configForm.MdiParent = this;
                configForm.Dock = DockStyle.Fill;
                configForm.FormBorderStyle = FormBorderStyle.None;
                configForm.Show();
            }
            else
            {
                if (configForm.WindowState == FormWindowState.Minimized)
                {
                    configForm.WindowState = FormWindowState.Normal;
                    configForm.BringToFront();
                }
                else if (configForm.WindowState == FormWindowState.Normal)
                {
                    configForm.BringToFront();
                }
            }
        }
        private void ReadBtn_Click(object sender, EventArgs e)
        {
            LoadExistingData();
            foreach (var plc in PlcList)
            {
                try
                {
                    CreateLiveTable(plc.plc_Name, plc.LiveDataList);
                    MessageBox.Show($"Live data table for {plc.plc_Name} created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating live data table for {plc.plc_Name}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            EnsureThresholdHistoryTableExists();
            EnsureOnOffTableExists();
            EnsureValueChangeTableExists();
            foreach (var plc in PlcList)
            {
                string tagNamesString = string.Join(",", plc.LiveDataList);
                string IntervalTagNames = string.Join(",", plc.IntervalCheckedItems);      // Live Process Accortding to the number of Plc's
                LivePlcProcess(plc, tagNamesString, IntervalTagNames);

            }
            HistoryProcess();
        }
        private static void HistoryProcess()
        {
            try
            {
                // Create a new ProcessStartInfo
                ProcessStartInfo processInfo1 = new ProcessStartInfo
                {
                    FileName = @"C:\Users\rakes\source\repos\HistoryProcess\HistoryProcess\bin\Debug\net8.0\HistoryProcess.exe", // Path to the external process
                    CreateNoWindow = false,
                    UseShellExecute = false
                };

                // Start the process
                Process process1 = Process.Start(processInfo1);
                if (process1 != null)
                {
                    MessageBox.Show($"Process for History data table started.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting process for : {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void LivePlcProcess(PlcModel plc, string tagNamesString, string IntervalTagNames)
        {
            try
            {
                // Create a new ProcessStartInfo
                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Users\rakes\source\repos\LiveTableProcess\LiveTableProcess\bin\Debug\net8.0\LiveTableProcess.exe", // Path to the external process
                    Arguments = $"{plc.plc_Name} {plc.plc_IP} {plc.plc_startAdress} {plc.noOfPoints} \"{tagNamesString}\" \"{IntervalTagNames}\"",
                    CreateNoWindow = false,
                    UseShellExecute = false
                };

                // Start the process
                Process process = Process.Start(processInfo);
                if (process != null)
                {
                    MessageBox.Show($"Process for live data table of {plc.plc_Name} started.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting process for {plc.plc_Name}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void InsertDataIntoDatabase(DataTable liveDataTable)
        {
            string connectionString = "Data Source=HEMANG;Initial Catalog=PlcThreadTable;Integrated Security=True;Trust Server Certificate=True"; // Update with your DB connection string
            string tableName = "plc1Live"; // The live data table name based on PLC name

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = tableName;

                    try
                    {
                        // Write data from DataTable to the live data table in the database
                        bulkCopy.WriteToServer(liveDataTable);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error inserting data into database: {ex.Message}");
                    }
                }
            }
        }

        private void SetupLiveDataTable()
        {
            liveDataTable = new DataTable();
            if (!liveDataTable.Columns.Contains("Timestamp"))
            {
                liveDataTable.Columns.Add("Timestamp", typeof(DateTime));
            }
            // Create columns based on tag names
            foreach (var plc in PlcList)
            {
                foreach (var tag in plc.LiveDataList)
                {
                    if (!liveDataTable.Columns.Contains(tag))
                    {
                        liveDataTable.Columns.Add(tag, typeof(int)); // Assuming values are integers
                    } // Assuming values are integers
                }
            }
        }

        private void EnsureThresholdHistoryTableExists()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if HistoryTable exists; if not, create it
                string checkTableQuery = @"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IntervalHistoryTable]') AND type in (N'U'))
                BEGIN
                CREATE TABLE IntervalHistoryTable (
                     Timestamp DATETIME,
                     PlcName NVARCHAR(50),
                     Tag NVARCHAR(50),
                     Value INT                 
                )
            END";

                using (SqlCommand command = new SqlCommand(checkTableQuery, connection))
                {
                    command.ExecuteNonQuery();  // This will create the table if it doesn't exist
                }
            }
        }

        private void EnsureHistoryTableExists()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if HistoryTable exists; if not, create it
                string checkTableQuery = @"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ThresholdHistoryTable]') AND type in (N'U'))
                BEGIN
                CREATE TABLE ThresholdHistoryTable (
                    Timestamp DATETIME,
                    PlcName NVARCHAR(50),
                    SelectedItem NVARCHAR(50),
                    Value INT
                )
            END";

                using (SqlCommand command = new SqlCommand(checkTableQuery, connection))
                {
                    command.ExecuteNonQuery();  // This will create the table if it doesn't exist
                }
            }
        }
        private void EnsureValueChangeTableExists()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if HistoryTable exists; if not, create it
                string checkTableQuery = @"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OnValueChangeTable]') AND type in (N'U'))
                BEGIN
                    CREATE TABLE OnValueChangeTable (
                        Timestamp DATETIME,
                        PlcName NVARCHAR(50),
                        SelectedItem NVARCHAR(50),
                        Value INT
                    )
                END";

                using (SqlCommand command = new SqlCommand(checkTableQuery, connection))
                {
                    command.ExecuteNonQuery();  // This will create the table if it doesn't exist
                }
            }
        }

        private void EnsureOnOffTableExists()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if HistoryTable exists; if not, create it
                string checkTableQuery = @"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OnOffBitChangeTable]') AND type in (N'U'))
                BEGIN
                CREATE TABLE OnOffBitChangeTable (
                    Timestamp DATETIME,
                    PlcName NVARCHAR(50),
                    SelectedItem NVARCHAR(50),
                    Value INT
                )
            END";

                using (SqlCommand command = new SqlCommand(checkTableQuery, connection))
                {
                    command.ExecuteNonQuery();  // This will create the table if it doesn't exist
                }
            }
        }
        private void CreateLiveTable(string plc_Name, List<string> liveDataList)
        {
            string createTableQuery = $"IF OBJECT_ID(N'[dbo].[{plc_Name}Live]', N'U') IS NULL BEGIN " +
                              $"CREATE TABLE {plc_Name}Live (" +
                              $"Timestamp DATETIME, " +    // Timestamp column
                              $"Tag NVARCHAR(50), " +      // Tag column (string for tag name)
                              $"Value INT) END";           // Value column (integer for tag value)

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(createTableQuery, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void startReadOperation(List<PlcModel> plcList)
        {

            try
            {
                foreach (var plc in plcList)
                {
                    string tagNamesString = string.Join(",", plc.LiveDataList);
                    try
                    {
                        // Create a new ProcessStartInfo
                        ProcessStartInfo processInfo = new ProcessStartInfo
                        {
                            FileName = @"C:\Users\rakes\source\repos\LiveTableProcess\LiveTableProcess\bin\Debug\net8.0\LiveTableProcess.exe", // Path to the external process
                            Arguments = $"{plc.plc_Name} {plc.plc_IP} {plc.plc_startAdress} {plc.noOfPoints} \"{tagNamesString}\"",
                            CreateNoWindow = true,
                            UseShellExecute = false
                        };

                        // Start the process
                        Process process = Process.Start(processInfo);

                        if (process != null)
                        {
                            MessageBox.Show($"Process for live data table of {plc.plc_Name} started.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error starting process for {plc.plc_Name}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }

                // Ensure the history and threshold tables exist before inserting data
                // EnsureHistoryTableExists(); -------------------> threshold use


                // Create a thread for filling interval history data
                Thread IntervalhistoryThread = new Thread(() =>
                {
                    try
                    {
                        FillIntervalHistoryTable(plcList);
                        MessageBox.Show("Interval history table filled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error filling interval history table: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
                IntervalhistoryThread.Start();

                try
                {
                    // Create a new ProcessStartInfo
                    ProcessStartInfo Th1 = new ProcessStartInfo
                    {
                        FileName = @"C:\Users\rakes\source\repos\ThresholdProcess\ThresholdProcess\bin\Debug\net8.0\ThresholdProcess.exe", // Path to the external process
                        CreateNoWindow = false,
                        UseShellExecute = false
                    };

                    // Start the process
                    Process process1 = Process.Start(Th1);

                    if (process1 != null)
                    {
                        MessageBox.Show($"Process for live data table of Threshold History Table started.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error starting process for Threshold History Table: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                Thread OnOffBitThread = new Thread(() =>
                {
                    try
                    {
                        FillOnOffBitTable(plcList);
                        MessageBox.Show("On/Off history table filled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error filling threshold history table: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
                OnOffBitThread.Start();
                Thread OnValueChange = new Thread(() =>
                {
                    try
                    {
                        FillOnValueChangeTable(plcList);
                        MessageBox.Show("OnValueChange history table filled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error filling threshold history table: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
                OnValueChange.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in execution: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FillOnValueChangeTable(List<PlcModel> plcList)
        {
            Random random = new Random();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var plc in plcList)
                {

                    foreach (var item in plc.ValueChange)
                    {

                        // Generate a random value for testing
                        int value = random.Next(0, 100);  // Random integer between 0 and 100

                        string insertQuery = "INSERT INTO OnValueChangeTable (Timestamp, PlcName, SelectedItem, Value) " +
                                             "VALUES (@timestamp, @plcName, @selectedItem, @value)";

                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            // Add parameters to avoid SQL injection
                            command.Parameters.AddWithValue("@timestamp", DateTime.Now);  // Use the current timestamp
                            command.Parameters.AddWithValue("@plcName", plc.plc_Name);
                            command.Parameters.AddWithValue("@selectedItem", item);
                            command.Parameters.AddWithValue("@value", value);  // Insert random value

                            command.ExecuteNonQuery();  // Execute the insert command
                        }
                    }
                }
            }
        }
        private void FillOnOffBitTable(List<PlcModel> plcList)
        {
            Random random = new Random();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var plc in plcList)
                {
                    foreach (var item in plc.OnOffBit)
                    {

                        // Generate a random value for testing
                        int value = random.Next(0, 100);  // Random integer between 0 and 100

                        string insertQuery = "INSERT INTO OnOffBitChangeTable (Timestamp, PlcName, SelectedItem, Value) " +
                                             "VALUES (@timestamp, @plcName, @selectedItem, @value)";

                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            // Add parameters to avoid SQL injection
                            command.Parameters.AddWithValue("@timestamp", DateTime.Now);  // Use the current timestamp
                            command.Parameters.AddWithValue("@plcName", plc.plc_Name);
                            command.Parameters.AddWithValue("@selectedItem", item);
                            command.Parameters.AddWithValue("@value", value);  // Insert random value

                            command.ExecuteNonQuery();  // Execute the insert command
                        }
                    }
                }
            }
        }
        private void FillThresholdHistoryTable(List<PlcModel> plcList)
        {
            Random random = new Random();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var plc in plcList)
                {
                    foreach (var item in plc.ThresHoldCheckedItems)
                    {

                        // Generate a random value for testing
                        int value = random.Next(0, 100);  // Random integer between 0 and 100

                        string insertQuery = "INSERT INTO ThresholdHistoryTable (Timestamp, PlcName, SelectedItem, Value) " +
                                             "VALUES (@timestamp, @plcName, @selectedItem, @value)";

                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            // Add parameters to avoid SQL injection
                            command.Parameters.AddWithValue("@timestamp", DateTime.Now);  // Use the current timestamp
                            command.Parameters.AddWithValue("@plcName", plc.plc_Name);
                            command.Parameters.AddWithValue("@selectedItem", item);
                            command.Parameters.AddWithValue("@value", value);  // Insert random value

                            command.ExecuteNonQuery();  // Execute the insert command
                        }
                    }
                }
            }
        }

        private void FillIntervalHistoryTable(List<PlcModel> plcList)
        {
            Random random = new Random();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var plc in plcList)
                {
                    foreach (var item in plc.IntervalCheckedItems)
                    {
                        // Generate a random value for testing
                        int value = random.Next(0, 100);   // Random integer between 0 and 100

                        string insertQuery = "INSERT INTO IntervalHistoryTable (Timestamp, PlcName, SelectedItem, Value) " +
                                             "VALUES (@timestamp, @plcName, @selectedItem, @value)";

                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            // Add parameters to avoid SQL injection
                            command.Parameters.AddWithValue("@timestamp", DateTime.Now);  // Use the current timestamp
                            command.Parameters.AddWithValue("@plcName", plc.plc_Name);
                            command.Parameters.AddWithValue("@selectedItem", item);
                            command.Parameters.AddWithValue("@value", value);  // Insert random value
                            command.ExecuteNonQuery();  // Execute the insert command
                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            deleteoperation(PlcList);
        }

        private void deleteoperation(List<PlcModel> plcList)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete all the created tables?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Delete the live data tables for each PLC
                        foreach (var plc in plcList)
                        {
                            DropTable(connection, $"{plc.plc_Name}Live");
                        }

                        // Delete the history table
                        DropTable(connection, "IntervalHistoryTable");
                        DropTable(connection, "OnOffBitChangeTable");
                        DropTable(connection, "OnValueChangeTable");

                        // Delete any other custom tables if needed
                        DropTable(connection, "ThresholdHistoryTable"); // Example for threshold history table

                        MessageBox.Show("All tables have been deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting tables: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void DropTable(SqlConnection connection, string tableName)
        {
            try
            {
                string query = $"IF OBJECT_ID(N'[dbo].[{tableName}]', N'U') IS NOT NULL DROP TABLE [dbo].[{tableName}]";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();  // Executes the DROP TABLE command
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting table {tableName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void storeBtn_Click(object sender, EventArgs e)
        {
          
        }

        private void HomeBtn_Click(object sender, EventArgs e)
        {

        }
    }
}

//// Create a thread for live data table creation
//Thread liveThread = new Thread(() =>
//{
//    try
//    {
//        CreateLiveTable(plc.plc_Name, plc.LiveDataList);
//        MessageBox.Show($"Live data table for {plc.plc_Name} created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show($"Error creating live data table for {plc.plc_Name}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//    }
//});
//liveThread.Start();


//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

//foreach (var plc in plcList)
//{
//    Thread liveThread = new Thread(() =>
//    {
//        CreateLiveTable(plc.plc_Name, plc.LiveDataList);
//    });
//    liveThread.Start();
//}
//EnsureHistoryTableExists();
//EnsureThresholdHistoryTableExists();
//Thread IntervalhistoryThread = new Thread(() =>
//{
//    FillIntervalHistoryTable(plcList);
//});
//IntervalhistoryThread.Start(); 

//Thread ThresholdhistoryThread = new Thread(() =>
//{
//    FillThresholdHistoryTable(plcList);
//});
//ThresholdhistoryThread.Start();

//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------..

// Create a thread for filling threshold history data
//Thread ThresholdhistoryThread = new Thread(() =>
//{
//    try
//    {
//        FillThresholdHistoryTable(plcList);
//        MessageBox.Show("Threshold history table filled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show($"Error filling threshold history table: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//    }
//});
//ThresholdhistoryThread.Start();