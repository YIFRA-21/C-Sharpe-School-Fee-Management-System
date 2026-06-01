using System.Drawing.Printing;
using static SchoolFeeManagemetSystem.API.DTOs.PaymentDTOs;

namespace SchoolFeeManagementSystem.Forms
{
    public partial class PaymentManagement : Form
    {
   

        private readonly ApiClient _api =
            new ApiClient();

     

        private List<PaymentDTO> _allData =
            new List<PaymentDTO>();

        private PaymentDTO selectedPayment;


        private int currentPage = 1;

        private int pageSize = 8;

  

        public PaymentManagement()
        {
            InitializeComponent();
        }


        private async void PaymentManagement_Load(
            object sender,
            EventArgs e)
        {
            await LoadData();
        }


        private async Task LoadData()
        {
            try
            {
                var data =
                    await _api.GetAllAsync<PaymentDTO>(
                        "Payment");

                _allData = data;

                LoadPage();

                lblTotalRecords.Text =
                    $"Total Records : {data.Count}";
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



        private void LoadPage()
        {
            var pageData =
                _allData
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            dgvPayments.DataSource = null;

            dgvPayments.DataSource = pageData;

            dgvPayments.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

      

        private async void btnAdd_Click(
            object sender,
            EventArgs e)
        {
            PaymentForm form =
                new PaymentForm();

            if (form.ShowDialog() ==
                DialogResult.OK)
            {
                await LoadData();
            }
        }



        private async void btnUpdate_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (dgvPayments.CurrentRow == null)
                {
                    MessageBox.Show(
                        "Please select payment first");

                    return;
                }

                var selected =
                    dgvPayments.CurrentRow
                    .DataBoundItem as PaymentDTO;

                if (selected == null)
                {
                    MessageBox.Show(
                        "Invalid selection");

                    return;
                }

                PaymentForm form =
                    new PaymentForm(selected);

                if (form.ShowDialog() ==
                    DialogResult.OK)
                {
                    await LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ================= DELETE =================

        private async void btnDelete_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (dgvPayments.CurrentRow == null)
                {
                    MessageBox.Show(
                        "Select payment first");

                    return;
                }

                var data =
                    (PaymentDTO)dgvPayments
                    .CurrentRow.DataBoundItem;

                DialogResult confirm =
                    MessageBox.Show(
                        "Delete this payment?",
                        "Confirm",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    await _api.DeletePaymentAsync(
                        data.Id);

                    MessageBox.Show(
                        "Deleted Successfully");

                    await LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ================= CLEAR =================

        private void btnClear_Click(
            object sender,
            EventArgs e)
        {
            dgvPayments.ClearSelection();
        }

        // ================= NEXT =================

        private void btnNext_Click(
            object sender,
            EventArgs e)
        {
            if ((currentPage * pageSize)
                < _allData.Count)
            {
                currentPage++;

                LoadPage();
            }
        }

 

        private void btnBack_Click(  object sender,  EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;

                LoadPage();
            }
        }

      

        private void btnPrint_Click( object sender, EventArgs e)
        {
            try
            {
                if (dgvPayments.CurrentRow == null)
                {
                    MessageBox.Show(
                        "Select payment first");

                    return;
                }

                selectedPayment =
                    dgvPayments.CurrentRow
                    .DataBoundItem as PaymentDTO;

                if (selectedPayment == null)
                {
                    MessageBox.Show(
                        "Invalid payment");

                    return;
                }

                PrintDocument printDocument = new PrintDocument();

                printDocument.PrintPage +=
                    PrintDocument_PrintPage;


                PrintPreviewDialog preview =
                    new PrintPreviewDialog();

                preview.Document =
                    printDocument;

                preview.Width = 1000;

                preview.Height = 700;

                preview.ShowDialog();

          

                PrintDialog printDialog =
                    new PrintDialog();

                printDialog.Document =
                    printDocument;

                if (printDialog.ShowDialog()
                    == DialogResult.OK)
                {
                    printDocument.Print();

                    MessageBox.Show(
                        "Receipt Printed Successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

   

        private void PrintDocument_PrintPage( object sender, PrintPageEventArgs e)
        {
            Font titleFont =
                new Font(
                    "Arial",
                    18,
                    FontStyle.Bold);

            Font bodyFont =
                new Font(
                    "Arial",
                    12,
                    FontStyle.Regular);

            int y = 50;

            // TITLE

            e.Graphics.DrawString(
                "SCHOOL FEE RECEIPT",
                titleFont,
                Brushes.Black,
                220,
                y);

            y += 60;

            // RECEIPT INFO

            e.Graphics.DrawString(
                "Receipt No : "
                + selectedPayment.ReceiptNo,
                bodyFont,
                Brushes.Black,
                50,
                y);

            y += 35;

            e.Graphics.DrawString(
                "Student Name : "
                + selectedPayment.StudentName,
                bodyFont,
                Brushes.Black,
                50,
                y);

            y += 35;

            e.Graphics.DrawString(
                "Student ID : "
                + selectedPayment.StudentId,
                bodyFont,
                Brushes.Black,
                50,
                y);

            y += 35;

            e.Graphics.DrawString(
                "Fee Structure ID : "
                + selectedPayment.FeeStructureId,
                bodyFont,
                Brushes.Black,
                50,
                y);

            y += 35;

            e.Graphics.DrawString(
                "Class : "
                + selectedPayment.Class,
                bodyFont,
                Brushes.Black,
                50,
                y);

            y += 35;

            e.Graphics.DrawString(
                "Total Fee : "
                + selectedPayment.TotalFee,
                bodyFont,
                Brushes.Black,
                50,
                y);

            y += 35;

            e.Graphics.DrawString(
                "Paid Amount : "
                + selectedPayment.PaidAmount,
                bodyFont,
                Brushes.Black,
                50,
                y);

            y += 35;

            e.Graphics.DrawString(
                "Balance : "
                + selectedPayment.Balance,
                bodyFont,
                Brushes.Black,
                50,
                y);

            y += 35;

            e.Graphics.DrawString(
                "Payment Method : "
                + selectedPayment.PaymentMethod,
                bodyFont,
                Brushes.Black,
                50,
                y);

            y += 35;

            e.Graphics.DrawString(
                "Payment Date : "
                + selectedPayment.PaymentDate
                .ToShortDateString(),
                bodyFont,
                Brushes.Black,
                50,
                y);

            y += 35;

            e.Graphics.DrawString(
                "Reference No : "
                + selectedPayment.ReferenceNo,
                bodyFont,
                Brushes.Black,
                50,
                y);

            y += 35;

            e.Graphics.DrawString(
                "Remarks : "
                + selectedPayment.Remarks,
                bodyFont,
                Brushes.Black,
                50,
                y);

            y += 70;

            e.Graphics.DrawString(
                "Signature : __________________",
                bodyFont,
                Brushes.Black,
                50,
                y);
        }
    }
}