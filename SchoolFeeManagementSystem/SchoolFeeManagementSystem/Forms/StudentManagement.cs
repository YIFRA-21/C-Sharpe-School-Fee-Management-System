using System.Data;
using static SchoolFeeManagemetSystem.API.DTOs.StudentDTOs;

namespace SchoolFeeManagementSystem.Forms
{
    public partial class StudentManagement : Form
    {
        private readonly ApiClient _api = new ApiClient();

        private List<StudentDTO> allStudents = new List<StudentDTO>();
        private List<StudentDTO> filteredStudents = new List<StudentDTO>();

        private int currentPage = 1;
        private int pageSize = 5;
        public StudentManagement()
        {
            InitializeComponent();
        }

        private async void StudentForm_Load(object sender, EventArgs e)
        {
            await LoadStudents();
        }
        private async Task LoadStudents()
        {
            // Use GetAllAsync<T> which exists on ApiClient to fetch a list of students
            allStudents = await _api.GetAllAsync<StudentDTO>("Student");

            filteredStudents = allStudents;
            currentPage = 1;

            UpdateTotalCount();
            DisplayPage();
        }
        private void DisplayPage()
        {
            var pagedData = filteredStudents
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            dgvStudents.DataSource = null;
            dgvStudents.DataSource = pagedData;

            btnPrevious.Enabled = currentPage > 1;
            btnNext.Enabled = (currentPage * pageSize) < filteredStudents.Count;
        }
        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        // ================= DELETE =================
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow == null) return;

            var student = (StudentDTO)dgvStudents.CurrentRow.DataBoundItem;

            var confirm = MessageBox.Show("Delete this student?", "Confirm", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                await _api.DeleteAsync("Student", student.Id);
                await LoadStudents();
            }
        }
        // ================= UPDATE =================
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow == null) return;

            var student = (StudentDTO)dgvStudents.CurrentRow.DataBoundItem;

            var form = new StudentForm(student);

            if (form.ShowDialog() == DialogResult.OK)
            {
                await LoadStudents();
            }
        }
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadStudents();
        }
        // ================= ADD =================
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new StudentForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                await LoadStudents();
            }
        }

        private void lblTotalStudents_Click(object sender, EventArgs e)
        {
            lblTotalStudents.Text = $"Total Students: {filteredStudents.Count}";
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            currentPage--;
            DisplayPage();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currentPage++;
            DisplayPage();
        }

        private async void btnFilter_Click(object sender, EventArgs e)
        {
            var keyword = txtSearch.Text?.ToLower() ?? string.Empty;

            filteredStudents = allStudents
                .Where(s =>
                    (s.FullName ?? string.Empty).ToLower().Contains(keyword) ||
                    (s.AdmissionNo ?? string.Empty).ToLower().Contains(keyword) ||
                    (s.RollNo ?? string.Empty).ToLower().Contains(keyword))
                .ToList();

            currentPage = 1;

            UpdateTotalCount();
            DisplayPage();
        }

        private void UpdateTotalCount()
        {
            lblTotalStudents.Text = $"Total Students: {filteredStudents.Count}";
        }
    }
}


