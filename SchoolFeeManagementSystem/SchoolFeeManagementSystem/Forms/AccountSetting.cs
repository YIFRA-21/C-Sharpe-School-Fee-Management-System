using SchoolFeeManagemetSystem.API.DTOs;
using System.Drawing;
using System.IO;
using static SchoolFeeManagemetSystem.API.DTOs.UserDTOs;

namespace SchoolFeeManagementSystem.Forms
{
    public partial class AccountSetting : Form
    {
        private readonly ApiClient api =  new ApiClient();

        private string selectedImage = "";

        private ErrorProvider errorProvider1 =
            new ErrorProvider();

        public AccountSetting()
        {
            InitializeComponent();

            Load += AccountSetting_Load;
        }

        // ================= LOAD FORM =================

        private async void AccountSetting_Load(
            object sender,
            EventArgs e)
        {
            try
            {
                // PASSWORD HIDE
                txtOldPassword.UseSystemPasswordChar = true;
                txtNewPassword.UseSystemPasswordChar = true;
                txtConfirmPassword.UseSystemPasswordChar = true;

                // ROLE
                cmbRole.Items.Clear();

                cmbRole.Items.Add("Admin");
                cmbRole.Items.Add("Staff");

                // IMAGE
                pictureBoxLogo.SizeMode =
                    PictureBoxSizeMode.StretchImage;

                // SHOW PASSWORD EVENT
                chkShowPassword.CheckedChanged +=
                    chkShowPassword_CheckedChanged;

                // LOAD USER
                await LoadUserData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ================= LOAD USER =================

        private async Task LoadUserData()
        {
            try
            {
                var users =
                    await api.GetAllAsync<UserDTO>("User");

                var user =
                    users.FirstOrDefault(x =>
                        x.Id == LoggedInUser.UserId);

                if (user == null)
                {
                    MessageBox.Show("User Not Found");
                    return;
                }

                txtFullName.Text =
                    user.FullName;

                txtUsername.Text =
                    user.Username;

                cmbRole.Text =
                    user.Role;

                // LOAD IMAGE
                if (!string.IsNullOrWhiteSpace(
                    user.LogoPath))
                {
                    if (File.Exists(user.LogoPath))
                    {
                        using (FileStream fs = new FileStream(user.LogoPath,  FileMode.Open,  FileAccess.Read))
                        {
                            pictureBoxLogo.Image =
                                Image.FromStream(fs);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    

        private void btnChangeLogo_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                OpenFileDialog open =
                    new OpenFileDialog();

                open.Filter =
                    "Image Files|*.jpg;*.jpeg;*.png";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    selectedImage =
                        open.FileName;

                    pictureBoxLogo.Image =
                        Image.FromFile(selectedImage);

                    pictureBoxLogo.SizeMode =
                        PictureBoxSizeMode.StretchImage;

                    lblStatus.ForeColor =
                        Color.Green;

                    lblStatus.Text =
                        "Logo Selected Successfully";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ================= MAKE CHANGE =================

        private async void btnMakeChange_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                errorProvider1.Clear();

                // ================= VALIDATION =================

                bool isValid = true;

                if (string.IsNullOrWhiteSpace(
                    txtFullName.Text))
                {
                    errorProvider1.SetError(
                        txtFullName,
                        "Full Name Required");

                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(
                    txtUsername.Text))
                {
                    errorProvider1.SetError(
                        txtUsername,
                        "Username Required");

                    isValid = false;
                }

                if (cmbRole.SelectedIndex == -1)
                {
                    errorProvider1.SetError(
                        cmbRole,
                        "Select Role");

                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(
                    txtOldPassword.Text))
                {
                    errorProvider1.SetError(
                        txtOldPassword,
                        "Old Password Required");

                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(
                    txtNewPassword.Text))
                {
                    errorProvider1.SetError(
                        txtNewPassword,
                        "New Password Required");

                    isValid = false;
                }

                if (txtNewPassword.Text !=
                    txtConfirmPassword.Text)
                {
                    errorProvider1.SetError(
                        txtConfirmPassword,
                        "Password Not Match");

                    isValid = false;
                }

                if (!isValid)
                    return;

                // ================= GET USER =================

                var users =
                    await api.GetAllAsync<UserDTO>("User");

                var user =
                    users.FirstOrDefault(x =>
                        x.Id == LoggedInUser.UserId);

                if (user == null)
                {
                    MessageBox.Show(
                        "User Not Found");

                    return;
                }

                // ================= CHECK OLD PASSWORD =================

                if (user.Password.Trim() !=
                    txtOldPassword.Text.Trim())
                {
                    MessageBox.Show(
                        "Old Password Incorrect");

                    return;
                }

                // ================= DTO =================

                UpdateAccountDTO dto =
                    new UpdateAccountDTO()
                    {
                        Id = user.Id,

                        FullName =
                            txtFullName.Text.Trim(),

                        Username =
                            txtUsername.Text.Trim(),

                        OldPassword =
                            txtOldPassword.Text.Trim(),

                        NewPassword =
                            txtNewPassword.Text.Trim(),

                        LogoPath =
                            string.IsNullOrWhiteSpace(
                                selectedImage)
                            ? user.LogoPath
                            : selectedImage
                    };


                await api.PutAsync(
                    "User/update-account",
                    dto);

             

                LoggedInUser.FullName =
                    dto.FullName;

                LoggedInUser.Username =
                    dto.Username;

                LoggedInUser.LogoPath =
                    dto.LogoPath;

                // ================= SUCCESS =================

                lblStatus.ForeColor =
                    Color.Green;

                lblStatus.Text =
                    "Account Updated Successfully";

                MessageBox.Show(
                    "Account Updated Successfully");

                // CLEAR PASSWORDS
                txtOldPassword.Clear();

                txtNewPassword.Clear();

                txtConfirmPassword.Clear();
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor =
                    Color.Red;

                lblStatus.Text =
                    "Update Failed";

                MessageBox.Show(ex.Message);
            }
        }

        // ================= SHOW / HIDE PASSWORD =================

        private void chkShowPassword_CheckedChanged(
            object sender,
            EventArgs e)
        {
            bool show =
                chkShowPassword.Checked;

            txtOldPassword.UseSystemPasswordChar =
                !show;

            txtNewPassword.UseSystemPasswordChar =
                !show;

            txtConfirmPassword.UseSystemPasswordChar =
                !show;
        }
    }
}