using FontAwesome.Sharp;
using System.Runtime.InteropServices;

namespace SchoolFeeManagementSystem
{
    public partial class StaffRole : Form
    {
        // ================= VARIABLES =================

        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;

        // ================= CONSTRUCTOR =================

        public StaffRole()
        {
            InitializeComponent();

            // Left Border Panel
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            StaffpanelMenu.Controls.Add(leftBorderBtn);

            // Form Settings
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds =
                Screen.FromHandle(this.Handle).WorkingArea;
        }

        // ================= COLORS =================

        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
            public static Color color7 = Color.FromArgb(9, 237, 237);
        }

        // ================= ACTIVATE BUTTON =================

        private void ActiveButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();

                currentBtn = (IconButton)senderBtn;

                // Button Style
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation =
                    TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                // Left Border
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location =
                    new Point(0, currentBtn.Location.Y);

                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                // Top Icon
                StafficonButtonDashboard.IconChar =
                    currentBtn.IconChar;

                StafficonButtonDashboard.IconColor = color;
            }
        }

        // ================= DISABLE BUTTON =================

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor =
                    Color.FromArgb(31, 30, 68);

                currentBtn.ForeColor = Color.Gainsboro;

                currentBtn.TextAlign =
                    ContentAlignment.MiddleLeft;

                currentBtn.IconColor = Color.Gainsboro;

                currentBtn.TextImageRelation =
                    TextImageRelation.ImageBeforeText;

                currentBtn.ImageAlign =
                    ContentAlignment.MiddleLeft;
            }
        }

        // ================= RESET HOME =================

        private void Reset()
        {
            DisableButton();

            leftBorderBtn.Visible = false;

            StafficonButtonDashboard.IconChar =
                IconChar.Home;

            StafficonButtonDashboard.IconColor =
                Color.MediumPurple;

            StaffHomeTitle.Text = "Home";

            // Close current child form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            StaffpanelDesktopPane.Controls.Clear();
        }

        // ================= OPEN CHILD FORM =================

        private void openChildForm(Form childForm)
        {
            // Close old form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            // Clear old controls
            StaffpanelDesktopPane.Controls.Clear();

            currentChildForm = childForm;

            childForm.TopLevel = false;
            childForm.FormBorderStyle =
                FormBorderStyle.None;

            childForm.Dock = DockStyle.Fill;

            StaffpanelDesktopPane.Controls.Add(childForm);

            StaffpanelDesktopPane.Tag = childForm;

            childForm.BringToFront();

            childForm.Show();

            StaffHomeTitle.Text = childForm.Text;
        }

        // ================= HOME =================

        private void StaffbtnHome_Click(object sender, EventArgs e)
        {
            Reset();
        }

        // ================= DASHBOARD =================

        private void StaffbtnDashboard_Click_1(object sender, EventArgs e)
        {
            ActiveButton(sender, RGBColors.color1);

            openChildForm(new Forms.Dashboard());
        }

        // ================= STUDENT MANAGEMENT =================

        private void StaffbtnStudentManagement_Click_1(object sender, EventArgs e)
        {
            ActiveButton(sender, RGBColors.color2);

            openChildForm(new Forms.StudentManagement());
        }

        // ================= FEES SECTION =================

        private void StaffbtnFeeSection_Click_1(
            object sender,
            EventArgs e)
        {
            ActiveButton(sender, RGBColors.color3);

            openChildForm(new Forms.Fees());
        }

        // ================= PAYMENT MANAGEMENT =================
        private void staffbtnPaymentManagement_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGBColors.color4);

            openChildForm(new Forms.PaymentManagement());
        }


        // ================= REPORT SECTION =================

        private void StaffbtnReportSection_Click_1(object sender, EventArgs e)
        {
            ActiveButton(sender, RGBColors.color6);

            openChildForm(new Forms.ViewReports());
        }

        // ================= LOGOUT =================

        private void StaffbtnLogout_Click_1(object sender, EventArgs e)
        {
            ActiveButton(sender, RGBColors.color7);

            DialogResult result = MessageBox.Show(
                "Do you want to logout?",
                "Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Login loginForm = new Login();

                loginForm.Show();

                this.Hide();
            }
        }

        // ================= WINDOW DRAG =================

        [DllImport("user32.DLL",
            EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL",
            EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void StaffpanelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();

            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        // ================= MINIMIZE =================

        private void iconeMinimaize1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        // ================= MAXIMIZE =================
        private void iconMaximaize1_Click(object sender, EventArgs e)
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
        private void iconExit1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}