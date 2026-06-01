using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using static SchoolFeeManagemetSystem.API.DTOs.FeeStructureDTOs;

namespace SchoolFeeManagementSystem.Forms
{
    public partial class AssigFeeToClass : Form
    {
        private ApiClient api = new ApiClient();
        private int selectedId = 0;

        // ================= ERROR PROVIDER =================
        ErrorProvider errorProvider = new ErrorProvider();

        public AssigFeeToClass()
        {
            InitializeComponent();

            dgvFee.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFee.MultiSelect = false;
            dgvFee.ReadOnly = true;

            dgvFee.CellClick += dgvFee_CellClick;

            // ================= VALIDATION EVENTS =================
            cmbClass.Validating += cmbClass_Validating;
            txtFeeCategoryId.Validating += txtFeeCategoryId_Validating;
            txtAmount.Validating += txtAmount_Validating;
            cmbFrequency.Validating += cmbFrequency_Validating;
            txtLateFee.Validating += txtLateFee_Validating;
            txtGraceDays.Validating += txtGraceDays_Validating;
            txtDescription.Validating += txtDescription_Validating;
        }

        private async void AssigFeeToClass_Load(object sender, EventArgs e)
        {
            // ================= LOAD COMBOBOX =================
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

            cmbFrequency.Items.AddRange(new string[]
            {
               "Monthly",
                "Quarterly",
                "Semester",
                "Yearly"
            });

            await LoadData();
        }

        // ================= LOAD DATA =================
        private async Task LoadData()
        {
            try
            {
                var data = await api.GetFeeStructuresAsync();

                dgvFee.DataSource = null;
                dgvFee.AutoGenerateColumns = true;
                dgvFee.DataSource = data;

                dgvFee.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Authentication/API Error : " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ================= VALIDATION METHOD =================
        private bool ValidateForm()
        {
            bool isValid = true;

            // CLASS
            if (cmbClass.SelectedIndex == -1)
            {
                errorProvider.SetError(cmbClass, "Select Class");
                cmbClass.BackColor = Color.MistyRose;
                isValid = false;
            }
            else
            {
                errorProvider.SetError(cmbClass, "");
                cmbClass.BackColor = Color.White;
            }

            // FEE CATEGORY ID
            if (string.IsNullOrWhiteSpace(txtFeeCategoryId.Text))
            {
                errorProvider.SetError(
                    txtFeeCategoryId,
                    "Fee Category ID is required");

                txtFeeCategoryId.BackColor = Color.MistyRose;

                isValid = false;
            }
            else if (!int.TryParse(txtFeeCategoryId.Text, out _))
            {
                errorProvider.SetError(
                    txtFeeCategoryId,
                    "Only numeric value allowed");

                txtFeeCategoryId.BackColor = Color.MistyRose;

                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtFeeCategoryId, "");
                txtFeeCategoryId.BackColor = Color.White;
            }

            // AMOUNT
            if (string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                errorProvider.SetError(
                    txtAmount,
                    "Amount is required");

                txtAmount.BackColor = Color.MistyRose;

                isValid = false;
            }
            else if (!decimal.TryParse(txtAmount.Text, out _))
            {
                errorProvider.SetError(
                    txtAmount,
                    "Invalid Amount");

                txtAmount.BackColor = Color.MistyRose;

                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtAmount, "");
                txtAmount.BackColor = Color.White;
            }

            // FREQUENCY
            if (cmbFrequency.SelectedIndex == -1)
            {
                errorProvider.SetError(
                    cmbFrequency,
                    "Select Frequency");

                cmbFrequency.BackColor = Color.MistyRose;

                isValid = false;
            }
            else
            {
                errorProvider.SetError(cmbFrequency, "");
                cmbFrequency.BackColor = Color.White;
            }

            // LATE FEE
            if (string.IsNullOrWhiteSpace(txtLateFee.Text))
            {
                errorProvider.SetError(
                    txtLateFee,
                    "Late Fee is required");

                txtLateFee.BackColor = Color.MistyRose;

                isValid = false;
            }
            else if (!decimal.TryParse(txtLateFee.Text, out _))
            {
                errorProvider.SetError(
                    txtLateFee,
                    "Invalid Late Fee");

                txtLateFee.BackColor = Color.MistyRose;

                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtLateFee, "");
                txtLateFee.BackColor = Color.White;
            }

            // GRACE DAYS
            if (string.IsNullOrWhiteSpace(txtGraceDays.Text))
            {
                errorProvider.SetError(
                    txtGraceDays,
                    "Grace Days required");

                txtGraceDays.BackColor = Color.MistyRose;

                isValid = false;
            }
            else if (!int.TryParse(txtGraceDays.Text, out _))
            {
                errorProvider.SetError(
                    txtGraceDays,
                    "Only numeric value allowed");

                txtGraceDays.BackColor = Color.MistyRose;

                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtGraceDays, "");
                txtGraceDays.BackColor = Color.White;
            }

            // DESCRIPTION
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                errorProvider.SetError(
                    txtDescription,
                    "Description is required");

                txtDescription.BackColor = Color.MistyRose;

                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtDescription, "");
                txtDescription.BackColor = Color.White;
            }

            return isValid;
        }

        // ================= SAVE =================
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                MessageBox.Show(
                    "Please fill all fields correctly",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

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
                    FeeCategoryId =
                        Convert.ToInt32(txtFeeCategoryId.Text)
                };

                await api.CreateFeeStructureAsync(dto);

                MessageBox.Show(
                    "Saved Successfully",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadData();

                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Authentication/API Error : " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ================= EDIT =================
        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedId == 0)
            {
                MessageBox.Show(
                    "Select a row first",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!ValidateForm())
            {
                MessageBox.Show(
                    "Please fill all fields correctly",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
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
                    FeeCategoryId =
                        Convert.ToInt32(txtFeeCategoryId.Text)
                };

                await api.UpdateFeeStructureAsync(dto);

                MessageBox.Show(
                    "Updated Successfully",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadData();

                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Authentication/API Error : " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ================= DELETE =================
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == 0)
            {
                MessageBox.Show(
                    "Select a row first",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure to delete?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    await api.DeleteFeeStructureAsync(selectedId);

                    MessageBox.Show(
                        "Deleted Successfully",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadData();

                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Authentication/API Error : " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        // ================= CELL CLICK =================
        private void dgvFee_CellClick(object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvFee.Rows[e.RowIndex];

            if (row.Cells["Id"].Value == null)
            {
                MessageBox.Show("Column not found!");
                return;
            }

            selectedId =
                Convert.ToInt32(row.Cells["Id"].Value);

            cmbClass.Text =
                row.Cells["Class"].Value?.ToString();

            txtFeeCategoryId.Text =
                row.Cells["FeeCategoryId"].Value?.ToString();

            txtAmount.Text =
                row.Cells["Amount"].Value?.ToString();

            cmbFrequency.Text =
                row.Cells["Frequency"].Value?.ToString();

            int dueDay =
                Convert.ToInt32(row.Cells["DueDay"].Value);

            dtpDueDate.Value =
                new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    dueDay);

            txtLateFee.Text =
                row.Cells["LateFee"].Value?.ToString();

            txtGraceDays.Text =
                row.Cells["GraceDays"].Value?.ToString();

            txtDescription.Text =
                row.Cells["Description"].Value?.ToString();
        }

        // ================= CLEAR =================
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

            // RESET COLORS
            txtFeeCategoryId.BackColor = Color.White;
            txtAmount.BackColor = Color.White;
            txtLateFee.BackColor = Color.White;
            txtGraceDays.BackColor = Color.White;
            txtDescription.BackColor = Color.White;

            cmbClass.BackColor = Color.White;
            cmbFrequency.BackColor = Color.White;

            errorProvider.Clear();
        }

        // ================= REFRESH =================
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadData();

            Clear();
        }

        // ================= VALIDATION EVENTS =================

        private void cmbClass_Validating(object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (cmbClass.SelectedIndex == -1)
            {
                errorProvider.SetError(
                    cmbClass,
                    "Select Class");

                cmbClass.BackColor = Color.MistyRose;
            }
            else
            {
                errorProvider.SetError(cmbClass, "");
                cmbClass.BackColor = Color.White;
            }
        }

        private void txtFeeCategoryId_Validating(object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (!int.TryParse(txtFeeCategoryId.Text, out _))
            {
                errorProvider.SetError(
                    txtFeeCategoryId,
                    "Invalid Fee Category ID");

                txtFeeCategoryId.BackColor = Color.MistyRose;
            }
            else
            {
                errorProvider.SetError(txtFeeCategoryId, "");
                txtFeeCategoryId.BackColor = Color.White;
            }
        }

        private void txtAmount_Validating(object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out _))
            {
                errorProvider.SetError(
                    txtAmount,
                    "Invalid Amount");

                txtAmount.BackColor = Color.MistyRose;
            }
            else
            {
                errorProvider.SetError(txtAmount, "");
                txtAmount.BackColor = Color.White;
            }
        }

        private void cmbFrequency_Validating(object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (cmbFrequency.SelectedIndex == -1)
            {
                errorProvider.SetError(
                    cmbFrequency,
                    "Select Frequency");

                cmbFrequency.BackColor = Color.MistyRose;
            }
            else
            {
                errorProvider.SetError(cmbFrequency, "");
                cmbFrequency.BackColor = Color.White;
            }
        }

        private void txtLateFee_Validating(object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (!decimal.TryParse(txtLateFee.Text, out _))
            {
                errorProvider.SetError(
                    txtLateFee,
                    "Invalid Late Fee");

                txtLateFee.BackColor = Color.MistyRose;
            }
            else
            {
                errorProvider.SetError(txtLateFee, "");
                txtLateFee.BackColor = Color.White;
            }
        }

        private void txtGraceDays_Validating(object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (!int.TryParse(txtGraceDays.Text, out _))
            {
                errorProvider.SetError(
                    txtGraceDays,
                    "Invalid Grace Days");

                txtGraceDays.BackColor = Color.MistyRose;
            }
            else
            {
                errorProvider.SetError(txtGraceDays, "");
                txtGraceDays.BackColor = Color.White;
            }
        }

        private void txtDescription_Validating(object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                errorProvider.SetError(
                    txtDescription,
                    "Enter Description");

                txtDescription.BackColor = Color.MistyRose;
            }
            else
            {
                errorProvider.SetError(txtDescription, "");
                txtDescription.BackColor = Color.White;
            }
        }
    }
}