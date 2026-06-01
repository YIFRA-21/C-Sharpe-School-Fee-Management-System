using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using static SchoolFeeManagemetSystem.API.DTOs.ReportDTOs;

namespace SchoolFeeManagementSystem.Forms
{
    public partial class ViewReports : Form
    {
        private readonly ApiClient _api = new ApiClient();

        private List<ReportDTO> allData = new List<ReportDTO>();
        private int currentPage = 1;
        private int pageSize = 10;

        public ViewReports()
        {
            InitializeComponent();
        }

        // ================= LOAD FORM =================
        private async void ViewReports_Load(object sender, EventArgs e)
        {
         

            await LoadReport();
        }

        // ================= LOAD REPORT =================
        private async Task LoadReport()
        {
            try
            {
                var from = dtFromDate.Value.Date;
                var to = dtToDate.Value.Date.AddDays(1).AddSeconds(-1); // FIX date issue

                var cls = cmbClass.Text == "All" ? "" : cmbClass.Text;
                var category = cmbFeeCategory.Text == "All" ? "" : cmbFeeCategory.Text;

                allData = await _api.GetReportsAsync(from, to, cls, category)
                          ?? new List<ReportDTO>();

                currentPage = 1;

                LoadPage();
                LoadSummary();

                lblTotalReports.Text = allData.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dgvReports.DataSource = null;
                allData = new List<ReportDTO>();
            }
        }

        // ================= PAGINATION =================
        private void LoadPage()
        {
            var pageData = allData
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            dgvReports.DataSource = null;
            dgvReports.DataSource = pageData;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if ((currentPage * pageSize) < allData.Count)
            {
                currentPage++;
                LoadPage();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadPage();
            }
        }

        // ================= SUMMARY =================
        private void LoadSummary()
        {
            if (allData == null || !allData.Any())
            {
                lblTotalStudents.Text = "0";
                lblTotalPaid.Text = "0.0";
                lblTotalCollection.Text = "0.0";
                lblTotalTransactions.Text = "0";
                lblTotalReports.Text = "0";
                lblTotalOutstanding.Text = "0.0";
                return;
            }

            lblTotalStudents.Text = allData.Select(x => x.StudentName)
                                           .Distinct()
                                           .Count()
                                           .ToString();

            decimal total = allData.Sum(x => x.Amount);

            lblTotalStudents.Text= $"Total Students : {allData.Select(x => x.StudentName).Distinct().Count()}";
            lblTotalPaid.Text = $"Total Paid : {total.ToString("N2")}";
            lblTotalCollection.Text = $"Total Collection : {total.ToString("N2")}";
            lblTotalTransactions.Text = $"Total Transactions : {allData.Count}";
            lblTotalOutstanding.Text = $"Total Outstanding : 0.0";
            lblTotalReports.Text= $"Total Reports : {allData.Count}";   
        }

        // ================= GENERATE BUTTON =================
        private async void btnGenerateReport_Click(object sender, EventArgs e)
        {
            await LoadReport();
        }

        // ================= EXPORT PDF =================
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (dgvReports.Rows.Count == 0)
            {
                MessageBox.Show("No data to export!");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Document doc = new Document(PageSize.A4);
                PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                doc.Open();

                PdfPTable table = new PdfPTable(dgvReports.Columns.Count);

                // HEADER
                foreach (DataGridViewColumn col in dgvReports.Columns)
                {
                    table.AddCell(new Phrase(col.HeaderText));
                }

                // DATA
                foreach (DataGridViewRow row in dgvReports.Rows)
                {
                    if (row.IsNewRow) continue;

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        table.AddCell(cell.Value?.ToString() ?? "");
                    }
                }

                doc.Add(table);
                doc.Close();

                MessageBox.Show("PDF Exported Successfully!");
            }
        }
    }
}