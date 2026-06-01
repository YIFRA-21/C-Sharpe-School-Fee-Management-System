using SchoolFeeManagementSystem.Forms;
using static SchoolFeeManagemetSystem.API.DTOs.UserDTOs;

namespace SchoolFeeManagementSystem
{
    public partial class Login : Form
    {
        private readonly ApiClient _api = new ApiClient();

        private ErrorProvider errorProvider = new ErrorProvider();

        public Login()
        {
            InitializeComponent();

            Load += Login_Load;
        }

        // ================= FORM LOAD =================

        private void Login_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;

            cmbRole.Items.Clear();

            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Staff");

            cmbRole.SelectedIndex = -1;

            lblStatus.Text = "";

            lblStatus.ForeColor = Color.Blue;

            chkShowPassword.Checked = false;

            chkShowPassword.CheckedChanged +=
                chkShowPassword_CheckedChanged;
        }

        // ================= LOGIN BUTTON =================

        private async void btnLogin_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                // CLEAR ERROR
                errorProvider.Clear();

                // ================= VALIDATION =================

                bool isValid = true;

                if (string.IsNullOrWhiteSpace(
                    txtUsername.Text))
                {
                    errorProvider.SetError(
                        txtUsername,
                        "Username Required");

                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(
                    txtPassword.Text))
                {
                    errorProvider.SetError(
                        txtPassword,
                        "Password Required");

                    isValid = false;
                }

                if (cmbRole.SelectedIndex == -1)
                {
                    errorProvider.SetError(
                        cmbRole,
                        "Select Role");

                    isValid = false;
                }

                if (!isValid)
                {
                    return;
                }

                // ================= STATUS =================

                lblStatus.ForeColor =
                    Color.Blue;

                lblStatus.Text =
                    "Checking Login...";

                btnLogin.Enabled = false;

                // ================= LOGIN DTO =================

                LoginDTO dto = new LoginDTO()
                {
                    Username =
                        txtUsername.Text.Trim(),

                    Password =
                        txtPassword.Text.Trim()
                };

                // ================= API LOGIN =================

                var user =
                    await _api.LoginAsync(dto);

                // ================= INVALID LOGIN =================

                if (user == null)
                {
                    lblStatus.ForeColor =
                        Color.Red;

                    lblStatus.Text =
                        "Invalid Username or Password";

                    MessageBox.Show(
                        "Wrong Username or Password");

                    txtPassword.Clear();

                    txtPassword.Focus();

                    return;
                }

                // ================= ROLE CHECK =================

                if (user.Role != cmbRole.Text)
                {
                    MessageBox.Show(
                        "Selected Role Incorrect");

                    lblStatus.ForeColor =
                        Color.Red;

                    lblStatus.Text =
                        "Role Not Match";

                    return;
                }


                LoggedInUser.UserId =
                    user.Id;

                LoggedInUser.Username =
                    user.Username;

                LoggedInUser.FullName =
                    user.FullName;

                LoggedInUser.Role =
                    user.Role;

                LoggedInUser.LogoPath =
                    user.LogoPath;

                // ================= SUCCESS =================

                lblStatus.ForeColor =
                    Color.Green;

                lblStatus.Text =
                    "Login Success";

                MessageBox.Show(
                    "Welcome " +
                    user.FullName);

                // ================= ADMIN =================

                if (user.Role == "Admin")
                {
                    Admin frm =
                        new Admin();

                    frm.Show();

                    this.Hide();
                }

                // ================= STAFF =================

                else if (user.Role == "Staff")
                {
                    StaffRole frm =
                        new StaffRole();

                    frm.Show();

                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor =
                    Color.Red;

                lblStatus.Text =
                    "System Error";

                MessageBox.Show(
                    ex.Message);
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }

        // ================= SHOW / HIDE PASSWORD =================

        private void chkShowPassword_CheckedChanged(
            object sender,
            EventArgs e)
        {
            txtPassword.UseSystemPasswordChar =
                !chkShowPassword.Checked;
        }

        // ================= CLEAR BUTTON =================

        private void btnClear_Click(
            object sender,
            EventArgs e)
        {
            txtUsername.Clear();

            txtPassword.Clear();

            cmbRole.SelectedIndex = -1;

            lblStatus.Text = "";

            errorProvider.Clear();

            txtUsername.Focus();
        }

        // ================= EXIT BUTTON =================

        private void btnExit_Click(
            object sender,
            EventArgs e)
        {
            Application.Exit();
        }
    }
}