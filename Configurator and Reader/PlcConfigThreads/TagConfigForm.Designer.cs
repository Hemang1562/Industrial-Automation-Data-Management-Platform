namespace PlcConfigThreads
{
    partial class TagConfigForm
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
            selectedPlcLbl = new Label();
            comboBox1 = new ComboBox();
            applyBtn = new Button();
            intervalListBox = new CheckedListBox();
            label1 = new Label();
            SaveBtn = new Button();
            CloseBtn = new Button();
            TypeComboBox = new ComboBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // selectedPlcLbl
            // 
            selectedPlcLbl.AutoSize = true;
            selectedPlcLbl.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            selectedPlcLbl.Location = new Point(29, 26);
            selectedPlcLbl.Name = "selectedPlcLbl";
            selectedPlcLbl.Size = new Size(88, 20);
            selectedPlcLbl.TabIndex = 0;
            selectedPlcLbl.Text = "Select Plc :";
            // 
            // comboBox1
            // 
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(123, 23);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 1;
            // 
            // applyBtn
            // 
            applyBtn.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            applyBtn.Location = new Point(310, 70);
            applyBtn.Name = "applyBtn";
            applyBtn.Size = new Size(94, 29);
            applyBtn.TabIndex = 2;
            applyBtn.Text = "Apply";
            applyBtn.UseVisualStyleBackColor = true;
            applyBtn.Click += applyBtn_Click;
            // 
            // intervalListBox
            // 
            intervalListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            intervalListBox.FormattingEnabled = true;
            intervalListBox.Location = new Point(169, 136);
            intervalListBox.Name = "intervalListBox";
            intervalListBox.Size = new Size(221, 268);
            intervalListBox.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(250, 113);
            label1.Name = "label1";
            label1.Size = new Size(89, 20);
            label1.TabIndex = 4;
            label1.Text = "Select Tags";
            // 
            // SaveBtn
            // 
            SaveBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            SaveBtn.Location = new Point(205, 410);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new Size(168, 29);
            SaveBtn.TabIndex = 7;
            SaveBtn.Text = "Save";
            SaveBtn.UseVisualStyleBackColor = true;
            SaveBtn.Click += SaveBtn_Click;
            // 
            // CloseBtn
            // 
            CloseBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseBtn.BackColor = Color.DarkCyan;
            CloseBtn.FlatAppearance.BorderSize = 0;
            CloseBtn.FlatStyle = FlatStyle.Flat;
            CloseBtn.Location = new Point(750, 12);
            CloseBtn.Name = "CloseBtn";
            CloseBtn.Size = new Size(35, 29);
            CloseBtn.TabIndex = 8;
            CloseBtn.Text = "X";
            CloseBtn.UseVisualStyleBackColor = false;
            // 
            // TypeComboBox
            // 
            TypeComboBox.FormattingEnabled = true;
            TypeComboBox.Items.AddRange(new object[] { "On Interval", "Threshold Value", "On/Off Bit", "Value Change" });
            TypeComboBox.Location = new Point(132, 70);
            TypeComboBox.Name = "TypeComboBox";
            TypeComboBox.Size = new Size(151, 28);
            TypeComboBox.TabIndex = 9;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(29, 76);
            label3.Name = "label3";
            label3.Size = new Size(99, 20);
            label3.TabIndex = 10;
            label3.Text = "Select Type :";
            // 
            // TagConfigForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(797, 450);
            Controls.Add(label3);
            Controls.Add(TypeComboBox);
            Controls.Add(CloseBtn);
            Controls.Add(SaveBtn);
            Controls.Add(label1);
            Controls.Add(intervalListBox);
            Controls.Add(applyBtn);
            Controls.Add(comboBox1);
            Controls.Add(selectedPlcLbl);
            Name = "TagConfigForm";
            Text = "TagConfigForm";
            Load += TagConfigForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label selectedPlcLbl;
        private ComboBox comboBox1;
        private Button applyBtn;
        private CheckedListBox intervalListBox;
        private Label label1;
        private Button SaveBtn;
        private Button CloseBtn;
        private ComboBox TypeComboBox;
        private Label label3;
        //private Syncfusion.Windows.Forms.Tools.MultiSelectionComboBox multiSelectionComboBox1;
    }
}