using ClosedXML.Excel;
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
            cmbClass.Items.Clear();
            cmbFeeCategory.Items.Clear();

            cmbClass.Items.Add("All");
            cmbClass.Items.Add("Grade 1");
            cmbClass.Items.Add("Grade 2");
            cmbClass.Items.Add("Grade 3");

            cmbFeeCategory.Items.Add("All");
            cmbFeeCategory.Items.Add("Tuition");
            cmbFeeCategory.Items.Add("Library");
            cmbFeeCategory.Items.Add("Transport");

            cmbClass.SelectedIndex = 0;
            cmbFeeCategory.SelectedIndex = 0;

            await LoadReport();
        }

        // ================= LOAD REPORT =================
        private async Task LoadReport()
        {
            try
            {
                var from = dtFromDate.Value.Date;

                var to = dtToDate.Value.Date
                    .AddDays(1)
                    .AddSeconds(-1);

                var cls = cmbClass.Text == "All"
                    ? ""
                    : cmbClass.Text;

                var category = cmbFeeCategory.Text == "All"
                    ? ""
                    : cmbFeeCategory.Text;

                allData = await _api.GetReportsAsync(from, to, cls, category)
                          ?? new List<ReportDTO>();

                currentPage = 1;

                LoadPage();

                LoadSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                dgvReports.DataSource = null;

                allData = new List<ReportDTO>();
            }
        }

        // ================= LOAD PAGE =================
        private void LoadPage()
        {
            var pageData = allData
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            dgvReports.DataSource = null;

            dgvReports.DataSource = pageData;
        }

        // ================= NEXT =================
        private void btnNext_Click(object sender, EventArgs e)
        {
            if ((currentPage * pageSize) < allData.Count)
            {
                currentPage++;

                LoadPage();
            }
        }

        // ================= BACK =================
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
            if (allData == null || allData.Count == 0)
            {
                lblTotalStudents.Text = "0";
                lblTotalPaid.Text = "0.0";
                lblTotalCollection.Text = "0.0";
                lblTotalTransactions.Text = "0";
                lblTotalOutstanding.Text = "0.0";
                lblTotalReports.Text = "0";

                return;
            }
            int totalStudents = allData .Select(x => x.StudentName).Distinct().Count();

            decimal totalAmount = allData.Sum(x => x.Amount);

            lblTotalOutstanding.Text ="0.0";
            lblTotalReports.Text = "Total Reports: " + allData.Count.ToString();
            lblTotalStudents.Text = "Total Students: " + totalStudents.ToString();
            lblTotalPaid.Text= "Total Paid: " + totalAmount.ToString("N2");
            lblTotalCollection.Text= "Total Collection: " + totalAmount.ToString("N2");
            lblTotalTransactions.Text= "Total Transactions: " + allData.Count.ToString();
            lblTotalOutstanding.Text = "Total Outstanding: 0.0";

        }

        // ================= GENERATE REPORT =================
        private async void btnGenerateReport_Click(object sender, EventArgs e)
        {
            await LoadReport();
        }

        // ================= EXPORT EXCEL =================
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReports.Rows.Count == 0)
                {
                    MessageBox.Show("No Data Found!");

                    return;
                }

                SaveFileDialog save = new SaveFileDialog();

                save.Filter = "Excel File|*.xlsx";

                save.FileName = "Reports.xlsx";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add("Reports");

                        // HEADER
                        for (int i = 0; i < dgvReports.Columns.Count; i++)
                        {
                            ws.Cell(1, i + 1).Value =
                                dgvReports.Columns[i].HeaderText;
                        }

                        // DATA
                        for (int i = 0; i < dgvReports.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvReports.Columns.Count; j++)
                            {
                                ws.Cell(i + 2, j + 1).Value =
                                    dgvReports.Rows[i].Cells[j].Value?.ToString();
                            }
                        }

                        ws.Columns().AdjustToContents();

                        wb.SaveAs(save.FileName);
                    }

                    MessageBox.Show("Excel Exported Successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}