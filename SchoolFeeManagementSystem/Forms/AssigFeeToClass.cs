using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SchoolFeeManagemetSystem.API.DTOs.FeeStructureDTOs;

namespace SchoolFeeManagementSystem.Forms
{
    public partial class AssigFeeToClass : Form
    {
        private ApiClient api = new ApiClient();
        private int selectedId = 0;

        public AssigFeeToClass()
        {
            InitializeComponent();

            dgvFee.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFee.MultiSelect = false;
            dgvFee.ReadOnly = true;
        }

        private async void AssigFeeToClass_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            var data = await api.GetFeeStructuresAsync();

            dgvFee.DataSource = null;
            dgvFee.AutoGenerateColumns = true;
            dgvFee.DataSource = data;
        }

        // SAVE
        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var dto = new CreateFeeStructureDTO
                {
                    Class = cmbClass.Text,
                    Amount = Convert.ToDecimal(txtAmount.Text),
                    Frequency = cmbFrequency.Text,
                    DueDay = dtpDueDate.Value.Day,
                    LateFee = Convert.ToDecimal(txtLateFee.Text),
                    GraceDays = Convert.ToInt32(txtGraceDays.Text),
                    Description = txtDescription.Text,
                    FeeCategoryId = Convert.ToInt32(txtFeeCategoryId.Text)
                };

                await api.CreateFeeStructureAsync(dto);

                MessageBox.Show("Saved Successfully");
                await LoadData();
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // EDIT
        private async void btnEdit_Click(object sender, EventArgs e)
        {

            var dto = new UpdateFeeStructureDTO
            {
                Id = selectedId,
                Class = cmbClass.Text,
                Amount = Convert.ToDecimal(txtAmount.Text),
                Frequency = cmbFrequency.Text,
                DueDay = dtpDueDate.Value.Day,
                LateFee = Convert.ToDecimal(txtLateFee.Text),
                GraceDays = Convert.ToInt32(txtGraceDays.Text),
                Description = txtDescription.Text,
                FeeCategoryId = Convert.ToInt32(txtFeeCategoryId.Text)
            };

            await api.UpdateFeeStructureAsync(dto);
            MessageBox.Show("Updated");
            await LoadData();
        }

        // DELETE
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            await api.DeleteFeeStructureAsync(selectedId);

            MessageBox.Show("Deleted");
            await LoadData();
        }

     
        private void dgvFee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvFee.Rows[e.RowIndex];
            if (row.Cells["Id"].Value == null)
            {
                MessageBox.Show("Column not found!");
                return;
            }

            selectedId = Convert.ToInt32(row.Cells["Id"].Value);

            cmbClass.Text = row.Cells["Class"].Value?.ToString();
            txtFeeCategoryId.Text = row.Cells["FeeCategoryId"].Value?.ToString();
            txtAmount.Text = row.Cells["Amount"].Value?.ToString();
            cmbFrequency.Text = row.Cells["Frequency"].Value?.ToString();

            int dueDay = Convert.ToInt32(row.Cells["DueDay"].Value);
            dtpDueDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dueDay);

            txtLateFee.Text = row.Cells["LateFee"].Value?.ToString();
            txtGraceDays.Text = row.Cells["GraceDays"].Value?.ToString();
            txtDescription.Text = row.Cells["Description"].Value?.ToString();
        }

        private void Clear()
        {
            selectedId = 0;

            txtFeeCategoryId.Clear();
            txtAmount.Clear();
            txtLateFee.Clear();
            txtGraceDays.Clear();
            txtDescription.Clear();

            cmbClass.SelectedIndex = -1;
            cmbFrequency.SelectedIndex = -1;
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadData();
        }
    }
}