namespace SchoolFeeManagemetSystem.API.Interface
{
    public class ReportDTO
    {
        public string ReceiptNo { get; internal set; }
        public string StudentName { get; internal set; }
        public string Class { get; internal set; }
        public decimal Amount { get; internal set; }
        public string FeeCategory { get; internal set; }
        public DateTime PaymentDate { get; internal set; }
        public string PaymentMethod { get; internal set; }
        public string ReferenceNo { get; internal set; }
    }
}