namespace SchoolFeeManagemetSystem.API.DTOs
{
    public class FeeStructureDTOs
    {
        public class CreateFeeStructureDTO
        {
            public string Class { get; set; }=String.Empty;

            public int FeeCategoryId { get; set; }

            public decimal Amount { get; set; }
            public string Frequency { get; set; }=String.Empty;

            public int DueDay { get; set; }
            public decimal LateFee { get; set; }
            public int GraceDays { get; set; }

            public string Description { get; set; }= String.Empty;
        }
        public class UpdateFeeStructureDTO
        {
            public int Id { get; set; }

            public string Class { get; set; }=String.Empty;

            public int FeeCategoryId { get; set; }

            public decimal Amount { get; set; }
            public string Frequency { get; set; }=String.Empty;

            public int DueDay { get; set; }
            public decimal LateFee { get; set; }
            public int GraceDays { get; set; }

            public string Description { get; set; }= string.Empty;
        }
        public class FeeStructureDTO
        {
            public int Id { get; set; }

            public string Class { get; set; }=string.Empty;

            public int FeeCategoryId { get; set; }
            public string FeeCategoryName { get; set; }= string.Empty;

            public decimal Amount { get; set; }
            public string Frequency { get; set; }=string.Empty;

            public int DueDay { get; set; }
            public decimal LateFee { get; set; }
            public int GraceDays { get; set; }

            public string Description { get; set; }= string.Empty;
        }
    }
}
