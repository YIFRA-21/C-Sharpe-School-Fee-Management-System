using static SchoolFeeManagemetSystem.API.DTOs.PaymentDTOs;

namespace SchoolFeeManagemetSystem.API.DTOs
{
    public class DashboardDTO
    {
        public int TotalStudents { get; set; }
        public decimal TotalFee { get; set; }
        public decimal TotalCollected { get; set; }
        public decimal TotalOutstanding { get; set; }
        public decimal TodayCollection { get; set; }

        public int StudentsWithDues { get; set; }
        public decimal OverdueAmount { get; set; }

        public List<PaymentDTO> RecentPayments { get; set; }=new List<PaymentDTO>();

        // 🔥 NEW FOR CHARTS
        public Dictionary<string, decimal> MonthlyCollection { get; set; }=new Dictionary<string, decimal>();
        public Dictionary<string, decimal> MonthlyOutstanding { get; set; }=new Dictionary<string, decimal>();
        public Dictionary<string, decimal> FeeCategoryData { get; set; }=new Dictionary<string, decimal>();
    }
}
