using Microsoft.AspNetCore.Identity.Data;
using SchoolFeeManagementSystem.Forms;
using static SchoolFeeManagemetSystem.API.DTOs.UserDTOs;

namespace SchoolFeeManagementSystem
{
    public partial class Login : Form
    {
        private readonly ApiClient _api = new ApiClient();
        public Login()
        {
            InitializeComponent();
            
        }
        private void LoginForm_Load( object sender,EventArgs e)
        {
            txtPassword.PasswordChar = '*';
            cmbRole.Items.Clear();
            cmbRole.SelectedIndex = 0;
            lblStatus.Text = "";
            lblStatus.ForeColor = Color.Blue;
        }
        private async void btnLogin_Click( object sender,EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(
                    txtUsername.Text))
                {
                    MessageBox.Show(
                        "Enter Username");

                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(
                    txtPassword.Text))
                {
                    MessageBox.Show(
                        "Enter Password");

                    txtPassword.Focus();
                    return;
                }

                lblStatus.ForeColor = Color.Blue;
                lblStatus.Text = "Checking Login...";

                btnLogin.Enabled = false;

                LoginDTO dto = new LoginDTO()
                {
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text.Trim()
                };

                var user = await _api.LoginAsync(dto);

                if (user == null)
                {
                    lblStatus.ForeColor = Color.Red;

                    lblStatus.Text ="Invalid Username or Password";

                    MessageBox.Show("Login Failed!");

                    return;

                }

                if (user.Role == "Admin")
                {
                    lblStatus.ForeColor = Color.Green;

                    lblStatus.Text = "Admin Login Success";
                    MessageBox.Show("Welcome Admin : " + user.FullName);
                    Staff frm =new Admin();

                    frm.Show();

                    this.Hide();
                }

                else if (user.Role == "Staff")
                {
                    lblStatus.ForeColor = Color.Green;

                    lblStatus.Text = "Staff Login Success";
                    MessageBox.Show("Welcome Staff : " + user.FullName);
                    Staff frm = new Staff();

                    frm.Show();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show(
                        "Invalid User Role");

                    lblStatus.ForeColor = Color.Red;

                    lblStatus.Text =
                        "Role Error";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message);

                lblStatus.ForeColor = Color.Red;

                lblStatus.Text =
                    "System Error";
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }
        private void chkShowPassword_CheckedChanged(object sender,EventArgs e)

        {
            if (chkShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }
    }
}

