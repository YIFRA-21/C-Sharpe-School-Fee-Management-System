namespace SchoolFeeManagemetSystem.API.DTOs
{
    public class ReportDTOs
    {


        public class CreateReportDTO
        {
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }

            public string Class { get; set; }= string.Empty;
            public string FeeCategory { get; set; }= string.Empty;
        }
        public class UpdateReportDTO
        {
            public int Id { get; set; }

            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }

            public string Class { get; set; }= string.Empty;
            public string FeeCategory { get; set; }=string.Empty;
        }

        public class ReportFilterDTO
        {
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }

            public string Class { get; set; }= string.Empty;       
            public string FeeCategory { get; set; }= string.Empty;  
        }
        public class ReportDTO
        {
            public string ReceiptNo { get; set; } = string.Empty;

            public string StudentName { get; set; } = string.Empty;
            public string Class { get; set; } = string.Empty;

            public string FeeCategory { get; set; } = string.Empty;

            public decimal Amount { get; set; }

            public DateTime PaymentDate { get; set; }

            public string PaymentMethod { get; set; }= string.Empty;
            public string ReferenceNo { get; set; }= string.Empty;
        }
        public class ReportSummaryDTO
        {
            public decimal TotalCollection { get; set; }
            public decimal TotalPaid { get; set; }
            public decimal TotalOutstanding { get; set; }

            public int TotalStudents { get; set; }
            public int TotalTransactions { get; set; }
        }

        public class ReportResponseDTO
        {
            public List<ReportDTO> Data { get; set; }= new List<ReportDTO>();

            public ReportSummaryDTO Summary { get; set; }= new ReportSummaryDTO();
        }
    }
}
