using static SchoolFeeManagemetSystem.API.DTOs.PaymentDTOs;


namespace SchoolFeeManagementSystem.Forms
{
    public partial class PaymentManagement : Form
    {
        private readonly ApiClient _api = new ApiClient();
        private List<PaymentDTO> _allData = new List<PaymentDTO>();
        private int selectedId = 0;

        private int currentPage = 1;
        private int pageSize = 8;

        public PaymentManagement()
        {
            InitializeComponent();
            LoadData();
        }

        private async Task LoadData()
        {
           
                var data = await _api.GetAllAsync<PaymentDTO>("Payment");
                dgvPayments.DataSource = data;
                lblTotalRecords.Text = $"Total Records : {data.Count}";
           
        }
        private void LoadPage()
        {
            var pageData = _allData
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            dgvPayments.DataSource = null;
            dgvPayments.DataSource = pageData;
        }
        private async void PaymentManagement_Load(object sender, EventArgs e)
        {
            await LoadData();
        }
        // ➕ ADD
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new PaymentForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                await LoadData();
            }
        }

        // ✏ UPDATE
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvPayments.CurrentRow == null)
            {
                MessageBox.Show("Please select a row first");
                return;
            }

            var selected = dgvPayments.CurrentRow.DataBoundItem as PaymentDTO;

            if (selected == null)
            {
                MessageBox.Show("Invalid selection");
                return;
            }

            var form = new PaymentForm(selected); 

            if (form.ShowDialog() == DialogResult.OK)
            {
                await LoadData(); 
            }
        }

        // ❌ DELETE
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPayments.CurrentRow == null) return;

            var data = (PaymentDTO)dgvPayments.CurrentRow.DataBoundItem;

            await _api.DeletePaymentAsync(data.Id);
            MessageBox.Show("Deleted Successfully");
            LoadData();
        }

        // 🔄 CLEAR
        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvPayments.ClearSelection();
        }

        // ▶ NEXT
        private void btnNext_Click(object sender, EventArgs e)
        {
            if ((currentPage * pageSize) < _allData.Count)
            {
                currentPage++;
                LoadPage();
            }
        }

        // ◀ BACK
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadPage();
            }
        }

        // 🖨 PRINT RECEIPT (PDF)
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvPayments.CurrentRow == null)
            {
                MessageBox.Show("Select payment first");
                return;
            }

            var data = (PaymentDTO)dgvPayments.CurrentRow.DataBoundItem;

            PdfHelper.GenerateReceipt(data);
            MessageBox.Show("PDF Generated Successfully!");
        }

        

       
    }
}






