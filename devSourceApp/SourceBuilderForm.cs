using Encrypt_V5;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace devSourceApp
{
    public partial class SourceBuilderForm : Form
    {


        builderMethods builderMethods = new builderMethods();
        public SourceBuilderForm()
        {
            InitializeComponent();
            //StyleModernTextBox(textBox1);
            // Make button inactive (user can’t click it)
            button6.Enabled = false;
            comboBox2.Items.Add("Z:\\Tools\\ADSR Tool V5");
            comboBox2.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            // Later, when you want it to work:
            button4.Enabled = false;
            label3.Visible = false;
            label4.Visible = false;
            this.Icon = new Icon(Path.Combine(Application.StartupPath, "codinghtml_117947.ico"));

        }

        //BuildDLL
        private void button5_Click(object sender, EventArgs e)
        {
            //buildDLL
            label3.Visible = true;
            label3.Refresh();
            button6.Enabled = false;
            button4.Enabled = false;


            SourceBuilder sourceBuilder = new SourceBuilder();
            if (textBox1.Text == "")
            {
                // MessageBox.Show("Please Mention the Path");
                ModernMessageBox.ShowBox("Please Mention the Path");

            }
            else
            {
                try
                {

                    sourceBuilder.GenerateFile(textBox1.Text, "32");
                    if (sourceBuilder.GenerateFile(textBox1.Text, "64"))
                    {
                        // ModernMessageBox.ShowBox("Build Succeed");
                        button6.Enabled = true;
                    }
                    else { ModernMessageBox.ShowBox("BuildError"); }
                }

                catch (Exception ex) { ModernMessageBox.ShowBox("BuildError"); }
            }
            label3.Visible = false;
            button6.Focus();


        }
        //Update215Server
        private void button6_Click(object sender, EventArgs e)
        {
            string[] csprojFiles = Directory.GetFiles(textBox1.Text, "*.csproj");
            string FileName = Path.GetFileNameWithoutExtension(csprojFiles[0]);
            if (csprojFiles.Length == 1)
            {
                string _filePathtoMove = Path.Combine(textBox1.Text, "bin\\Debug\\", FileName, DateTime.Today.ToString("dd-MMM-yyyy"));

                string[] filesToUpload = Directory.GetFiles(_filePathtoMove, "*");
                string destRoot = comboBox2.Text;
                if(Directory.Exists(destRoot))
                {
                    builderMethods.CopyFoldersFromNearestNamedAncestor(filesToUpload, FileName, destRoot);
                    Clipboard.SetText(Path.Combine("\\\\10.10.13.215\\development\\Tools\\ADSR Tools V5", FileName, DateTime.Today.ToString("dd-MMM-yyyy")));
                    ModernMessageBox.ShowBox("Files Moved to Server and Path Copied");
                    button4.Enabled = true;
                    button4.Focus();
                }
                else
                {
                    ModernMessageBox.ShowBox("Directory Not Exists");
                }
                

            }
        }

        //FormLink
        private void button4_Click(object sender, EventArgs e)
        {
            string url = "http://10.0.5.159:5252/"; // <-- Put your URL here
            try
            {
                // This works in .NET Framework and .NET Core/6+
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot open website: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //TextBox for sourceFilePath
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void btnFormLink_Click(object sender, EventArgs e)
        {

        }

        private void btnBuildDllSidebar_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        



        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DestinationFolder_Click(object sender, EventArgs e)
        {

        }

       

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            button6.Enabled = false;
            button4.Enabled = false;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string decryptedText = textBox2.Text;
            Encrypt _eS = new Encrypt();
            if (comboBox1.Text.Contains("3"))
            {
                textBox3.Text = _eS.DecPassword35(decryptedText);
            }
            else if (comboBox1.Text.Contains("4"))
            {
                textBox3.Text = _eS.DecPassword45(decryptedText);
            }
            else if (comboBox1.Text.Contains("Aes"))
            {

                textBox3.Text = _eS.decrypt(decryptedText);
            }
            else
            {
                ModernMessageBox.ShowBox("Please Check Encrption Method", "Danger");
            }
            Clipboard.SetText(textBox3.Text);
            label4.Visible = true;
            //ModernMessageBox.ShowBox("Copied To Clipboard");
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            label4.Visible = false;
        }
    }
}
