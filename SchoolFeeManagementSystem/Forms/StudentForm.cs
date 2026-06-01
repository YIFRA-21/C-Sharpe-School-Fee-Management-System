using static SchoolFeeManagemetSystem.API.DTOs.StudentDTOs;

namespace SchoolFeeManagementSystem.Forms
{
    public partial class StudentForm : Form
    {
        private readonly ApiClient _api = new ApiClient();
        private StudentDTO _student;

        public StudentForm(StudentDTO student = null)
        {
            InitializeComponent();
            _student = student;
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            LoadDropdowns();

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
        }

        private void LoadDropdowns()
        {
            cmbClass.Items.AddRange(new string[]
            {
                "Grade 1","Grade 2","Grade 3","Grade 4","Grade 5",
                "Grade 6","Grade 7","Grade 8","Grade 9","Grade 10"
            });

            cmbSection.Items.AddRange(new string[] { "A", "B", "C" ,"D", "E", "F" ,"G", "H", "I", "J"});

         
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_student == null)
                {
                    var dto = new CreateStudentDTO
                    {
                        AdmissionNo = txtAdmissionNo.Text,
                        RollNo = txtRollNo.Text,
                        FullName = txtFullName.Text,
                        Class = cmbClass.Text,
                        Section = cmbSection.Text,
                        DateOfBirth = dtpDOB.Value,
                        Gender = cmbGender.Text,
                        PhoneNumber = txtPhone.Text,
                        Email = txtEmail.Text,
                        Address = txtAddress.Text
                    };

                    await _api.PostAsync<StudentDTO>("Student", dto);
                }
                else
                {
                    var dto = new UpdateStudentDTO
                    {
                        Id = _student.Id,
                        AdmissionNo = txtAdmissionNo.Text,
                        RollNo = txtRollNo.Text,
                        FullName = txtFullName.Text,
                        Class = cmbClass.Text,
                        Section = cmbSection.Text,
                        DateOfBirth = dtpDOB.Value,
                        Gender = cmbGender.Text,
                        PhoneNumber = txtPhone.Text,
                        Email = txtEmail.Text,
                        Address = txtAddress.Text
                    };

                    await _api.PutAsync("Student", dto);
                }

                MessageBox.Show("Saved successfully!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}