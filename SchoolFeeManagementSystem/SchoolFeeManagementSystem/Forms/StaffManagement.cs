using System.Data;
using static SchoolFeeManagemetSystem.API.DTOs.StaffDTOs;

namespace SchoolFeeManagementSystem.Forms
{
    public partial class StaffManagement : Form
    {
        private readonly ApiClient _api = new ApiClient();
        private List<StaffDtos> allStaffs = new List<StaffDtos>();
        private List<StaffDtos> filteredStaffs = new List<StaffDtos>();

        private int currentPage = 1;
        private int pageSize = 5;
        public StaffManagement()
        {
            InitializeComponent();
        }
        // LOAD FORM
      
        private async void StaffManagement_Load(object sender, EventArgs e)
        {
            await LoadStaffs();
        }
        private async Task LoadStaffs()
        {
            try
            {
                allStaffs = await _api.GetAllStaffAsync();

                filteredStaffs = allStaffs;

                currentPage = 1;

                UpdateTotalCount();

                DisplayPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DisplayPage()
        {
            var pagedData = filteredStaffs
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            dgvStaff.DataSource = null;
            dgvStaff.DataSource = pagedData;

            btnBack.Enabled = currentPage > 1;

            btnNext.Enabled =
                (currentPage * pageSize) < filteredStaffs.Count;
        }
        private void UpdateTotalCount()
        {
            lblTotal.Text =$"Total Staffs : {filteredStaffs.Count}";
        }

        private async void btnAddNew_Click(object sender, EventArgs e)
        {
            StaffForm form = new StaffForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                await LoadStaffs();
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvStaff.CurrentRow == null)
                return;

            var staff =(StaffDtos)dgvStaff.CurrentRow.DataBoundItem;

            StaffForm form = new StaffForm(staff);

            if (form.ShowDialog() == DialogResult.OK)
            {
                await LoadStaffs();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {

            if (dgvStaff.CurrentRow == null)
                return;

            var staff = (StaffDtos)dgvStaff.CurrentRow.DataBoundItem;

            var confirm = MessageBox.Show( "Delete this staff?",  "Confirm",  MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                bool success =
                    await _api.DeleteStaffAsync(staff.StaffId);

                if (success)
                {
                    MessageBox.Show("Deleted Successfully");

                    await LoadStaffs();
                }
                else
                {
                    MessageBox.Show("Delete Failed");
                }
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            await LoadStaffs();
        }

        private async void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                string search = txtSearch.Text
                    .Trim()
                    .ToLower();

                var list = await _api.GetAllStaffAsync();

                var filtered = list.Where(x =>

                    x.StaffId.ToString().Contains(search)

                    || x.FullName.ToLower().Contains(search)

                    || x.Position.ToLower().Contains(search)

                    || x.Department.ToLower().Contains(search)

                ).ToList();

                dgvStaff.DataSource = null;

                dgvStaff.DataSource = filtered;

                lblTotal.Text =
                    "Total Staffs : " + filtered.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnBack_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;

                DisplayPage();
            }
        }

        private async void btnNexts_Click(object sender, EventArgs e)
        {
            if ((currentPage * pageSize) < filteredStaffs.Count)
            {
                currentPage++;

                DisplayPage();
            }
        }

       
    }
}





   