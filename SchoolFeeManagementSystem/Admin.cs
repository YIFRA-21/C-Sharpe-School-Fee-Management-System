using FontAwesome.Sharp;
using System.Runtime.InteropServices;

namespace SchoolFeeManagementSystem
{
    public partial class Staff : Form
    {
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        public Staff()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);

            //Form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }


        private struct RGBColors
        {
            public static System.Drawing.Color color1 = System.Drawing.Color.FromArgb(172, 126, 241);
            public static System.Drawing.Color color2 = System.Drawing.Color.FromArgb(249, 118, 176);
            public static System.Drawing.Color color3 = System.Drawing.Color.FromArgb(253, 138, 114);
            public static System.Drawing.Color color4 = System.Drawing.Color.FromArgb(95, 77, 221);
            public static System.Drawing.Color color5 = System.Drawing.Color.FromArgb(249, 88, 155);
            public static System.Drawing.Color color6 = System.Drawing.Color.FromArgb(24, 161, 251);
            public static System.Drawing.Color color7 = System.Drawing.Color.FromArgb(9, 237, 237);

        }

        private void ActiveButton(object senderBtn, System.Drawing.Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = System.Drawing.Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //Left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                iconButtonDashboard.IconChar = currentBtn.IconChar;
                iconButtonDashboard.IconColor = color;
            }
        }


        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = System.Drawing.Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = System.Drawing.Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = System.Drawing.Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }


        private void btnHome_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconButtonDashboard.IconChar = IconChar.Home;
            iconButtonDashboard.IconColor = System.Drawing.Color.MediumPurple;
            HomeTitle.Text = "Home";
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGBColors.color1);
            openChildForm(new Forms.Dashboard());
        }

        private void btnStudentManagement_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGBColors.color2);
            openChildForm(new Forms.StudentManagement());
        }

        private void btnFeeSection_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGBColors.color3);
            openChildForm(new Forms.Fees());
        }

        private void btnPaymentManagement_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGBColors.color4);
            openChildForm(new Forms.PaymentManagement());
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGBColors.color5);
            openChildForm(new Forms.AccountSetting());
        }

        private void btnReportSection_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGBColors.color6);
            openChildForm(new Forms.ViewReports());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGBColors.color7);
        }



        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        private void openChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktopPane.Controls.Add(childForm);
            panelDesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            HomeTitle.Text = childForm.Text;
        }

        private void IconeMinimaize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void IconeMaximaize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }


        private void iconeExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panelTitleBar_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}



