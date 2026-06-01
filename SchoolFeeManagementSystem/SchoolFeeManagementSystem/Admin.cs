using FontAwesome.Sharp;
using System.Runtime.InteropServices;
using System.IO;

namespace SchoolFeeManagementSystem
{
    public partial class Admin : Form
    {
        private IconButton currentBtn;

        private Panel leftBorderBtn;

        private Form currentChildForm;

        public Admin()
        {
            InitializeComponent();

            leftBorderBtn = new Panel();

            leftBorderBtn.Size = new Size(7, 60);

            panelMenu.Controls.Add(leftBorderBtn);

            // FORM SETTINGS
            this.Text = string.Empty;

            this.ControlBox = false;

            this.DoubleBuffered = true;

            this.MaximizedBounds =
                Screen.FromHandle(this.Handle).WorkingArea;

            // LOAD USER LOGO
            LoadUserLogo();
        }

        // ================= LOAD USER LOGO =================

        private void LoadUserLogo()
        {
            try
            {
                // CHECK LOGO PATH

                if (!string.IsNullOrEmpty(
                    LoggedInUser.LogoPath))
                {
                    // CHECK FILE EXISTS

                    if (File.Exists(
                        LoggedInUser.LogoPath))
                    {
                        pictureBoxLogo.Image =
                            Image.FromFile(
                                LoggedInUser.LogoPath);

                        pictureBoxLogo.SizeMode =
                            PictureBoxSizeMode.StretchImage;
                    }
                }
            }
            catch
            {
                MessageBox.Show(
                    "Failed To Load Logo");
            }
        }

        // ================= COLORS =================

        private struct RGBColors
        {
            public static Color color1 =  Color.FromArgb(172, 126, 241);

            public static Color color2 = Color.FromArgb(249, 118, 176);

            public static Color color3 = Color.FromArgb(253, 138, 114);

            public static Color color4 = Color.FromArgb(95, 77, 221);

            public static Color color5 = Color.FromArgb(249, 88, 155);

            public static Color color6 = Color.FromArgb(24, 161, 251);
            public static Color color7 = Color.FromArgb(9, 237, 237);
        }

        // ================= ACTIVE BUTTON =================

        private void ActiveButton(
            object senderBtn,
            Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();

                currentBtn =
                    (IconButton)senderBtn;

                currentBtn.BackColor =
                    Color.FromArgb(37, 36, 81);

                currentBtn.ForeColor =
                    color;

                currentBtn.TextAlign =
                    ContentAlignment.MiddleCenter;

                currentBtn.IconColor =
                    color;

                currentBtn.TextImageRelation =
                    TextImageRelation.TextBeforeImage;

                currentBtn.ImageAlign =
                    ContentAlignment.MiddleRight;

                // LEFT BORDER

                leftBorderBtn.BackColor =
                    color;

                leftBorderBtn.Location =
                    new Point(
                        0,
                        currentBtn.Location.Y);

                leftBorderBtn.Visible = true;

                leftBorderBtn.BringToFront();

                iconButtonDashboard.IconChar =
                    currentBtn.IconChar;

                iconButtonDashboard.IconColor =
                    color;
            }
        }

        // ================= DISABLE BUTTON =================

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor =
                    Color.FromArgb(31, 30, 68);

                currentBtn.ForeColor =
                    Color.Gainsboro;

                currentBtn.TextAlign =
                    ContentAlignment.MiddleLeft;

                currentBtn.IconColor =
                    Color.Gainsboro;

                currentBtn.TextImageRelation =
                    TextImageRelation.ImageBeforeText;

                currentBtn.ImageAlign =
                    ContentAlignment.MiddleLeft;
            }
        }

        // ================= RESET =================

        private void Reset()
        {
            DisableButton();

            leftBorderBtn.Visible = false;

            iconButtonDashboard.IconChar =
                IconChar.Home;

            iconButtonDashboard.IconColor =
                Color.MediumPurple;

            HomeTitle.Text = "Home";
        }

        // ================= HOME =================

        private void btnHome_Click(object sender, EventArgs e)
        {
            Reset();
        }

        // ================= DASHBOARD =================

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGBColors.color1);

            openChildForm( new Forms.Dashboard());
        }

        // ================= STUDENT =================

        private void btnStudentManagement_Click( object sender, EventArgs e)
        {
            ActiveButton(sender,RGBColors.color2);

            openChildForm( new Forms.StudentManagement());
        }

        // ================= STAFF =================

        private void btnStaffManagement_Click(object sender,EventArgs e)
        {
            ActiveButton( sender, RGBColors.color2);

            openChildForm(new Forms.StaffManagement());
        }

        // ================= FEES =================

        private void btnFeeSection_Click( object sender,  EventArgs e)
        {
            ActiveButton(  sender, RGBColors.color3);

            openChildForm(new Forms.Fees());
        }

        // ================= PAYMENT =================

        private void btnPaymentManagement_Click(
            object sender,
            EventArgs e)
        {
            ActiveButton( sender, RGBColors.color4);
        openChildForm( new Forms.PaymentManagement());
        }

        // ================= SETTINGS =================

        private void btnSetting_Click(object sender,EventArgs e)
        {
            ActiveButton(sender, RGBColors.color5);

            openChildForm(new Forms.AccountSetting());
        }

        // ================= REPORT =================

        private void btnReportSection_Click( object sender, EventArgs e)
        {
            ActiveButton( sender,RGBColors.color6);

            openChildForm( new Forms.ViewReports());
        }

        // ================= LOGOUT =================

        private void btnLogout_Click( object sender, EventArgs e)
        {
            Login loginForm =new Login();

            loginForm.Show();

            this.Hide();
        }

        // ================= MOVE FORM =================

        [DllImport(
            "user32.DLL",
            EntryPoint = "ReleaseCapture")]

        private extern static void
            ReleaseCapture();

        [DllImport(
            "user32.DLL",
            EntryPoint = "SendMessage")]

        private extern static void
            SendMessage(
            IntPtr hWnd,
            int wMsg,
            int wParam,
            int lParam);

        private void panelTitleBar_MouseDown(
            object sender,
            MouseEventArgs e)
        {
            ReleaseCapture();

            SendMessage(
                this.Handle,
                0x112,
                0xf012,
                0);
        }

        // ================= OPEN CHILD FORM =================

        private void openChildForm(
            Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            currentChildForm =
                childForm;

            childForm.TopLevel = false;

            childForm.FormBorderStyle =
                FormBorderStyle.None;

            childForm.Dock =
                DockStyle.Fill;

            panelDesktopPane.Controls.Add(
                childForm);

            panelDesktopPane.Tag =
                childForm;

            childForm.BringToFront();

            childForm.Show();

            HomeTitle.Text =
                childForm.Text;
        }

        // ================= MINIMIZE =================

        private void IconeMinimaize_Click(
            object sender,
            EventArgs e)
        {
            WindowState =
                FormWindowState.Minimized;
        }

        // ================= MAXIMIZE =================

        private void IconeMaximaize_Click(
            object sender,
            EventArgs e)
        {
            if (WindowState ==
                FormWindowState.Normal)
            {
                WindowState =
                    FormWindowState.Maximized;
            }
            else
            {
                WindowState =
                    FormWindowState.Normal;
            }
        }

        // ================= EXIT =================

        private void iconeExit_Click(
            object sender,
            EventArgs e)
        {
            Application.Exit();
        }
    }
}