using static SchoolFeeManagemetSystem.API.DTOs.FeeCategoryDTOs;
using System.ComponentModel;

namespace SchoolFeeManagementSystem.Forms
{
    public partial class Fees : Form
    {
        private ApiClient api = new ApiClient();

        private int selectedId = 0;

        private Form currentChildForm;

        // ================= ERROR PROVIDER =================
        ErrorProvider errorProvider = new ErrorProvider();

        public Fees()
        {
            InitializeComponent();

            dgvCategory.CellClick += dgvCategory_CellClick;

            // ================= VALIDATION EVENTS =================
            txtCategoryCode.Validating += txtCategoryCode_Validating;

            cmbCategoryName.Validating += cmbCategoryName_Validating;

            cmbFrequency.Validating += cmbFrequency_Validating;

            cmbStatus.Validating += cmbStatus_Validating;

            txtDescription.Validating += txtDescription_Validating;
        }

        // ================= FORM LOAD =================
        private async void FeeCategoryForm_Load(object sender, EventArgs e)
        {
            // ================= STATUS =================
            cmbStatus.Items.Clear();

            cmbStatus.Items.AddRange(new string[]
            {
                "Active",
                "Inactive"
            });

            // ================= FREQUENCY =================
            cmbFrequency.Items.Clear();

            cmbFrequency.Items.AddRange(new string[]
            {
                "Monthly",
                "Quarterly",
                "Semester",
                "Yearly"
            });

            // ================= CATEGORY NAME =================
            cmbCategoryName.Items.Clear();

            cmbCategoryName.Items.AddRange(new string[]
            {
                "Tuition Fee",
                "Registration Fee",
                "Library Fee",
                "Exam Fee",
                "Transport Fee",
                "Laboratory Fee",
                "Sports Fee",
                "Hostel Fee",
                "Uniform Fee",
                "Computer Fee"
            });

            // ================= COMBO STYLE =================
            cmbCategoryName.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cmbFrequency.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cmbStatus.DropDownStyle =
                ComboBoxStyle.DropDownList;

            await LoadData();
        }

        // ================= LOAD DATA =================
        private async Task LoadData()
        {
            try
            {
                var list =
                    await api.GetAllAsync<FeeCategoryDTO>(
                        "FeeCategory");

                dgvCategory.DataSource = null;

                dgvCategory.DataSource = list;

                dgvCategory.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                lblTotal.Text =
                    "Total Categories : " + list.Count;
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

        // ================= GRID CLICK =================
        private void dgvCategory_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dgvCategory.Rows[e.RowIndex];

            selectedId =
                Convert.ToInt32(row.Cells["Id"].Value);

            txtCategoryCode.Text =
                row.Cells["CategoryCode"].Value?.ToString();

            cmbCategoryName.Text =
                row.Cells["CategoryName"].Value?.ToString();

            txtDescription.Text =
                row.Cells["Description"].Value?.ToString();

            cmbFrequency.Text =
                row.Cells["Frequency"].Value?.ToString();

            bool isActive = false;

            if (row.Cells["IsActive"].Value != null)
            {
                isActive =
                    Convert.ToBoolean(
                        row.Cells["IsActive"].Value);
            }

            cmbStatus.Text =
                isActive ? "Active" : "Inactive";
        }

        // ================= VALIDATION =================
        private bool ValidateForm()
        {
            bool isValid = true;

            // CATEGORY CODE
            if (string.IsNullOrWhiteSpace(
                txtCategoryCode.Text))
            {
                errorProvider.SetError(
                    txtCategoryCode,
                    "Enter Category Code");

                txtCategoryCode.BackColor =
                    Color.MistyRose;

                isValid = false;
            }
            else
            {
                errorProvider.SetError(
                    txtCategoryCode,
                    "");

                txtCategoryCode.BackColor =
                    Color.White;
            }

            // CATEGORY NAME
            if (cmbCategoryName.SelectedIndex == -1)
            {
                errorProvider.SetError(
                    cmbCategoryName,
                    "Select Category Name");

                cmbCategoryName.BackColor =
                    Color.MistyRose;

                isValid = false;
            }
            else
            {
                errorProvider.SetError(
                    cmbCategoryName,
                    "");

                cmbCategoryName.BackColor =
                    Color.White;
            }

            // FREQUENCY
            if (cmbFrequency.SelectedIndex == -1)
            {
                errorProvider.SetError(
                    cmbFrequency,
                    "Select Frequency");

                cmbFrequency.BackColor =
                    Color.MistyRose;

                isValid = false;
            }
            else
            {
                errorProvider.SetError(
                    cmbFrequency,
                    "");

                cmbFrequency.BackColor =
                    Color.White;
            }

            // STATUS
            if (cmbStatus.SelectedIndex == -1)
            {
                errorProvider.SetError(
                    cmbStatus,
                    "Select Status");

                cmbStatus.BackColor =
                    Color.MistyRose;

                isValid = false;
            }
            else
            {
                errorProvider.SetError(
                    cmbStatus,
                    "");

                cmbStatus.BackColor =
                    Color.White;
            }

            // DESCRIPTION
            if (string.IsNullOrWhiteSpace(
                txtDescription.Text))
            {
                errorProvider.SetError(
                    txtDescription,
                    "Enter Description");

                txtDescription.BackColor =
                    Color.MistyRose;

                isValid = false;
            }
            else
            {
                errorProvider.SetError(
                    txtDescription,
                    "");

                txtDescription.BackColor =
                    Color.White;
            }

            return isValid;
        }

        // ================= VALIDATING EVENTS =================

        private void txtCategoryCode_Validating(
            object sender,
            CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtCategoryCode.Text))
            {
                errorProvider.SetError(
                    txtCategoryCode,
                    "Enter Category Code");

                txtCategoryCode.BackColor =
                    Color.MistyRose;
            }
            else
            {
                errorProvider.SetError(
                    txtCategoryCode,
                    "");

                txtCategoryCode.BackColor =
                    Color.White;
            }
        }

        private void cmbCategoryName_Validating(
            object sender,
            CancelEventArgs e)
        {
            if (cmbCategoryName.SelectedIndex == -1)
            {
                errorProvider.SetError(
                    cmbCategoryName,
                    "Select Category Name");

                cmbCategoryName.BackColor =
                    Color.MistyRose;
            }
            else
            {
                errorProvider.SetError(
                    cmbCategoryName,
                    "");

                cmbCategoryName.BackColor =
                    Color.White;
            }
        }

        private void cmbFrequency_Validating(
            object sender,
            CancelEventArgs e)
        {
            if (cmbFrequency.SelectedIndex == -1)
            {
                errorProvider.SetError(
                    cmbFrequency,
                    "Select Frequency");

                cmbFrequency.BackColor =
                    Color.MistyRose;
            }
            else
            {
                errorProvider.SetError(
                    cmbFrequency,
                    "");

                cmbFrequency.BackColor =
                    Color.White;
            }
        }

        private void cmbStatus_Validating(
            object sender,
            CancelEventArgs e)
        {
            if (cmbStatus.SelectedIndex == -1)
            {
                errorProvider.SetError(
                    cmbStatus,
                    "Select Status");

                cmbStatus.BackColor =
                    Color.MistyRose;
            }
            else
            {
                errorProvider.SetError(
                    cmbStatus,
                    "");

                cmbStatus.BackColor =
                    Color.White;
            }
        }

        private void txtDescription_Validating(
            object sender,
            CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtDescription.Text))
            {
                errorProvider.SetError(
                    txtDescription,
                    "Enter Description");

                txtDescription.BackColor =
                    Color.MistyRose;
            }
            else
            {
                errorProvider.SetError(
                    txtDescription,
                    "");

                txtDescription.BackColor =
                    Color.White;
            }
        }

        // ================= ADD =================
        private async void btnAdd_Click(
            object sender,
            EventArgs e)
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
                var dto =
                    new CreateFeeCategoryDTO
                    {
                        CategoryCode =
                            txtCategoryCode.Text,

                        CategoryName =
                            cmbCategoryName.Text,

                        Frequency =
                            cmbFrequency.Text,

                        Description =
                            txtDescription.Text,

                        Status =
                            cmbStatus.Text
                    };

                await api.PostAsync<FeeCategoryDTO>(
                    "FeeCategory",
                    dto);

                MessageBox.Show(
                    "Added Successfully",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadData();

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Authentication/API Error : "
                    + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ================= UPDATE =================
        private async void btnUpdate_Click(
            object sender,
            EventArgs e)
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

            if (selectedId == 0)
            {
                MessageBox.Show(
                    "Select a row first",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                var dto =
                    new UpdateFeeCategoryDTO
                    {
                        Id = selectedId,

                        CategoryCode =
                            txtCategoryCode.Text,

                        CategoryName =
                            cmbCategoryName.Text,

                        Frequency =
                            cmbFrequency.Text,

                        Description =
                            txtDescription.Text,

                        Status =
                            cmbStatus.Text
                    };

                await api.PutAsync(
                    $"FeeCategory/{selectedId}",
                    dto);

                MessageBox.Show(
                    "Updated Successfully",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadData();

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Authentication/API Error : "
                    + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ================= DELETE =================
        private async void btnDelete_Click(
            object sender,
            EventArgs e)
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

            DialogResult result =
                MessageBox.Show(
                    "Delete this record ?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    await api.DeleteAsync(
                        "FeeCategory",
                        selectedId);

                    MessageBox.Show(
                        "Deleted Successfully",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadData();

                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Authentication/API Error : "
                        + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        // ================= REFRESH =================
        private async void btnRefresh_Click(
            object sender,
            EventArgs e)
        {
            await LoadData();

            ClearForm();
        }

        // ================= CLEAR =================
        private void ClearForm()
        {
            selectedId = 0;

            txtCategoryCode.Clear();

            cmbCategoryName.SelectedIndex = -1;

            txtDescription.Clear();

            cmbFrequency.SelectedIndex = -1;

            cmbStatus.SelectedIndex = -1;

            txtCategoryCode.BackColor = Color.White;

            cmbCategoryName.BackColor = Color.White;

            txtDescription.BackColor = Color.White;

            cmbFrequency.BackColor = Color.White;

            cmbStatus.BackColor = Color.White;

            errorProvider.Clear();
        }

        // ================= ASSIGN FEE =================
        private void btnAssignFee_Click(
            object sender,
            EventArgs e)
        {
            FeesPanel.Controls.Clear();

            openChildForm(new AssigFeeToClass());
        }

        // ================= FEE CATEGORY PAGE =================
        private async void FeeCategoryForm_Click(
            object sender,
            EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();

                currentChildForm = null;
            }

            FeesPanel.Controls.Clear();

            LoadFeeCategoryControls();

            await LoadData();
        }

        // ================= LOAD CONTROLS =================
        private void LoadFeeCategoryControls()
        {
            FeesPanel.Controls.Add(lblCategoryCode);
            FeesPanel.Controls.Add(txtCategoryCode);

            FeesPanel.Controls.Add(lblCategoryId);
            FeesPanel.Controls.Add(txtCategoryId);

            FeesPanel.Controls.Add(lblCategoryName);
            FeesPanel.Controls.Add(cmbCategoryName);

            FeesPanel.Controls.Add(lblDescription);
            FeesPanel.Controls.Add(txtDescription);

            FeesPanel.Controls.Add(lblFrequency);
            FeesPanel.Controls.Add(cmbFrequency);

            FeesPanel.Controls.Add(lblStatus);
            FeesPanel.Controls.Add(cmbStatus);

            FeesPanel.Controls.Add(dgvCategory);

            FeesPanel.Controls.Add(btnAdd);
            FeesPanel.Controls.Add(btnEdit);
            FeesPanel.Controls.Add(btnDelete);
            FeesPanel.Controls.Add(btnRefresh);

            FeesPanel.Controls.Add(lblTotal);
        }

        // ================= OPEN CHILD FORM =================
        private void openChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            currentChildForm = childForm;

            childForm.TopLevel = false;

            childForm.FormBorderStyle =
                FormBorderStyle.None;

            childForm.Dock = DockStyle.Fill;

            FeesPanel.Controls.Add(childForm);

            FeesPanel.Tag = childForm;

            childForm.BringToFront();

            childForm.Show();
        }
    }
}