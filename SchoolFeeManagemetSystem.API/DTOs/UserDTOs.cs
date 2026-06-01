namespace SchoolFeeManagemetSystem.API.DTOs
{
    public class UserDTOs
    {
        public class CreateUserDTO
        {
            public string Username { get; set; }=String.Empty;
            public string Password { get; set; }= String.Empty;
            public string FullName { get; set; }=String.Empty;
            public string Role { get; set; }= String.Empty;
        }

        public class UpdateUserDTO
        {
            public int Id { get; set; }
            public string FullName { get; set; }=String.Empty;
            public string Role { get; set; }= String.Empty;
            public bool IsActive { get; set; }
        }

        public class LoginDTO
        {
            public string Username { get; set; }=String.Empty;
            public string Password { get; set; }= String.Empty;
        }

        public class UserDTO
        {
            public int Id { get; set; }
            public string Username { get; set; }=String.Empty;
            public string FullName { get; set; }=String.Empty;
            public string Role { get; set; }= String.Empty;
        }
    }
}
