namespace SchoolFeeManagemetSystem.API.DTOs
{
    public class StudentDTOs
    {
        public class CreateStudentDTO
        {
            public string AdmissionNo { get; set; }=String.Empty;
            public string RollNo { get; set; }=String.Empty;    

            public string FullName { get; set; }=String.Empty;
            public string Class { get; set; }=String.Empty;
            public string Section { get; set; }= String.Empty;

            public DateTime DateOfBirth { get; set; }
            public string Gender { get; set; }=String.Empty;

            public string PhoneNumber { get; set; }=String.Empty;
            public string Email { get; set; }=String.Empty;
            public string Address { get; set; }=String.Empty;
        }

        public class UpdateStudentDTO
        {
            public int Id { get; set; }

            public string AdmissionNo { get; set; }=String.Empty;
            public string RollNo { get; set; }=string.Empty;

            public string FullName { get; set; }=String.Empty;
            public string Class { get; set; }=String.Empty;
            public string Section { get; set; }= String.Empty;

            public DateTime DateOfBirth { get; set; }
            public string Gender { get; set; }=String.Empty;    
            public string PhoneNumber { get; set; }=String.Empty;
            public string Email { get; set; }=String.Empty;
            public string Address { get; set; }=String.Empty;
        }

        public class StudentDTO
        {
            public int Id { get; set; }

            public string AdmissionNo { get; set; }=String.Empty;
            public string RollNo { get; set; }=String.Empty;

            public string FullName { get; set; }=String.Empty;
            public string Class { get; set; }=String.Empty;
            public string Section { get; set; }= String.Empty;
            public DateTime DateOfBirth { get; set; }
            public string Gender { get; set; }=String.Empty;

            public string PhoneNumber { get; set; } = String.Empty;
            public string Email { get; set; } = String.Empty;
            public string Address { get; set; } = String.Empty;
        }
    }
}
