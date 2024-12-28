namespace PlcConfigThreads
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            storeBtn = new Button();
            DeleteBtn = new Button();
            ReadBtn = new Button();
            tagConfigBtn = new Button();
            PLCBtn = new Button();
            UsersBtn = new Button();
            HomeBtn = new Button();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(32, 30, 67);
            panel1.Controls.Add(storeBtn);
            panel1.Controls.Add(DeleteBtn);
            panel1.Controls.Add(ReadBtn);
            panel1.Controls.Add(tagConfigBtn);
            panel1.Controls.Add(PLCBtn);
            panel1.Controls.Add(UsersBtn);
            panel1.Controls.Add(HomeBtn);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(182, 450);
            panel1.TabIndex = 0;
            // 
            // storeBtn
            // 
            storeBtn.Dock = DockStyle.Top;
            storeBtn.Enabled = false;
            storeBtn.FlatAppearance.BorderSize = 0;
            storeBtn.FlatStyle = FlatStyle.Flat;
            storeBtn.Font = new Font("Century Gothic", 10.2F);
            storeBtn.ForeColor = Color.FromArgb(238, 238, 238);
            storeBtn.Location = new Point(0, 390);
            storeBtn.Name = "storeBtn";
            storeBtn.Size = new Size(182, 47);
            storeBtn.TabIndex = 7;
            storeBtn.Text = "Store";
            storeBtn.UseVisualStyleBackColor = true;
            storeBtn.Click += storeBtn_Click;
            // 
            // DeleteBtn
            // 
            DeleteBtn.Dock = DockStyle.Top;
            DeleteBtn.FlatAppearance.BorderSize = 0;
            DeleteBtn.FlatStyle = FlatStyle.Flat;
            DeleteBtn.Font = new Font("Century Gothic", 10.2F);
            DeleteBtn.ForeColor = Color.FromArgb(238, 238, 238);
            DeleteBtn.Location = new Point(0, 343);
            DeleteBtn.Name = "DeleteBtn";
            DeleteBtn.Size = new Size(182, 47);
            DeleteBtn.TabIndex = 6;
            DeleteBtn.Text = "Delete Tables";
            DeleteBtn.UseVisualStyleBackColor = true;
            DeleteBtn.Click += DeleteBtn_Click;
            // 
            // ReadBtn
            // 
            ReadBtn.Dock = DockStyle.Top;
            ReadBtn.FlatAppearance.BorderSize = 0;
            ReadBtn.FlatStyle = FlatStyle.Flat;
            ReadBtn.Font = new Font("Century Gothic", 10.2F);
            ReadBtn.ForeColor = Color.FromArgb(238, 238, 238);
            ReadBtn.Location = new Point(0, 296);
            ReadBtn.Name = "ReadBtn";
            ReadBtn.Size = new Size(182, 47);
            ReadBtn.TabIndex = 5;
            ReadBtn.Text = "Read Config File";
            ReadBtn.UseVisualStyleBackColor = true;
            ReadBtn.Click += ReadBtn_Click;
            // 
            // tagConfigBtn
            // 
            tagConfigBtn.Dock = DockStyle.Top;
            tagConfigBtn.FlatAppearance.BorderSize = 0;
            tagConfigBtn.FlatStyle = FlatStyle.Flat;
            tagConfigBtn.Font = new Font("Century Gothic", 10.2F);
            tagConfigBtn.ForeColor = Color.FromArgb(238, 238, 238);
            tagConfigBtn.Location = new Point(0, 249);
            tagConfigBtn.Name = "tagConfigBtn";
            tagConfigBtn.Size = new Size(182, 47);
            tagConfigBtn.TabIndex = 4;
            tagConfigBtn.Text = "Tag Config";
            tagConfigBtn.UseVisualStyleBackColor = true;
            tagConfigBtn.Click += tagConfigBtn_Click;
            // 
            // PLCBtn
            // 
            PLCBtn.Dock = DockStyle.Top;
            PLCBtn.FlatAppearance.BorderSize = 0;
            PLCBtn.FlatStyle = FlatStyle.Flat;
            PLCBtn.Font = new Font("Century Gothic", 10.2F);
            PLCBtn.ForeColor = Color.FromArgb(238, 238, 238);
            PLCBtn.Location = new Point(0, 202);
            PLCBtn.Name = "PLCBtn";
            PLCBtn.Size = new Size(182, 47);
            PLCBtn.TabIndex = 3;
            PLCBtn.Text = "PLC";
            PLCBtn.UseVisualStyleBackColor = true;
            PLCBtn.Click += PLCBtn_Click;
            // 
            // UsersBtn
            // 
            UsersBtn.Dock = DockStyle.Top;
            UsersBtn.FlatAppearance.BorderSize = 0;
            UsersBtn.FlatStyle = FlatStyle.Flat;
            UsersBtn.Font = new Font("Century Gothic", 10.2F);
            UsersBtn.ForeColor = Color.FromArgb(238, 238, 238);
            UsersBtn.Location = new Point(0, 155);
            UsersBtn.Name = "UsersBtn";
            UsersBtn.Size = new Size(182, 47);
            UsersBtn.TabIndex = 2;
            UsersBtn.Text = "Users";
            UsersBtn.UseVisualStyleBackColor = true;
            // 
            // HomeBtn
            // 
            HomeBtn.Dock = DockStyle.Top;
            HomeBtn.FlatAppearance.BorderSize = 0;
            HomeBtn.FlatStyle = FlatStyle.Flat;
            HomeBtn.Font = new Font("Century Gothic", 10.2F);
            HomeBtn.ForeColor = Color.FromArgb(238, 238, 238);
            HomeBtn.Location = new Point(0, 108);
            HomeBtn.Name = "HomeBtn";
            HomeBtn.Size = new Size(182, 47);
            HomeBtn.TabIndex = 1;
            HomeBtn.Text = "Home";
            HomeBtn.UseVisualStyleBackColor = true;
            HomeBtn.Click += HomeBtn_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(pictureBox1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(182, 108);
            panel2.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(182, 172);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            IsMdiContainer = true;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button HomeBtn;
        private Panel panel2;
        private PictureBox pictureBox1;
        private Button PLCBtn;
        private Button UsersBtn;
        private Button tagConfigBtn;
        private Button ReadBtn;
        private Button DeleteBtn;
        private Button storeBtn;
    }
}
