namespace SchoolFeeManagemetSystem.API.DTOs
{
    public class StaffDTOs
    {
        public class CreateStaffDto
        {
            public string FullName { get; set; }=String.Empty;

            public string Position { get; set; }=String.Empty;

            public string Department { get; set; }=String.Empty;

            public string Email { get; set; }=String.Empty; 
            public string Phone { get; set; }=String.Empty;

            public string Address { get; set; }=String.Empty;

            public decimal Salary { get; set; }=Decimal.MaxValue;
        }
        public class UpdateStaffDto
        {
            public int StaffId { get; set; }

            public string FullName { get; set; }=String.Empty;

            public string Position { get; set; }=String.Empty;

            public string Department { get; set; }=String.Empty;
            public string Email { get; set; }=String.Empty;

            public string Phone { get; set; }=String.Empty;

            public string Address { get; set; }=String.Empty;   

            public decimal Salary { get; set; }=decimal.MaxValue;
        }
        public class StaffDtos
        {
            public int StaffId { get; set; } 

            public string FullName { get; set; }= String.Empty;

            public string Position { get; set; }=String.Empty;

            public string Department { get; set; }=String.Empty;

            public string Email { get; set; }=String.Empty;
            public string Phone { get; set; }=String.Empty;

            public string Address { get; set; }=String.Empty;

            public decimal Salary { get; set; }
            public DateTime CreatedDate { get; set; }
        }
    }
}
