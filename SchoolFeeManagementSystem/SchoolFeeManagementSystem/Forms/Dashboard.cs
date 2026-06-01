using System.Windows.Forms.DataVisualization.Charting;
using static SchoolFeeManagemetSystem.API.DTOs.PaymentDTOs;

namespace SchoolFeeManagementSystem.Forms
{
    public partial class Dashboard : Form
    {
        private readonly ApiClient _api = new ApiClient();

        public Dashboard()
        {
            InitializeComponent();
        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            await LoadDashboard();
            await LoaddgvRecentPaymentsTables();
            await LoadOutstandingSummary();
        }


        private async Task LoaddgvRecentPaymentsTables()
        {
            try
            {
                var data = await _api.GetAllAsync<PaymentDTO>("Payment");
                var outstanding = data.Where(x => x.Balance > 0).ToList();
                dgvRecentPayments.DataSource = outstanding;
                dgvRecentPayments.ScrollBars = ScrollBars.Both;

                dgvRecentPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvRecentPayments.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                dgvRecentPayments.RowTemplate.Height = 30;

                dgvRecentPayments.Dock = DockStyle.Fill;
                // Set column width
                foreach (DataGridViewColumn col in dgvRecentPayments.Columns)
                {
                    col.Width = 130;
                }
                dgvRecentPayments.AutoGenerateColumns = true; 
                dgvRecentPayments.DataSource = null;
                dgvRecentPayments.DataSource = outstanding;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private async Task LoadOutstandingSummary()
        {
            try
            {
                var data = await _api.GetAllAsync<PaymentDTO>("Payment");

                if (data == null || !data.Any())
                {
                    dgvOutstandingSummery.DataSource = null;
                    lblStudentWithDues.Text = "0";
                    lblTotalOutstanding.Text = "0.00";
                    lblOverdueAmount.Text = "0.00";
                    return;
                }

                // 🔥 FILTER ONLY OUTSTANDING
                var outstanding = data.Where(x => x.Balance > 0).ToList();

                // ================= SUMMARY =================
                lblStudentWithDues.Text = outstanding
                    .Select(x => x.StudentName)
                    .Distinct()
                    .Count()
                    .ToString();

                lblTotalOutstanding.Text = outstanding
                    .Sum(x => x.Balance)
                    .ToString("N2");

                // (Optional overdue logic)
                lblOverdueAmount.Text = outstanding
                    .Where(x => x.PaymentDate < DateTime.Now.AddDays(-30))
                    .Sum(x => x.Balance)
                    .ToString("N2");

                // ================= TABLE =================
                var tableData = outstanding
                    .GroupBy(x => x.Class)
                    .Select(g => new
                    {
                        Class = g.Key,
                        TotalStudents = g.Select(x => x.StudentName).Distinct().Count(),
                        TotalOutstanding = g.Sum(x => x.Balance),
                        OverDueAmount = g
                            .Where(x => x.PaymentDate < DateTime.Now.AddDays(-30))
                            .Sum(x => x.Balance)
                    })
                    .ToList();

                dgvOutstandingSummery.AutoGenerateColumns = true;
                dgvOutstandingSummery.DataSource = null;
                dgvOutstandingSummery.DataSource = tableData;
                lblTotalOutstanding.ForeColor = Color.Red;
                dgvOutstandingSummery.DataSource = outstanding;
                dgvOutstandingSummery.ScrollBars = ScrollBars.Both;
                dgvOutstandingSummery.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                dgvOutstandingSummery.RowTemplate.Height = 30;

                dgvOutstandingSummery.Dock = DockStyle.Fill;
                lblStudentWithDues.Text= $"Students with Dues : {outstanding.Select(x => x.StudentName).Distinct().Count()}";
                lblTotalOutstanding.Text= $"Total Outstanding : {outstanding.Sum(x => x.Balance).ToString("N2")}";
                lblOverdueAmount.Text= $"Overdue Amount : {outstanding.Where(x => x.PaymentDate < DateTime.Now.AddDays(-30)).Sum(x => x.Balance).ToString("N2")}";



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // ================= MAIN =================
        private async Task LoadDashboard()
        {
            var data = await _api.GetAllAsync<PaymentDTO>("Payment");

            if (data == null || data.Count == 0)
            {
                MessageBox.Show("No payment data found!");
                return;
            }
            LoadCards(data);
            BuildBarChart(data);
            BuildPieChart(data);
        }

        // ================= CARDS =================
        private void LoadCards(List<PaymentDTO> data)
        {
            lblTotalStudents.Text = data
                .Select(x => x.StudentName)
                .Distinct()
                .Count().ToString();

            lblTotalFee.Text = data.Sum(x => x.TotalFee).ToString("N2");

            lblTotalCollected.Text = data.Sum(x => x.PaidAmount).ToString("N2");

            lblTotalOutstanding.Text = data.Sum(x => x.Balance).ToString("N2");

            lblTodayCollection.Text = data
                .Where(x => x.PaymentDate.Date == DateTime.Today)
                .Sum(x => x.PaidAmount)
                .ToString("N2");
            lblTotalStudents.Text = $"Total Students : {data.Select(x => x.StudentName).Distinct().Count()} ";
            lblTotalFee.Text = $"Total Fee : {data.Sum(x => x.TotalFee).ToString("N2")}";
            lblTotalCollected.Text = $"Total Collected : {data.Sum(x => x.PaidAmount).ToString("N2")}";
            lblTotalOutstanding.Text = $"Total Outstanding : {data.Sum(x => x.Balance).ToString("N2")}";
            lblTodayCollection.Text = $"Today's Collection : {data.Where(x => x.PaymentDate.Date == DateTime.Today).Sum(x => x.PaidAmount).ToString("N2")}";

        }

        // ================= BAR CHART =================
        private void BuildBarChart(List<PaymentDTO> data)
        {
            panelBarChart.Controls.Clear();

            Chart chart = new Chart();
            chart.Dock = DockStyle.Fill;

            ChartArea area = new ChartArea();
            area.AxisX.Interval = 1;
            chart.ChartAreas.Add(area);

            Series collected = new Series("Collected");
            collected.ChartType = SeriesChartType.Column;
            collected.Color = Color.Green;

            Series outstanding = new Series("Outstanding");
            outstanding.ChartType = SeriesChartType.Column;
            outstanding.Color = Color.Red;

            // 🔥 FIX: SHOW LAST 7 DAYS
            var last7Days = Enumerable.Range(0, 7)
                .Select(i => DateTime.Today.AddDays(-i))
                .OrderBy(d => d)
                .ToList();

            foreach (var day in last7Days)
            {
                var dayData = data.Where(x => x.PaymentDate.Date == day.Date);

                decimal paid = dayData.Sum(x => x.PaidAmount);
                decimal balance = dayData.Sum(x => x.Balance);

                collected.Points.AddXY(day.ToString("MMM dd"), paid);
                outstanding.Points.AddXY(day.ToString("MMM dd"), balance);
            }

            chart.Series.Add(collected);
            chart.Series.Add(outstanding);

            chart.Titles.Add("Fee Collection Overview (This Month)");

            panelBarChart.Controls.Add(chart);
        }

        // ================= PIE CHART =================
        private void BuildPieChart(List<PaymentDTO> data)
        {
            panelPieChart.Controls.Clear();

            Chart chart = new Chart();
            chart.Dock = DockStyle.Fill;

            ChartArea area = new ChartArea();
            chart.ChartAreas.Add(area);

            Series pie = new Series();
            pie.ChartType = SeriesChartType.Doughnut;

            pie.Label = "#PERCENT{P0}";
            pie.LegendText = "#VALX";

            // 🔥 FIX: GROUP BY CATEGORY CORRECTLY
            var grouped = data
                .GroupBy(x => string.IsNullOrEmpty(x.FeeCategory) ? "Other" : x.FeeCategory)
                .Select(x => new
                {
                    Category = x.Key,
                    Amount = x.Sum(p => p.PaidAmount)
                })
                .Where(x => x.Amount > 0)
                .ToList();

            foreach (var item in grouped)
            {
                pie.Points.AddXY(item.Category, item.Amount);
            }

            chart.Series.Add(pie);

            chart.Titles.Add("Fee Collection By Category");

            panelPieChart.Controls.Add(chart);
        }

        // ================= REFRESH =================
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadDashboard();
            await LoaddgvRecentPaymentsTables();
            await LoadOutstandingSummary();
        }

        // ================= BUTTONS =================
        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            new StudentForm().ShowDialog();
        }

        private void btnAddFee_Click(object sender, EventArgs e)
        {
            new Fees().ShowDialog();
        }

        private void btnCollectPayment_Click(object sender, EventArgs e)
        {
            new PaymentForm().ShowDialog();
        }

        private void btnGenerateReceipt_Click(object sender, EventArgs e)
        {
            new PaymentManagement().ShowDialog();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            new ViewReports().ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            new AccountSetting().ShowDialog();
        }
    }
}