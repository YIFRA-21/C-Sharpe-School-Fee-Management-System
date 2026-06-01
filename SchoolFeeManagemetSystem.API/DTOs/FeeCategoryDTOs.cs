namespace SchoolFeeManagemetSystem.API.DTOs
{
    public class FeeCategoryDTOs
    {
        public class CreateFeeCategoryDTO
        {
            public string CategoryCode { get; set; }=String.Empty;
            public string CategoryName { get; set; }= String.Empty;

            public string Description { get; set; }= String.Empty;
            public string Frequency { get; set; }= String.Empty;

            public bool IsActive { get; set; }
            public string Status { get; set; }= String.Empty;
        }

        public class UpdateFeeCategoryDTO
        {
            public int Id { get; set; }

            public string CategoryCode { get; set; }= String.Empty;
            public string CategoryName { get; set; }= String.Empty;

            public string Description { get; set; }= String.Empty;
            public string Frequency { get; set; }= String.Empty;
            public bool IsActive { get; set; }
            public string Status { get; set; }= String.Empty;
        }
        public class FeeCategoryDTO
        {
            public int Id { get; set; }

            public string CategoryCode { get; set; }= String.Empty;
            public string CategoryName { get; set; }= String.Empty;

            public string Description { get; set; }= String.Empty;
            public string Frequency { get; set; }= String.Empty;

            public bool IsActive { get; set; }
        }
    }
}
