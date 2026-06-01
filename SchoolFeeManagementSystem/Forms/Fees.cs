using static SchoolFeeManagemetSystem.API.DTOs.FeeCategoryDTOs;

namespace SchoolFeeManagementSystem.Forms
{
    public partial class Fees : Form
    {
        private ApiClient api = new ApiClient();
        private int selectedId = 0;

        public Fees()
        {
            InitializeComponent();
            dgvCategory.CellClick += dgvCategory_CellClick; // 🔥 IMPORTANT
        }

        private async void FeeCategoryForm_Load(object sender, EventArgs e)
        {
            cmbStatus.Items.AddRange(new string[] { "Active", "Inactive" });
            cmbFrequency.Items.AddRange(new string[] { "Monthly", "Yearly" });

            await LoadData();
        }

        // ================= LOAD =================
        private async Task LoadData()
        {
            var list = await api.GetAllAsync<FeeCategoryDTO>("FeeCategory");

            dgvCategory.DataSource = null;
            dgvCategory.DataSource = list;

            dgvCategory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lblTotal.Text = "Total Categories: " + list.Count;
        }

        // ================= ROW CLICK =================
        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvCategory.Rows[e.RowIndex];

            selectedId = Convert.ToInt32(row.Cells["Id"].Value);

            txtCategoryCode.Text = row.Cells["CategoryCode"].Value?.ToString();
            txtCategoryName.Text = row.Cells["CategoryName"].Value?.ToString();
            txtDescription.Text = row.Cells["Description"].Value?.ToString();
            cmbFrequency.Text = row.Cells["Frequency"].Value?.ToString();

            bool isActive = false;
            if (row.Cells["IsActive"].Value != null)
                isActive = Convert.ToBoolean(row.Cells["IsActive"].Value);

            cmbStatus.Text = isActive ? "Active" : "Inactive";
        }

        // ================= ADD =================
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var dto = new CreateFeeCategoryDTO
            {
                CategoryCode = txtCategoryCode.Text,
                CategoryName = txtCategoryName.Text,
                Frequency = cmbFrequency.Text,
                Description = txtDescription.Text,
                Status = cmbStatus.Text
            };

            await api.PostAsync<FeeCategoryDTO>("FeeCategory", dto);

            MessageBox.Show("Added Successfully");
            await LoadData();
            ClearForm();
        }

        // ================= EDIT =================
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedId == 0)
            {
                MessageBox.Show("Select a row first");
                return;
            }

            var dto = new UpdateFeeCategoryDTO
            {
                Id = selectedId,
                CategoryCode = txtCategoryCode.Text,
                CategoryName = txtCategoryName.Text,
                Frequency = cmbFrequency.Text,
                Description = txtDescription.Text,
                Status = cmbStatus.Text
            };

            await api.PutAsync($"FeeCategory/{selectedId}", dto);

            MessageBox.Show("Updated Successfully");
            await LoadData();
            ClearForm();
        }

        // ================= DELETE =================
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == 0)
            {
                MessageBox.Show("Select a row first");
                return;
            }

            var confirm = MessageBox.Show("Delete this record?", "Confirm", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                await api.DeleteAsync("FeeCategory", selectedId);

                MessageBox.Show("Deleted Successfully");
                await LoadData();
                ClearForm();
            }
        }

        // ================= REFRESH =================
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadData();
            ClearForm();
        }

        // ================= CLEAR =================
        private void ClearForm()
        {
            selectedId = 0;

            txtCategoryCode.Clear();
            txtCategoryName.Clear();
            txtDescription.Clear();

            cmbFrequency.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
        }

        // ================= OPEN OTHER FORM =================
        private void btnAssignFee_Click(object sender, EventArgs e)
        {
            var form = new AssigFeeToClass();
            form.ShowDialog();
        }
    }
}