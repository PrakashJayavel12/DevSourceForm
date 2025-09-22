namespace devSourceApp
{
    partial class SourceBuilderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SourceBuilderForm));
            panel2 = new Panel();
            label2 = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            button2 = new Button();
            label3 = new Label();
            label1 = new Label();
            DestinationFolder = new Label();
            comboBox2 = new ComboBox();
            button4 = new Button();
            textBox1 = new TextBox();
            button6 = new Button();
            button5 = new Button();
            tabPage2 = new TabPage();
            panel1 = new Panel();
            label4 = new Label();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            comboBox1 = new ComboBox();
            button1 = new Button();
            panelSidebar = new Panel();
            pictureBox1 = new PictureBox();
            btnDecrypt = new Button();
            btnBuildDllSidebar = new Button();
            panel2.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            panel1.SuspendLayout();
            panelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(131, 145, 141);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(tabControl1);
            panel2.Location = new Point(155, -1);
            panel2.Name = "panel2";
            panel2.Size = new Size(714, 434);
            panel2.TabIndex = 1;
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.FlatStyle = FlatStyle.Popup;
            label2.Location = new Point(207, 7);
            label2.Name = "label2";
            label2.Size = new Size(267, 19);
            label2.TabIndex = 11;
            label2.Text = "Developer Panel";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            tabControl1.Alignment = TabAlignment.Bottom;
            tabControl1.AllowDrop = true;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(-11, 29);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(734, 434);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(DestinationFolder);
            tabPage1.Controls.Add(comboBox2);
            tabPage1.Controls.Add(button4);
            tabPage1.Controls.Add(textBox1);
            tabPage1.Controls.Add(button6);
            tabPage1.Controls.Add(button5);
            tabPage1.Location = new Point(4, 4);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(726, 406);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(242, 179, 179);
            button2.Location = new Point(469, 278);
            button2.Margin = new Padding(0);
            button2.Name = "button2";
            button2.Size = new Size(172, 32);
            button2.TabIndex = 13;
            button2.Text = "Clear Details";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.BackColor = Color.FromArgb(166, 182, 191);
            label3.FlatStyle = FlatStyle.Popup;
            label3.Location = new Point(72, 285);
            label3.Name = "label3";
            label3.Size = new Size(338, 19);
            label3.TabIndex = 12;
            label3.Text = "Developer Panel";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.Click += label3_Click;
            // 
            // label1
            // 
            label1.BackColor = Color.White;
            label1.FlatStyle = FlatStyle.Popup;
            label1.Location = new Point(76, 87);
            label1.Name = "label1";
            label1.Size = new Size(196, 19);
            label1.TabIndex = 10;
            label1.Text = "Source Path";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // DestinationFolder
            // 
            DestinationFolder.BackColor = Color.White;
            DestinationFolder.FlatStyle = FlatStyle.Popup;
            DestinationFolder.Location = new Point(76, 159);
            DestinationFolder.Name = "DestinationFolder";
            DestinationFolder.Size = new Size(196, 19);
            DestinationFolder.TabIndex = 9;
            DestinationFolder.Text = "Destination Path";
            DestinationFolder.TextAlign = ContentAlignment.MiddleLeft;
            DestinationFolder.Click += DestinationFolder_Click;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(76, 181);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(334, 23);
            comboBox2.TabIndex = 8;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(191, 201, 199);
            button4.Location = new Point(469, 197);
            button4.Margin = new Padding(0);
            button4.Name = "button4";
            button4.Size = new Size(172, 45);
            button4.TabIndex = 7;
            button4.Text = "Form Link";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // textBox1
            // 
            textBox1.AllowDrop = true;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(76, 109);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(334, 23);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button6
            // 
            button6.BackColor = Color.FromArgb(191, 201, 199);
            button6.Location = new Point(469, 132);
            button6.Name = "button6";
            button6.Size = new Size(172, 45);
            button6.TabIndex = 5;
            button6.Text = "Update215server";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // button5
            // 
            button5.Anchor = AnchorStyles.None;
            button5.BackColor = Color.FromArgb(191, 201, 199);
            button5.CausesValidation = false;
            button5.FlatAppearance.BorderColor = Color.White;
            button5.FlatAppearance.BorderSize = 50;
            button5.ForeColor = SystemColors.ControlText;
            button5.Location = new Point(469, 68);
            button5.Name = "button5";
            button5.Size = new Size(172, 45);
            button5.TabIndex = 3;
            button5.Text = "buildDLL";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(panel1);
            tabPage2.Location = new Point(4, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(726, 406);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(label4);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(button1);
            panel1.Location = new Point(68, 15);
            panel1.Name = "panel1";
            panel1.Size = new Size(590, 286);
            panel1.TabIndex = 9;
            // 
            // label4
            // 
            label4.BackColor = Color.FromArgb(166, 182, 191);
            label4.FlatStyle = FlatStyle.Popup;
            label4.Location = new Point(114, 247);
            label4.Name = "label4";
            label4.Size = new Size(338, 19);
            label4.TabIndex = 13;
            label4.Text = "Password Copied To Clipboard";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            label4.Click += label4_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(86, 139);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(419, 23);
            textBox3.TabIndex = 12;
            textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(140, 98);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(312, 23);
            textBox2.TabIndex = 11;
            textBox2.TextAlign = HorizontalAlignment.Center;
            textBox2.TextChanged += textBox2_TextChanged_1;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Decryption 3.5", "Decryption 4.5", "Decryption Aes" });
            comboBox1.Location = new Point(209, 61);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(166, 23);
            comboBox1.TabIndex = 10;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(101, 118, 120);
            button1.ForeColor = SystemColors.ControlLightLight;
            button1.Location = new Point(209, 185);
            button1.Name = "button1";
            button1.Size = new Size(166, 40);
            button1.TabIndex = 9;
            button1.Text = "Decrypt";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(55, 72, 74);
            panelSidebar.Controls.Add(pictureBox1);
            panelSidebar.Controls.Add(btnDecrypt);
            panelSidebar.Controls.Add(btnBuildDllSidebar);
            panelSidebar.Location = new Point(-2, -1);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(180, 424);
            panelSidebar.TabIndex = 2;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(14, 13);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(137, 63);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // btnDecrypt
            // 
            btnDecrypt.ForeColor = SystemColors.Control;
            btnDecrypt.Location = new Point(14, 197);
            btnDecrypt.Name = "btnDecrypt";
            btnDecrypt.Size = new Size(150, 40);
            btnDecrypt.TabIndex = 1;
            btnDecrypt.Text = "Decryption";
            btnDecrypt.Click += btnDecrypt_Click;
            // 
            // btnBuildDllSidebar
            // 
            btnBuildDllSidebar.ForeColor = SystemColors.ControlLightLight;
            btnBuildDllSidebar.Location = new Point(14, 125);
            btnBuildDllSidebar.Name = "btnBuildDllSidebar";
            btnBuildDllSidebar.Size = new Size(150, 40);
            btnBuildDllSidebar.TabIndex = 2;
            btnBuildDllSidebar.Text = "Build DLL";
            btnBuildDllSidebar.Click += btnBuildDllSidebar_Click;
            // 
            // SourceBuilderForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(839, 373);
            Controls.Add(panelSidebar);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "SourceBuilderForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PathFinder";
            TransparencyKey = Color.FromArgb(255, 192, 192);
            panel2.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelSidebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel2;
        private Button btnDecrypt;
        private Button btnBuildDllSidebar;
        public Panel panelSidebar;
        private PictureBox pictureBox1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button button4;
        private TextBox textBox1;
        private Button button6;
        private Button button5;
        private TabPage tabPage2;
        private Label DestinationFolder;
        private ComboBox comboBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button button2;
        private Panel panel1;
        private TextBox textBox3;
        private TextBox textBox2;
        private ComboBox comboBox1;
        private Button button1;
        private Label label4;
    }
}
