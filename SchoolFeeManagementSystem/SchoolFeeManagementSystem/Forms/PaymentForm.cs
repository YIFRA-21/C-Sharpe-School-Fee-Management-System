using System;
using System.Drawing;
using System.Windows.Forms;
using static SchoolFeeManagemetSystem.API.DTOs.PaymentDTOs;

namespace SchoolFeeManagementSystem.Forms
{
    public partial class PaymentForm : Form
    {
       

        private readonly ApiClient _api = new ApiClient();

        private PaymentDTO _payment;
        private ErrorProvider errorProvider = new ErrorProvider();

        public PaymentForm()
        {
            InitializeComponent();

            InitializeForm();
        }

        public PaymentForm(PaymentDTO selected)
        {
            InitializeComponent();

            _payment = selected;

            InitializeForm();

            LoadPaymentData();
        }

        private void InitializeForm()
        {
            errorProvider.BlinkStyle =
                ErrorBlinkStyle.NeverBlink;

            LoadComboBoxes();

            AddValidationEvents();
        }

        private void LoadComboBoxes()
        {
            // CLASS

            cmbClass.Items.Clear();

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

            // PAYMENT METHOD

            cmbPaymentMethod.Items.Clear();

            cmbPaymentMethod.Items.AddRange(new string[]
            {
                "Cash",
                "Bank",
                "Mobile Banking",
                "TeleBirr"
            });

            cmbClass.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cmbPaymentMethod.DropDownStyle =
                ComboBoxStyle.DropDownList;
        }

        // ================= LOAD UPDATE DATA =================

        private void LoadPaymentData()
        {
            txtReceiptNo.Text =
                _payment.ReceiptNo;

            txtStudentId.Text =
                _payment.StudentId.ToString();

            txtFeeStructureId.Text =
                _payment.FeeStructureId.ToString();

            cmbClass.Text =
                _payment.Class;

            txtTotalFee.Text =
                _payment.TotalFee.ToString();

            txtPaidAmount.Text =
                _payment.PaidAmount.ToString();

            dtpPaymentDate.Value =
                _payment.PaymentDate;

            cmbPaymentMethod.Text =
                _payment.PaymentMethod;

            txtReferenceNo.Text =
                _payment.ReferenceNo;

            txtRemarks.Text =
                _payment.Remarks;
        }

        // ================= VALIDATION EVENTS =================

        private void AddValidationEvents()
        {
            txtReceiptNo.Validating +=
                txtReceiptNo_Validating;

            txtStudentId.Validating +=
                txtStudentId_Validating;

            txtFeeStructureId.Validating +=
                txtFeeStructureId_Validating;

            cmbClass.Validating +=
                cmbClass_Validating;

            txtTotalFee.Validating +=
                txtTotalFee_Validating;

            txtPaidAmount.Validating +=
                txtPaidAmount_Validating;

            cmbPaymentMethod.Validating +=
                cmbPaymentMethod_Validating;

            txtReferenceNo.Validating +=
                txtReferenceNo_Validating;

            txtRemarks.Validating +=
                txtRemarks_Validating;
        }

        // ================= SET ERROR =================

        private void SetError(
            Control control,
            string message)
        {
            errorProvider.SetError(
                control,
                message);

            control.BackColor =
                Color.MistyRose;
        }

        // ================= CLEAR ERROR =================

        private void ClearError(Control control)
        {
            errorProvider.SetError(
                control,
                "");

            control.BackColor =
                Color.White;
        }

        // ================= VALIDATE FORM =================

        private bool ValidateForm()
        {
            bool isValid = true;

            errorProvider.Clear();

            // RECEIPT NUMBER

            if (string.IsNullOrWhiteSpace(
                txtReceiptNo.Text))
            {
                SetError(
                    txtReceiptNo,
                    "Receipt Number is required");

                isValid = false;
            }

            // STUDENT ID

            if (!int.TryParse(
                txtStudentId.Text,
                out int studentId))
            {
                SetError(
                    txtStudentId,
                    "Invalid Student ID");

                isValid = false;
            }

            // FEE STRUCTURE ID

            if (!int.TryParse(
                txtFeeStructureId.Text,
                out int feeId))
            {
                SetError(
                    txtFeeStructureId,
                    "Invalid Fee Structure ID");

                isValid = false;
            }

            // CLASS

            if (cmbClass.SelectedIndex == -1)
            {
                SetError(
                    cmbClass,
                    "Select Class");

                isValid = false;
            }

            // TOTAL FEE

            if (!decimal.TryParse(
                txtTotalFee.Text,
                out decimal totalFee))
            {
                SetError(
                    txtTotalFee,
                    "Invalid Total Fee");

                isValid = false;
            }

            // PAID AMOUNT

            if (!decimal.TryParse(
                txtPaidAmount.Text,
                out decimal paidAmount))
            {
                SetError(
                    txtPaidAmount,
                    "Invalid Paid Amount");

                isValid = false;
            }

            // PAID > TOTAL

            if (paidAmount > totalFee)
            {
                SetError(
                    txtPaidAmount,
                    "Paid Amount cannot exceed Total Fee");

                isValid = false;
            }

            // PAYMENT METHOD

            if (cmbPaymentMethod.SelectedIndex == -1)
            {
                SetError(
                    cmbPaymentMethod,
                    "Select Payment Method");

                isValid = false;
            }

            // REFERENCE NO

            if (string.IsNullOrWhiteSpace(
                txtReferenceNo.Text))
            {
                SetError(
                    txtReferenceNo,
                    "Reference Number required");

                isValid = false;
            }

            // REMARKS

            if (string.IsNullOrWhiteSpace(
                txtRemarks.Text))
            {
                SetError(
                    txtRemarks,
                    "Remarks required");

                isValid = false;
            }

            return isValid;
        }

        // ================= SAVE =================

        private async void btnSave_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                // VALIDATE

                if (!ValidateForm())
                {
                    MessageBox.Show(
                        "Please correct all errors.",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                decimal totalFee =
                    Convert.ToDecimal(
                        txtTotalFee.Text);

                decimal paidAmount =
                    Convert.ToDecimal(
                        txtPaidAmount.Text);

                decimal balance =
                    totalFee - paidAmount;

                // ================= ADD =================

                if (_payment == null)
                {
                    var dto =
                        new CreatePaymentDTO
                        {
                            ReceiptNo =
                                txtReceiptNo.Text.Trim(),

                            StudentId =
                                Convert.ToInt32(
                                    txtStudentId.Text),

                            FeeStructureId =
                                Convert.ToInt32(
                                    txtFeeStructureId.Text),

                            Class =
                                cmbClass.Text,

                            TotalFee =
                                totalFee,

                            PaidAmount =
                                paidAmount,

                            Balance =
                                balance,

                            PaymentDate =
                                dtpPaymentDate.Value,

                            PaymentMethod =
                                cmbPaymentMethod.Text,

                            ReferenceNo =
                                txtReferenceNo.Text.Trim(),

                            Remarks =
                                txtRemarks.Text.Trim()
                        };

                    await _api.PostAsync<PaymentDTO>(
                        "Payment",
                        dto);

                    MessageBox.Show(
                        "Payment Added Successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                // ================= UPDATE =================

                else
                {
                    var dto =
                        new UpdatePaymentDTO
                        {
                            Id =
                                _payment.Id,

                            ReceiptNo =
                                txtReceiptNo.Text.Trim(),

                            StudentId =
                                Convert.ToInt32(
                                    txtStudentId.Text),

                            FeeStructureId =
                                Convert.ToInt32(
                                    txtFeeStructureId.Text),

                            Class =
                                cmbClass.Text,

                            TotalFee =
                                totalFee,

                            PaidAmount =
                                paidAmount,

                            Balance =
                                balance,

                            PaymentDate =
                                dtpPaymentDate.Value,

                            PaymentMethod =
                                cmbPaymentMethod.Text,

                            ReferenceNo =
                                txtReferenceNo.Text.Trim(),

                            Remarks =
                                txtRemarks.Text.Trim()
                        };

                    await _api.PutAsync(
                        $"Payment/{_payment.Id}",
                        dto);

                    MessageBox.Show(
                        "Payment Updated Successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                DialogResult = DialogResult.OK;

                Close();
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

        // ================= CLEAR FORM =================

        private void ClearForm()
        {
            txtReceiptNo.Clear();

            txtStudentId.Clear();

            txtFeeStructureId.Clear();

            cmbClass.SelectedIndex = -1;

            txtTotalFee.Clear();

            txtPaidAmount.Clear();

            cmbPaymentMethod.SelectedIndex = -1;

            txtReferenceNo.Clear();

            txtRemarks.Clear();

            dtpPaymentDate.Value =
                DateTime.Now;

            errorProvider.Clear();

            txtReceiptNo.BackColor = Color.White;

            txtStudentId.BackColor = Color.White;

            txtFeeStructureId.BackColor = Color.White;

            cmbClass.BackColor = Color.White;

            txtTotalFee.BackColor = Color.White;

            txtPaidAmount.BackColor = Color.White;

            cmbPaymentMethod.BackColor = Color.White;

            txtReferenceNo.BackColor = Color.White;

            txtRemarks.BackColor = Color.White;
        }

        // ================= REALTIME VALIDATION =================

        private void txtReceiptNo_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtReceiptNo.Text))
            {
                SetError(
                    txtReceiptNo,
                    "Receipt Number required");
            }
            else
            {
                ClearError(txtReceiptNo);
            }
        }

        private void txtStudentId_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (!int.TryParse(
                txtStudentId.Text,
                out int id))
            {
                SetError(
                    txtStudentId,
                    "Invalid Student ID");
            }
            else
            {
                ClearError(txtStudentId);
            }
        }

        private void txtFeeStructureId_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (!int.TryParse(
                txtFeeStructureId.Text,
                out int id))
            {
                SetError(
                    txtFeeStructureId,
                    "Invalid Fee Structure ID");
            }
            else
            {
                ClearError(txtFeeStructureId);
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

        private void txtTotalFee_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (!decimal.TryParse(
                txtTotalFee.Text,
                out decimal fee))
            {
                SetError(
                    txtTotalFee,
                    "Invalid Total Fee");
            }
            else
            {
                ClearError(txtTotalFee);
            }
        }

        private void txtPaidAmount_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (!decimal.TryParse(
                txtPaidAmount.Text,
                out decimal amount))
            {
                SetError(
                    txtPaidAmount,
                    "Invalid Paid Amount");
            }
            else
            {
                ClearError(txtPaidAmount);
            }
        }

        private void cmbPaymentMethod_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (cmbPaymentMethod.SelectedIndex == -1)
            {
                SetError(
                    cmbPaymentMethod,
                    "Select Payment Method");
            }
            else
            {
                ClearError(cmbPaymentMethod);
            }
        }

        private void txtReferenceNo_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtReferenceNo.Text))
            {
                SetError(
                    txtReferenceNo,
                    "Reference Number required");
            }
            else
            {
                ClearError(txtReferenceNo);
            }
        }

        private void txtRemarks_Validating(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtRemarks.Text))
            {
                SetError(
                    txtRemarks,
                    "Remarks required");
            }
            else
            {
                ClearError(txtRemarks);
            }
        }
    }
}