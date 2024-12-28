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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PlcConfigThreads
{
    public partial class TagConfigForm : Form
    {
        private List<PlcModel> PlcList = new List<PlcModel>();
        string filePath = @"C:\Users\rakes\Downloads\Here\NewFile.esconfig";
        string encryptionKey = "noway";
        public TagConfigForm()
        {
            InitializeComponent();
            LoadExistingData();
            LoadIntoComboBox();
            LoadIntoIntervalBox();
            CloseBtn.Click += delegate { this.Close(); };
        }

        private void LoadIntoIntervalBox()
        {

        }

        private void LoadIntoComboBox()
        {
            comboBox1.DataSource = PlcList;
            comboBox1.DisplayMember = "plc_Name";
            comboBox1.ValueMember = "id";
        }

        private void TagConfigForm_Load(object sender, EventArgs e)
        {

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
        private void applyBtn_Click(object sender, EventArgs e)
        {
            intervalListBox.Items.Clear();
            
                PlcModel selectedPLC = comboBox1.SelectedItem as PlcModel;
                if (selectedPLC != null)
                {
                    // Populate the CheckedListBox with numbers from StartNumber to EndNumber
                    for (int i = selectedPLC.plc_startAdress; i <= selectedPLC.noOfPoints + selectedPLC.plc_startAdress; i++)
                    {
                        intervalListBox.Items.Add("Tag_" + i);
                    }
                }        
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            PlcModel selectedPLC = comboBox1.SelectedItem as PlcModel;
            selectedPLC.LiveDataList.Clear();
            for (int i = selectedPLC.plc_startAdress; i <= selectedPLC.noOfPoints + selectedPLC.plc_startAdress; i++)
            {
                selectedPLC.LiveDataList.Add("Tag_" + i);
            }
            if (TypeComboBox.SelectedItem == "On Interval") 
            {
                if (selectedPLC != null)
                {
                    // Clear the previous checked items
                    selectedPLC.IntervalCheckedItems.Clear();

                    // Loop through checked items in the CheckedListBox
                    foreach (object item in intervalListBox.CheckedItems)
                    {
                        selectedPLC.IntervalCheckedItems.Add(item.ToString());
                    }
                    try
                    {
                        SaveClassToFileEncrypted(filePath, PlcList, encryptionKey);
                        MessageBox.Show("Configuration saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (TypeComboBox.SelectedItem == "Threshold Value")
            {
                if (selectedPLC != null)
                {
                    // Clear the previous checked items
                    selectedPLC.ThresHoldCheckedItems.Clear();
                    

                    // Loop through checked items in the CheckedListBox
                    foreach (object item in intervalListBox.CheckedItems)
                    {
                        selectedPLC.ThresHoldCheckedItems.Add(item.ToString());
                    }

                    try
                    {
                        SaveClassToFileEncrypted(filePath, PlcList, encryptionKey);
                        MessageBox.Show("Configuration saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (TypeComboBox.SelectedItem == "On/Off Bit")
            {
                if (selectedPLC != null)
                {
                    // Clear the previous checked items
                    selectedPLC.OnOffBit.Clear();


                    // Loop through checked items in the CheckedListBox
                    foreach (object item in intervalListBox.CheckedItems)
                    {
                        selectedPLC.OnOffBit.Add(item.ToString());
                    }

                    try
                    {
                        SaveClassToFileEncrypted(filePath, PlcList, encryptionKey);
                        MessageBox.Show("Configuration saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (TypeComboBox.SelectedItem == "Value Change")
            {
                if (selectedPLC != null)
                {
                    // Clear the previous checked items
                    selectedPLC.ValueChange.Clear();


                    // Loop through checked items in the CheckedListBox
                    foreach (object item in intervalListBox.CheckedItems)
                    {
                        selectedPLC.ValueChange.Add(item.ToString());
                    }

                    try
                    {
                        SaveClassToFileEncrypted(filePath, PlcList, encryptionKey);
                        MessageBox.Show("Configuration saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
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
    }
}
