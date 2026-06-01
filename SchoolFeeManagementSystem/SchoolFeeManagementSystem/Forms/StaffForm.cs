using System.Drawing;
using System.Text.RegularExpressions;
using static SchoolFeeManagemetSystem.API.DTOs.StaffDTOs;

namespace SchoolFeeManagementSystem.Forms
{
    public partial class StaffForm : Form
    {
        // ================= API =================

        private readonly ApiClient _api = new ApiClient();

        // ================= STAFF ID =================

        public int StaffId = 0;

        // ================= ERROR PROVIDER =================

        private ErrorProvider errorProvider =
            new ErrorProvider();

        // ================= ADD MODE =================

        public StaffForm()
        {
            InitializeComponent();

            errorProvider.BlinkStyle =
                ErrorBlinkStyle.NeverBlink;

            LoadPositions();

            LoadDepartments();

            AddValidationEvents();
        }

        // ================= UPDATE MODE =================

        public StaffForm(StaffDtos staff)
        {
            InitializeComponent();

            errorProvider.BlinkStyle =
                ErrorBlinkStyle.NeverBlink;

            LoadPositions();

            LoadDepartments();

            AddValidationEvents();

            StaffId = staff.StaffId;

            txtFullName.Text = staff.FullName;

            cmbPosition.Text = staff.Position;

            cmbDepartment.Text = staff.Department;

            txtEmail.Text = staff.Email;

            txtPhone.Text = staff.Phone;

            txtAddress.Text = staff.Address;

            txtSalary.Text = staff.Salary.ToString();
        }

        // ================= VALIDATION EVENTS =================

        private void AddValidationEvents()
        {
            txtFullName.Validating +=
                txtFullName_Validating;

            cmbPosition.Validating +=
                cmbPosition_Validating;

            cmbDepartment.Validating +=
                cmbDepartment_Validating;

            txtEmail.Validating +=
                txtEmail_Validating;

            txtPhone.Validating +=
                txtPhone_Validating;

            txtAddress.Validating +=
                txtAddress_Validating;

            txtSalary.Validating +=
                txtSalary_Validating;
        }

        // ================= POSITION =================

        private void LoadPositions()
        {
            cmbPosition.Items.Clear();

            cmbPosition.Items.Add("Principal");
            cmbPosition.Items.Add("Teacher");
            cmbPosition.Items.Add("Accountant");
            cmbPosition.Items.Add("Librarian");
            cmbPosition.Items.Add("IT Support");
        }

        // ================= DEPARTMENT =================

        private void LoadDepartments()
        {
            cmbDepartment.Items.Clear();

            cmbDepartment.Items.Add("Administration");
            cmbDepartment.Items.Add("Science Department");
            cmbDepartment.Items.Add("Finance Department");
            cmbDepartment.Items.Add("Library");
            cmbDepartment.Items.Add("IT Department");
        }

        // ================= SET ERROR =================

        private void SetError(
            Control control,
            string message)
        {
            errorProvider.SetError(control, message);

            control.BackColor = Color.MistyRose;
        }

        // ================= CLEAR ERROR =================

        private void ClearError(Control control)
        {
            errorProvider.SetError(control, "");

            control.BackColor = Color.White;
        }

        // ================= VALIDATE FORM =================

        private bool ValidateForm()
        {
            bool isValid = true;

            errorProvider.Clear();

            // Full Name
            if (string.IsNullOrWhiteSpace(
                txtFullName.Text))
            {
                SetError(
                    txtFullName,
                    "Full Name is required");

                isValid = false;
            }
            else if (!Regex.IsMatch(
                txtFullName.Text,
                @"^[a-zA-Z\s]+$"))
            {
                SetError(
                    txtFullName,
                    "Only letters allowed");

                isValid = false;
            }

            // Position
            if (cmbPosition.SelectedIndex == -1)
            {
                SetError(
                    cmbPosition,
                    "Select Position");

                isValid = false;
            }

            // Department
            if (cmbDepartment.SelectedIndex == -1)
            {
                SetError(
                    cmbDepartment,
                    "Select Department");

                isValid = false;
            }

            // Email
            if (string.IsNullOrWhiteSpace(
                txtEmail.Text))
            {
                SetError(
                    txtEmail,
                    "Email is required");

                isValid = false;
            }
            else if (!Regex.IsMatch(
                txtEmail.Text,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                SetError(
                    txtEmail,
                    "Invalid Email");

                isValid = false;
            }

            // Phone
            if (!Regex.IsMatch(
                txtPhone.Text,
                @"^[0-9]{10}$"))
            {
                SetError(
                    txtPhone,
                    "Phone must be 10 digits");

                isValid = false;
            }

            // Address
            if (string.IsNullOrWhiteSpace(
                txtAddress.Text))
            {
                SetError(
                    txtAddress,
                    "Address is required");

                isValid = false;
            }

            // Salary
            if (string.IsNullOrWhiteSpace(
                txtSalary.Text))
            {
                SetError(
                    txtSalary,
                    "Salary is required");

                isValid = false;
            }
            else if (!decimal.TryParse(
                txtSalary.Text,
                out decimal salary))
            {
                SetError(
                    txtSalary,
                    "Invalid Salary");

                isValid = false;
            }

            return isValid;
        }

        // ================= SAVE BUTTON =================

        private async void btnSave_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                // Validate
                if (!ValidateForm())
                {
                    MessageBox.Show(
                        "Please correct all errors.",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // ================= INSERT =================

                if (StaffId == 0)
                {
                    CreateStaffDto dto =
                        new CreateStaffDto()
                        {
                            FullName =
                                txtFullName.Text.Trim(),

                            Position =
                                cmbPosition.Text,

                            Department =
                                cmbDepartment.Text,

                            Email =
                                txtEmail.Text.Trim(),

                            Phone =
                                txtPhone.Text.Trim(),

                            Address =
                                txtAddress.Text.Trim(),

                            Salary =
                                Convert.ToDecimal(
                                    txtSalary.Text)
                        };

                    bool success =
                        await _api.CreateStaffAsync(dto);

                    if (success)
                    {
                        MessageBox.Show(
                            "Staff Saved Successfully!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        DialogResult = DialogResult.OK;

                        Close();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Save Failed",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }

                // ================= UPDATE =================

                else
                {
                    UpdateStaffDto dto =
                        new UpdateStaffDto()
                        {
                            StaffId = StaffId,

                            FullName =
                                txtFullName.Text.Trim(),

                            Position =
                                cmbPosition.Text,

                            Department =
                                cmbDepartment.Text,

                            Email =
                                txtEmail.Text.Trim(),

                            Phone =
                                txtPhone.Text.Trim(),

                            Address =
                                txtAddress.Text.Trim(),

                            Salary =
                                Convert.ToDecimal(
                                    txtSalary.Text)
                        };

                    bool success =
                        await _api.UpdateStaffAsync(dto);

                    if (success)
                    {
                        MessageBox.Show(
                            "Staff Updated Successfully!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        DialogResult = DialogResult.OK;

                        Close();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Update Failed",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(
                    "Authentication Failed!",
                    "Unauthorized",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ================= CLEAR BUTTON =================

        private void btnClear_Click(
            object sender,
            EventArgs e)
        {
            txtFullName.Clear();

            cmbPosition.SelectedIndex = -1;

            cmbDepartment.SelectedIndex = -1;

            txtEmail.Clear();

            txtPhone.Clear();

            txtAddress.Clear();

            txtSalary.Clear();

            errorProvider.Clear();

            txtFullName.BackColor = Color.White;
            cmbPosition.BackColor = Color.White;
            cmbDepartment.BackColor = Color.White;
            txtEmail.BackColor = Color.White;
            txtPhone.BackColor = Color.White;
            txtAddress.BackColor = Color.White;
            txtSalary.BackColor = Color.White;
        }

        // ================= REALTIME VALIDATION =================

        private void txtFullName_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtFullName.Text))
            {
                SetError(
                    txtFullName,
                    "Full Name required");
            }
            else if (!Regex.IsMatch(
                txtFullName.Text,
                @"^[a-zA-Z\s]+$"))
            {
                SetError(
                    txtFullName,
                    "Only letters allowed");
            }
            else
            {
                ClearError(txtFullName);
            }
        }

        private void cmbPosition_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (cmbPosition.SelectedIndex == -1)
            {
                SetError(
                    cmbPosition,
                    "Select Position");
            }
            else
            {
                ClearError(cmbPosition);
            }
        }

        private void cmbDepartment_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (cmbDepartment.SelectedIndex == -1)
            {
                SetError(
                    cmbDepartment,
                    "Select Department");
            }
            else
            {
                ClearError(cmbDepartment);
            }
        }

        private void txtEmail_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (!Regex.IsMatch(
                txtEmail.Text,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                SetError(
                    txtEmail,
                    "Invalid Email");
            }
            else
            {
                ClearError(txtEmail);
            }
        }

        private void txtPhone_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (!Regex.IsMatch(
                txtPhone.Text,
                @"^[0-9]{10}$"))
            {
                SetError(
                    txtPhone,
                    "Phone must be 10 digits");
            }
            else
            {
                ClearError(txtPhone);
            }
        }

        private void txtAddress_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtAddress.Text))
            {
                SetError(
                    txtAddress,
                    "Address required");
            }
            else
            {
                ClearError(txtAddress);
            }
        }

        private void txtSalary_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (!decimal.TryParse(
                txtSalary.Text,
                out decimal salary))
            {
                SetError(
                    txtSalary,
                    "Invalid Salary");
            }
            else
            {
                ClearError(txtSalary);
            }
        }
    }
}