using static SchoolFeeManagemetSystem.API.DTOs.PaymentDTOs;
using ScoolFeeManagementSystem.Data.Entities;

namespace SchoolFeeManagementSystem.Forms
{
    public partial class PaymentForm : Form
    {
        private readonly ApiClient _api = new ApiClient();
        private PaymentDTO _payment;
        private PaymentDTO? selected;
        public PaymentForm()
        {
            InitializeComponent();
        }

        // ✅ UPDATE MODE
        public PaymentForm(PaymentDTO selected)
        {
            InitializeComponent(); 
            _payment = selected;

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
             
                if (string.IsNullOrWhiteSpace(txtReceiptNo.Text))
                {
                    MessageBox.Show("Receipt No is required");
                    return;
                }

                if (!decimal.TryParse(txtTotalFee.Text, out decimal totalFee))
                {
                    MessageBox.Show("Invalid Total Fee");
                    return;
                }

                if (!decimal.TryParse(txtPaidAmount.Text, out decimal paidAmount))
                {
                    MessageBox.Show("Invalid Paid Amount");
                    return;
                }

                decimal balance = totalFee - paidAmount;

                // ================= ADD =================
                if (_payment == null)
                {
                    var dto = new CreatePaymentDTO
                    {
                        ReceiptNo = txtReceiptNo.Text,
                        StudentId = int.Parse(txtStudentId.Text),
                        FeeStructureId = int.Parse(txtFeeStructureId.Text),
                        Class = cmbClass.Text,
                        TotalFee = totalFee,
                        PaidAmount = paidAmount,
                        Balance = balance,
                        PaymentDate = dtpPaymentDate.Value,
                        PaymentMethod = cmbPaymentMethod.Text,
                        ReferenceNo = txtReferenceNo.Text,
                        Remarks = txtRemarks.Text
                    };

                    await _api.PostAsync<PaymentDTO>("Payment", dto);

                    MessageBox.Show("Added Successfully");
                }
                // ================= UPDATE =================
                else
                {
                    var dto = new UpdatePaymentDTO
                    {
                        Id = _payment.Id,
                        ReceiptNo = txtReceiptNo.Text,
                        StudentId = int.Parse(txtStudentId.Text),
                        FeeStructureId = int.Parse(txtFeeStructureId.Text),
                        Class = cmbClass.Text,
                        TotalFee = totalFee,
                        PaidAmount = paidAmount,
                        Balance = balance,
                        PaymentDate = dtpPaymentDate.Value,
                        PaymentMethod = cmbPaymentMethod.Text,
                        ReferenceNo = txtReferenceNo.Text,
                        Remarks = txtRemarks.Text
                    };

                    await _api.PutAsync("Payment", dto);

                    MessageBox.Show("Updated Successfully");
                }

      
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