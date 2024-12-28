using Microsoft.Data.SqlClient;
using Syncfusion.WinForms.Core.Utils;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataPager.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PlcConfigThreads
{
    public partial class PlcConfigForm : Form
    {
        private List<PlcModel> PlcList = new List<PlcModel>();
        string filePath = @"C:\Users\rakes\Downloads\Here\NewFile.esconfig";
        string encryptionKey = "noway";
        string connectionString = "Data Source=HEMANG;Initial Catalog=PlcThreadTable;Integrated Security=True;Trust Server Certificate=True";

        public PlcConfigForm()
        {
            InitializeComponent();
            LoadExistingData();
            tabControl1.TabPages.Remove(tabPage2);
            CloseBtn.Click += delegate { this.Close(); };
        }

        private void PlcConfigForm_Load(object sender, EventArgs e)
        {

        }
        private void LoadExistingData()
        {
            if (File.Exists(filePath))
            {
                PlcList = LoadClassCollectionFromFileEncrypted(filePath, encryptionKey);
            }
            sfDataGrid1.DataSource = PlcList;
            //sfDataPager1.PageSize = 5;
            //sfDataPager1.AllowOnDemandPaging = true;
            //sfDataPager1.OnDemandLoading += OnDemandLoading;

        }
        BusyIndicator busyIndicator = new BusyIndicator();

        //private void OnDemandLoading(object sender, OnDemandLoadingEventArgs e)
        //{
        //    //Show busy indicator while loading the data.
        //    if (sfDataGrid1.TableControl.IsHandleCreated)
        //    {
        //        busyIndicator.Show(this.sfDataGrid1.TableControl);
        //        Thread.Sleep(100);
        //    }

        //    sfDataPager1.LoadDynamicData(e.StartRowIndex, PlcList.Skip(e.StartRowIndex).Take(e.PageSize));
        //    busyIndicator.Hide();
        //    sfDataGrid1.DataSource = sfDataPager1.PagedSource;
        //}
        public static void SaveClassToFileEncrypted(string filePath, List<PlcModel> dataList, string encryptionKey)
        {
            // Serialize the class to a byte array
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, dataList);
                byte[] dataToEncrypt = ms.ToArray();

                // Encrypt the data
                using (Aes aes = Aes.Create())
                {
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        aes.Key = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(encryptionKey));
                    }
                    aes.GenerateIV();

                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(aes.IV, 0, aes.IV.Length);

                        using (CryptoStream cs = new CryptoStream(fs, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                        }
                    }
                }
            }
        }
        public static void ClearEncryptedFile(string filePath, string encryptionKey)
        {
            // Create an empty list to overwrite the file
            List<PlcModel> emptyList = new List<PlcModel>();
            SaveClassToFileEncrypted(filePath, emptyList, encryptionKey); // Overwrite file with empty data
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

                        if (deserializedObject is List<PlcModel> deserializedList)
                        {
                            return deserializedList;
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

        private void AddBtn_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Add(tabPage2);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {

                PlcModel pm = new PlcModel
                {
                    id = PlcList.Count > 0 ? PlcList[PlcList.Count - 1].id + 1 : 1,
                    plc_Name = plcNameTxtBox.Text,
                    plc_Type = PlcTypeComboBox.Text,
                    plc_IP = ipTxtBox.Text,
                    plc_port = Convert.ToInt32(plcPortTxtBox.Text),
                    plc_slaveAdress = Convert.ToInt32(slaveaddTxtBox.Text),
                    plc_startAdress = Convert.ToInt32(StartAddTxtBox.Text),
                    noOfPoints = Convert.ToInt32(NoOfPointsTxtBox.Text)
                };
                PlcList.Add(pm);
                SaveClassToFileEncrypted(filePath, PlcList, encryptionKey);
                MessageBox.Show("Data saved successfully!");
                LoadExistingData();
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Add(tabPage1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message);
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            sfDataGrid1.SearchController.AllowFiltering = true;
            sfDataGrid1.SearchController.Search(searchTxtBox.Text);
            //Set the color for highlighting the search text
            sfDataGrid1.SearchController.SearchColor = Color.LightGreen;
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to clear all the data?", "Confirmation",
                                 MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            ////// Check the user's response
            if (result == DialogResult.Yes)
            {
                // Call the method to clear the file
                ClearEncryptedFile(filePath, encryptionKey);
                MessageBox.Show("File data has been cleared.");
            }
            else
            {
                // If No is clicked, do nothing
                MessageBox.Show("Operation canceled.");
            }
            deleteoperation(PlcList);
            LoadExistingData();
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

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (sfDataGrid1.SelectedItem != null)
            {
                // Get the selected PLC model
                PlcModel selectedPlc = sfDataGrid1.SelectedItem as PlcModel;

                // Show a confirmation message before deletion
                DialogResult dialogResult = MessageBox.Show($"Are you sure you want to delete {selectedPlc.plc_Name}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    // Step 1: Remove the selected item from the plcList
                    PlcList.Remove(selectedPlc);

                    // Step 2: Refresh the SfDataGrid to reflect changes
                    sfDataGrid1.DataSource = null;
                    sfDataGrid1.DataSource = PlcList;

                    MessageBox.Show($"{selectedPlc.plc_Name} has been deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SaveClassToFileEncrypted(filePath, PlcList, encryptionKey);

                }
                DialogResult dialogResult1 = MessageBox.Show("Are you sure you want to delete the created tables?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult1 == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            // Delete the live data tables for each PLC
                            
                                DropTable(connection, $"{selectedPlc.plc_Name}Live");                                                      

                            MessageBox.Show("Plc table have been deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting tables: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // If no row is selected
                MessageBox.Show("Please select a PLC to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Add(tabPage2);
            if (sfDataGrid1.SelectedItem != null)
            {
                // Get the selected PLC model
                PlcModel selectedPlc = sfDataGrid1.SelectedItem as PlcModel;
                plcNameTxtBox.Text = selectedPlc.plc_Name;
                plcPortTxtBox.Text = Convert.ToString(selectedPlc.plc_port);
                PlcTypeComboBox.Text = selectedPlc.plc_Type;
                slaveaddTxtBox.Text = Convert.ToString(selectedPlc.plc_slaveAdress);
                StartAddTxtBox.Text = Convert.ToString(selectedPlc.plc_startAdress);
                NoOfPointsTxtBox.Text = Convert.ToString(selectedPlc.noOfPoints);              
            }
            else
            {
                // If no row is selected
                MessageBox.Show("Please select a PLC to Edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
