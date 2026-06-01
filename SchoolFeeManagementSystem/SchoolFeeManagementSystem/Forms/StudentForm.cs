using System.Drawing;
using System.Text.RegularExpressions;
using static SchoolFeeManagemetSystem.API.DTOs.StudentDTOs;

namespace SchoolFeeManagementSystem.Forms
{
    public partial class StudentForm : Form
    {
        // ================= API =================

        private readonly ApiClient _api = new ApiClient();

        // ================= STUDENT =================

        private StudentDTO _student;

        // ================= ERROR PROVIDER =================

        private ErrorProvider errorProvider = new ErrorProvider();

        // ================= CONSTRUCTOR =================

        public StudentForm(StudentDTO student = null)
        {
            InitializeComponent();

            _student = student;

            // Error Provider
            errorProvider.BlinkStyle =
                ErrorBlinkStyle.NeverBlink;
        }

        // ================= FORM LOAD =================

        private void StudentForm_Load(
            object sender,
            EventArgs e)
        {
            LoadDropdowns();

            // Gender Combo
            cmbGender.Items.AddRange(new string[]
            {
                "Male",
                "Female"
            });

            // Load Update Data
            if (_student != null)
            {
                txtAdmissionNo.Text = _student.AdmissionNo;
                txtRollNo.Text = _student.RollNo;
                txtFullName.Text = _student.FullName;
                cmbClass.Text = _student.Class;
                cmbSection.Text = _student.Section;
                dtpDOB.Value = _student.DateOfBirth;
                cmbGender.Text = _student.Gender;
                txtPhone.Text = _student.PhoneNumber;
                txtEmail.Text = _student.Email;
                txtAddress.Text = _student.Address;
            }

            // Validation Events
            txtAdmissionNo.Validating +=
                txtAdmissionNo_Validating;

            txtRollNo.Validating +=
                txtRollNo_Validating;

            txtFullName.Validating +=
                txtFullName_Validating;

            cmbClass.Validating +=
                cmbClass_Validating;

            cmbSection.Validating +=
                cmbSection_Validating;

            cmbGender.Validating +=
                cmbGender_Validating;

            txtPhone.Validating +=
                txtPhone_Validating;

            txtEmail.Validating +=
                txtEmail_Validating;

            txtAddress.Validating +=
                txtAddress_Validating;
        }

        // ================= LOAD DROPDOWNS =================

        private void LoadDropdowns()
        {
            cmbClass.Items.AddRange(new string[]
            {
                "Grade 1",
                "Grade 2",
                "Grade 3",
                "Grade 4",
                "Grade 5",
                "Grade 6",
                "Grade 7",
                "Grade 8",
                "Grade 9",
                "Grade 10",
                "Grade 11",
                "Grade 12"
            });

            cmbSection.Items.AddRange(new string[]
            {
                "A","B","C","D","E",
                "F","G","H","I","J"
            });
        }

        // ================= ERROR METHODS =================

        private void SetError(
            Control control,
            string message)
        {
            errorProvider.SetError(control, message);

            control.BackColor = Color.MistyRose;
        }

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

            // Admission No
            if (string.IsNullOrWhiteSpace(
                txtAdmissionNo.Text))
            {
                SetError(
                    txtAdmissionNo,
                    "Admission Number required");

                isValid = false;
            }

            // Roll No
            if (string.IsNullOrWhiteSpace(
                txtRollNo.Text))
            {
                SetError(
                    txtRollNo,
                    "Roll Number required");

                isValid = false;
            }

            // Full Name
            if (string.IsNullOrWhiteSpace(
                txtFullName.Text))
            {
                SetError(
                    txtFullName,
                    "Full Name required");

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

            // Class
            if (cmbClass.SelectedIndex == -1)
            {
                SetError(
                    cmbClass,
                    "Select Class");

                isValid = false;
            }

            // Section
            if (cmbSection.SelectedIndex == -1)
            {
                SetError(
                    cmbSection,
                    "Select Section");

                isValid = false;
            }

            // Gender
            if (cmbGender.SelectedIndex == -1)
            {
                SetError(
                    cmbGender,
                    "Select Gender");

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

            // Email
            if (!Regex.IsMatch(
                txtEmail.Text,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                SetError(
                    txtEmail,
                    "Invalid Email");

                isValid = false;
            }

            // Address
            if (string.IsNullOrWhiteSpace(
                txtAddress.Text))
            {
                SetError(
                    txtAddress,
                    "Address required");

                isValid = false;
            }

            return isValid;
        }

        // ================= SAVE =================

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate Form
                if (!ValidateForm())
                {
                    MessageBox.Show(
                        "Please correct all errors.",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // CREATE
                if (_student == null)
                {
                    var dto = new CreateStudentDTO
                    {
                        AdmissionNo =  txtAdmissionNo.Text.Trim(),

                        RollNo = txtRollNo.Text.Trim(),

                        FullName =  txtFullName.Text.Trim(),

                        Class =  cmbClass.Text,

                        Section =
                            cmbSection.Text,

                        DateOfBirth =
                            dtpDOB.Value,

                        Gender =
                            cmbGender.Text,

                        PhoneNumber =
                            txtPhone.Text.Trim(),

                        Email =
                            txtEmail.Text.Trim(),

                        Address =
                            txtAddress.Text.Trim()
                    };

                    await _api.PostAsync<StudentDTO>(
                        "Student",
                        dto);

                    MessageBox.Show(
                        "Student Added Successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                // UPDATE
                else
                {
                    var dto = new UpdateStudentDTO
                    {
                        Id = _student.Id,

                        AdmissionNo =
                            txtAdmissionNo.Text.Trim(),

                        RollNo =
                            txtRollNo.Text.Trim(),

                        FullName =
                            txtFullName.Text.Trim(),

                        Class =
                            cmbClass.Text,

                        Section =
                            cmbSection.Text,

                        DateOfBirth =
                            dtpDOB.Value,

                        Gender =
                            cmbGender.Text,

                        PhoneNumber =
                            txtPhone.Text.Trim(),

                        Email =
                            txtEmail.Text.Trim(),

                        Address =
                            txtAddress.Text.Trim()
                    };

                    await _api.PutAsync(
                        $"Student/{_student.Id}",
                        dto);

                    MessageBox.Show(
                        "Student Updated Successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;

                this.Close();
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

        // ================= REALTIME VALIDATION =================

        private void txtAdmissionNo_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtAdmissionNo.Text))
            {
                SetError(
                    txtAdmissionNo,
                    "Admission Number required");
            }
            else
            {
                ClearError(txtAdmissionNo);
            }
        }

        private void txtRollNo_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtRollNo.Text))
            {
                SetError(
                    txtRollNo,
                    "Roll Number required");
            }
            else
            {
                ClearError(txtRollNo);
            }
        }

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

        private void cmbClass_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (cmbClass.SelectedIndex == -1)
            {
                SetError(
                    cmbClass,
                    "Select Class");
            }
            else
            {
                ClearError(cmbClass);
            }
        }

        private void cmbSection_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (cmbSection.SelectedIndex == -1)
            {
                SetError(
                    cmbSection,
                    "Select Section");
            }
            else
            {
                ClearError(cmbSection);
            }
        }

        private void cmbGender_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (cmbGender.SelectedIndex == -1)
            {
                SetError(
                    cmbGender,
                    "Select Gender");
            }
            else
            {
                ClearError(cmbGender);
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
                    "Invalid Email Address");
            }
            else
            {
                ClearError(txtEmail);
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
    }
}