using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

public class ModernMessageBox : Form
{
    private Label lblMessage;
    private Button btnOk;

    public ModernMessageBox(string message, string title = "Message")
    {
        // Form setup
        this.Text = title;
        this.FormBorderStyle = FormBorderStyle.None;
        this.StartPosition = FormStartPosition.CenterParent;
       
        this.BackColor = Color.FromArgb(164, 186, 177); // Background color
        this.Padding = new Padding(12);

        //OK button setup
        btnOk = new Button()
        {
            Text = "OK",
            BackColor = Color.FromArgb(193, 218, 224),
            ForeColor = Color.FromArgb(60, 79, 83),
            FlatStyle = FlatStyle.Popup,
            Width = 90,
            Height = 36
        };
        btnOk.FlatAppearance.BorderSize = 0;
        btnOk.Click += (s, e) => this.Close();

        // Message label setup
        lblMessage = new Label()
        {
            Text = message,
            AutoSize = true, // shrink to text size
            Font = new Font("Segoe UI", 11, FontStyle.Regular),
            ForeColor = Color.White,
            TextAlign = ContentAlignment.MiddleCenter
        };

        // Panel to center label
        Panel centerPanel = new Panel() { Dock = DockStyle.Fill };
        centerPanel.Controls.Add(lblMessage);

        centerPanel.Resize += (s, e) =>
        {
            lblMessage.Location = new Point(
                (centerPanel.Width - lblMessage.Width) / 2,
                (centerPanel.Height - lblMessage.Height) / 2
            );
        };

        // Bottom panel for button
        Panel bottomPanel = new Panel() { Dock = DockStyle.Bottom, Height = 60 };
        bottomPanel.Controls.Add(btnOk);

        bottomPanel.Resize += (s, e) =>
        {
            btnOk.Location = new Point(
                (bottomPanel.Width - btnOk.Width) / 2,
                (bottomPanel.Height - btnOk.Height) / 2
            );
        };

        this.Controls.Add(centerPanel);
        this.Controls.Add(bottomPanel);

        // Calculate form size based on label
        using (Graphics g = this.CreateGraphics())
        {
            SizeF textSize = g.MeasureString(message, lblMessage.Font, 400); // max width 400px
            this.ClientSize = new Size(
                Math.Max((int)textSize.Width + 40, 300),
                (int)textSize.Height + 100
            );
        }

        // Rounded corners
        this.Region = Region.FromHrgn(NativeMethods.CreateRoundRectRgn(0, 0, this.Width, this.Height, 12, 12));
    }

    public static void ShowBox(string message, string title = "Message")
    {
        using (ModernMessageBox box = new ModernMessageBox(message, title))
        {
            box.ShowDialog();
        }
    }
}

// Native method for rounded corners
internal class NativeMethods
{
    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    public static extern IntPtr CreateRoundRectRgn(
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
    );
}

// Usage example
// ModernMessageBox.ShowBox("Files Moved to Server and Path Copied");
