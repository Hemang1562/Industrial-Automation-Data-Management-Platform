namespace PlcConfigThreads
{
    partial class PlcConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            CloseBtn = new Button();
            PlcLabel = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            clearBtn = new Button();
            DeleteBtn = new Button();
            EditBtn = new Button();
            AddBtn = new Button();
            SearchBtn = new Button();
            searchTxtBox = new TextBox();
            sfDataGrid1 = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            tabPage2 = new TabPage();
            PlcTypeComboBox = new ComboBox();
            cancelBtn = new Button();
            SaveBtn = new Button();
            NoOfPointsTxtBox = new TextBox();
            slaveaddTxtBox = new TextBox();
            StartAddTxtBox = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            ipTxtBox = new TextBox();
            plcPortTxtBox = new TextBox();
            plcPortLbl = new Label();
            plcTypeLbl = new Label();
            label1 = new Label();
            plcNameTxtBox = new TextBox();
            plcNameLbl = new Label();
            sfDataPager1 = new Syncfusion.WinForms.DataPager.SfDataPager();
            panel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)sfDataGrid1).BeginInit();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(CloseBtn);
            panel1.Controls.Add(PlcLabel);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 72);
            panel1.TabIndex = 0;
            // 
            // CloseBtn
            // 
            CloseBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseBtn.BackColor = Color.DarkCyan;
            CloseBtn.FlatAppearance.BorderSize = 0;
            CloseBtn.FlatStyle = FlatStyle.Flat;
            CloseBtn.Location = new Point(753, 24);
            CloseBtn.Name = "CloseBtn";
            CloseBtn.Size = new Size(35, 29);
            CloseBtn.TabIndex = 1;
            CloseBtn.Text = "X";
            CloseBtn.UseVisualStyleBackColor = false;
            // 
            // PlcLabel
            // 
            PlcLabel.AutoSize = true;
            PlcLabel.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            PlcLabel.Location = new Point(45, 29);
            PlcLabel.Name = "PlcLabel";
            PlcLabel.Size = new Size(35, 18);
            PlcLabel.TabIndex = 0;
            PlcLabel.Text = "PLC";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 72);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 378);
            tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(sfDataPager1);
            tabPage1.Controls.Add(clearBtn);
            tabPage1.Controls.Add(DeleteBtn);
            tabPage1.Controls.Add(EditBtn);
            tabPage1.Controls.Add(AddBtn);
            tabPage1.Controls.Add(SearchBtn);
            tabPage1.Controls.Add(searchTxtBox);
            tabPage1.Controls.Add(sfDataGrid1);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(792, 345);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "PlcList";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // clearBtn
            // 
            clearBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            clearBtn.Font = new Font("Century Gothic", 9F);
            clearBtn.Location = new Point(697, 177);
            clearBtn.Name = "clearBtn";
            clearBtn.Size = new Size(89, 29);
            clearBtn.TabIndex = 6;
            clearBtn.Text = "Clear";
            clearBtn.UseVisualStyleBackColor = true;
            clearBtn.Click += clearBtn_Click;
            // 
            // DeleteBtn
            // 
            DeleteBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            DeleteBtn.Font = new Font("Century Gothic", 9F);
            DeleteBtn.Location = new Point(697, 142);
            DeleteBtn.Name = "DeleteBtn";
            DeleteBtn.Size = new Size(89, 29);
            DeleteBtn.TabIndex = 5;
            DeleteBtn.Text = "Delete";
            DeleteBtn.UseVisualStyleBackColor = true;
            DeleteBtn.Click += DeleteBtn_Click;
            // 
            // EditBtn
            // 
            EditBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            EditBtn.Font = new Font("Century Gothic", 9F);
            EditBtn.Location = new Point(697, 107);
            EditBtn.Name = "EditBtn";
            EditBtn.Size = new Size(87, 29);
            EditBtn.TabIndex = 4;
            EditBtn.Text = "Edit";
            EditBtn.UseVisualStyleBackColor = true;
            EditBtn.Click += EditBtn_Click;
            // 
            // AddBtn
            // 
            AddBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AddBtn.Font = new Font("Century Gothic", 9F);
            AddBtn.Location = new Point(697, 72);
            AddBtn.Name = "AddBtn";
            AddBtn.Size = new Size(87, 29);
            AddBtn.TabIndex = 3;
            AddBtn.Text = "Add";
            AddBtn.UseVisualStyleBackColor = true;
            AddBtn.Click += AddBtn_Click;
            // 
            // SearchBtn
            // 
            SearchBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SearchBtn.Font = new Font("Century Gothic", 9F);
            SearchBtn.Location = new Point(662, 10);
            SearchBtn.Name = "SearchBtn";
            SearchBtn.Size = new Size(94, 29);
            SearchBtn.TabIndex = 2;
            SearchBtn.Text = "Search";
            SearchBtn.UseVisualStyleBackColor = true;
            SearchBtn.Click += SearchBtn_Click;
            // 
            // searchTxtBox
            // 
            searchTxtBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            searchTxtBox.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            searchTxtBox.Location = new Point(21, 12);
            searchTxtBox.Name = "searchTxtBox";
            searchTxtBox.PlaceholderText = "Search Here";
            searchTxtBox.Size = new Size(635, 26);
            searchTxtBox.TabIndex = 1;
            // 
            // sfDataGrid1
            // 
            sfDataGrid1.AccessibleName = "Table";
            sfDataGrid1.AllowFiltering = true;
            sfDataGrid1.AllowTriStateSorting = true;
            sfDataGrid1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            sfDataGrid1.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill;
            sfDataGrid1.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            sfDataGrid1.Location = new Point(21, 45);
            sfDataGrid1.Name = "sfDataGrid1";
            sfDataGrid1.PreviewRowHeight = 35;
            sfDataGrid1.ShowGroupDropArea = true;
            sfDataGrid1.Size = new Size(670, 254);
            sfDataGrid1.Style.BorderColor = Color.FromArgb(100, 100, 100);
            sfDataGrid1.Style.CheckBoxStyle.CheckedBackColor = Color.FromArgb(0, 120, 215);
            sfDataGrid1.Style.CheckBoxStyle.CheckedBorderColor = Color.FromArgb(0, 120, 215);
            sfDataGrid1.Style.CheckBoxStyle.IndeterminateBorderColor = Color.FromArgb(0, 120, 215);
            sfDataGrid1.Style.HyperlinkStyle.DefaultLinkColor = Color.FromArgb(0, 120, 215);
            sfDataGrid1.TabIndex = 0;
            sfDataGrid1.Text = "sfDataGrid1";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(PlcTypeComboBox);
            tabPage2.Controls.Add(cancelBtn);
            tabPage2.Controls.Add(SaveBtn);
            tabPage2.Controls.Add(NoOfPointsTxtBox);
            tabPage2.Controls.Add(slaveaddTxtBox);
            tabPage2.Controls.Add(StartAddTxtBox);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(ipTxtBox);
            tabPage2.Controls.Add(plcPortTxtBox);
            tabPage2.Controls.Add(plcPortLbl);
            tabPage2.Controls.Add(plcTypeLbl);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(plcNameTxtBox);
            tabPage2.Controls.Add(plcNameLbl);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(792, 345);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "PlcEdit";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // PlcTypeComboBox
            // 
            PlcTypeComboBox.BackColor = Color.FromArgb(224, 224, 224);
            PlcTypeComboBox.FormattingEnabled = true;
            PlcTypeComboBox.Items.AddRange(new object[] { "TCP", "RTU" });
            PlcTypeComboBox.Location = new Point(40, 115);
            PlcTypeComboBox.Name = "PlcTypeComboBox";
            PlcTypeComboBox.Size = new Size(255, 28);
            PlcTypeComboBox.TabIndex = 15;
            // 
            // cancelBtn
            // 
            cancelBtn.Font = new Font("Century Gothic", 10.8F);
            cancelBtn.Location = new Point(235, 248);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(156, 42);
            cancelBtn.TabIndex = 14;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = true;
            // 
            // SaveBtn
            // 
            SaveBtn.Font = new Font("Century Gothic", 10.8F);
            SaveBtn.Location = new Point(40, 248);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new Size(156, 42);
            SaveBtn.TabIndex = 13;
            SaveBtn.Text = "Save";
            SaveBtn.UseVisualStyleBackColor = true;
            SaveBtn.Click += SaveBtn_Click;
            // 
            // NoOfPointsTxtBox
            // 
            NoOfPointsTxtBox.BackColor = Color.FromArgb(224, 224, 224);
            NoOfPointsTxtBox.BorderStyle = BorderStyle.None;
            NoOfPointsTxtBox.Font = new Font("Century Gothic", 10.2F);
            NoOfPointsTxtBox.Location = new Point(456, 194);
            NoOfPointsTxtBox.Name = "NoOfPointsTxtBox";
            NoOfPointsTxtBox.Size = new Size(136, 21);
            NoOfPointsTxtBox.TabIndex = 12;
            // 
            // slaveaddTxtBox
            // 
            slaveaddTxtBox.BackColor = Color.FromArgb(224, 224, 224);
            slaveaddTxtBox.BorderStyle = BorderStyle.None;
            slaveaddTxtBox.Font = new Font("Century Gothic", 10.2F);
            slaveaddTxtBox.Location = new Point(40, 194);
            slaveaddTxtBox.Name = "slaveaddTxtBox";
            slaveaddTxtBox.Size = new Size(169, 21);
            slaveaddTxtBox.TabIndex = 11;
            // 
            // StartAddTxtBox
            // 
            StartAddTxtBox.BackColor = Color.FromArgb(224, 224, 224);
            StartAddTxtBox.BorderStyle = BorderStyle.None;
            StartAddTxtBox.Font = new Font("Century Gothic", 10.2F);
            StartAddTxtBox.Location = new Point(245, 194);
            StartAddTxtBox.Name = "StartAddTxtBox";
            StartAddTxtBox.Size = new Size(163, 21);
            StartAddTxtBox.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 10.2F);
            label4.Location = new Point(40, 170);
            label4.Name = "label4";
            label4.Size = new Size(169, 21);
            label4.TabIndex = 9;
            label4.Text = "PLC SlaveAddress :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 10.2F);
            label3.Location = new Point(456, 170);
            label3.Name = "label3";
            label3.Size = new Size(122, 21);
            label3.TabIndex = 8;
            label3.Text = "No Of Points :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 10.2F);
            label2.Location = new Point(245, 170);
            label2.Name = "label2";
            label2.Size = new Size(163, 21);
            label2.TabIndex = 7;
            label2.Text = "PLC StartAddress :";
            // 
            // ipTxtBox
            // 
            ipTxtBox.BackColor = Color.FromArgb(224, 224, 224);
            ipTxtBox.BorderStyle = BorderStyle.None;
            ipTxtBox.Font = new Font("Century Gothic", 10.2F);
            ipTxtBox.Location = new Point(387, 115);
            ipTxtBox.Name = "ipTxtBox";
            ipTxtBox.Size = new Size(259, 21);
            ipTxtBox.TabIndex = 6;
            // 
            // plcPortTxtBox
            // 
            plcPortTxtBox.BackColor = Color.FromArgb(224, 224, 224);
            plcPortTxtBox.BorderStyle = BorderStyle.None;
            plcPortTxtBox.Font = new Font("Century Gothic", 10.2F);
            plcPortTxtBox.Location = new Point(387, 49);
            plcPortTxtBox.Name = "plcPortTxtBox";
            plcPortTxtBox.Size = new Size(259, 21);
            plcPortTxtBox.TabIndex = 5;
            // 
            // plcPortLbl
            // 
            plcPortLbl.AutoSize = true;
            plcPortLbl.Font = new Font("Century Gothic", 10.2F);
            plcPortLbl.Location = new Point(387, 25);
            plcPortLbl.Name = "plcPortLbl";
            plcPortLbl.Size = new Size(90, 21);
            plcPortLbl.TabIndex = 4;
            plcPortLbl.Text = "PLC Port :";
            // 
            // plcTypeLbl
            // 
            plcTypeLbl.AutoSize = true;
            plcTypeLbl.Font = new Font("Century Gothic", 10.2F);
            plcTypeLbl.Location = new Point(40, 91);
            plcTypeLbl.Name = "plcTypeLbl";
            plcTypeLbl.Size = new Size(96, 21);
            plcTypeLbl.TabIndex = 3;
            plcTypeLbl.Text = "PLC Type :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 10.2F);
            label1.Location = new Point(387, 91);
            label1.Name = "label1";
            label1.Size = new Size(182, 21);
            label1.TabIndex = 2;
            label1.Text = "IP Address/Slave ID :";
            // 
            // plcNameTxtBox
            // 
            plcNameTxtBox.BackColor = Color.FromArgb(224, 224, 224);
            plcNameTxtBox.BorderStyle = BorderStyle.None;
            plcNameTxtBox.Font = new Font("Century Gothic", 10.2F);
            plcNameTxtBox.Location = new Point(40, 49);
            plcNameTxtBox.Name = "plcNameTxtBox";
            plcNameTxtBox.Size = new Size(259, 21);
            plcNameTxtBox.TabIndex = 1;
            // 
            // plcNameLbl
            // 
            plcNameLbl.AutoSize = true;
            plcNameLbl.Font = new Font("Century Gothic", 10.2F);
            plcNameLbl.Location = new Point(41, 25);
            plcNameLbl.Name = "plcNameLbl";
            plcNameLbl.Size = new Size(108, 21);
            plcNameLbl.TabIndex = 0;
            plcNameLbl.Text = "PLC Name :";
            // 
            // sfDataPager1
            // 
            sfDataPager1.AccessibleName = "DataPager";
            sfDataPager1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            sfDataPager1.CanOverrideStyle = true;
            sfDataPager1.Location = new Point(21, 308);
            sfDataPager1.Name = "sfDataPager1";
            sfDataPager1.Size = new Size(670, 29);
            sfDataPager1.TabIndex = 7;
            sfDataPager1.Text = "sfDataPager1";
            // 
            // PlcConfigForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Controls.Add(panel1);
            Name = "PlcConfigForm";
            Text = "PlcConfigForm";
            Load += PlcConfigForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)sfDataGrid1).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label PlcLabel;
        private Button CloseBtn;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button SearchBtn;
        private TextBox searchTxtBox;
        private Syncfusion.WinForms.DataGrid.SfDataGrid sfDataGrid1;
        private Button DeleteBtn;
        private Button EditBtn;
        private Button AddBtn;
        private Label plcNameLbl;
        private TextBox plcNameTxtBox;
        private TextBox plcPortTxtBox;
        private Label plcPortLbl;
        private Label plcTypeLbl;
        private Label label1;
        private TextBox ipTxtBox;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox NoOfPointsTxtBox;
        private TextBox slaveaddTxtBox;
        private TextBox StartAddTxtBox;
        private Button SaveBtn;
        private Button cancelBtn;
        private ComboBox PlcTypeComboBox;
        private Button clearBtn;
        private Syncfusion.WinForms.DataPager.SfDataPager sfDataPager1;
    }
}