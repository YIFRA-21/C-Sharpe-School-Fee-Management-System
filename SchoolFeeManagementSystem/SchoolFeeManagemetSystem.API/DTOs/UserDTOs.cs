namespace SchoolFeeManagemetSystem.API.DTOs
{
    public class UserDTOs
    {

        public class CreateUserDTO
        {
            public string Username { get; set; } =
                string.Empty;

            public string Password { get; set; } =
                string.Empty;

            public string FullName { get; set; } = string.Empty;

            public string Role { get; set; } =  string.Empty;

            public string LogoPath { get; set; } = string.Empty;
        }

        public class UpdateUserDTO
        {
            public int Id { get; set; }

            public string Username { get; set; } =string.Empty;

            public string Password { get; set; } =  string.Empty;

            public string FullName { get; set; } = string.Empty;

            public string Role { get; set; } =  string.Empty;

            public bool IsActive { get; set; }

            public string LogoPath { get; set; } = string.Empty;
        }

        public class LoginDTO
        {
            public string Username { get; set; } =string.Empty;

            public string Password { get; set; } = string.Empty;
        }

        public class UserDTO
        {
            public int Id { get; set; }

            public string Username { get; set; } =string.Empty;

            public string Password { get; set; } =  string.Empty;

            public string FullName { get; set; } = string.Empty;

            public string Role { get; set; } = string.Empty;

            public bool IsActive { get; set; }

            public string LogoPath { get; set; } =   string.Empty;
            public string NewPassword { get; internal set; }=string.Empty;
        }

        public class UpdateAccountDTO
        {
            public int Id { get; set; }

            public string FullName { get; set; } = string.Empty;

            public string Username { get; set; } = string.Empty;

            public string OldPassword { get; set; } =  string.Empty;

            public string NewPassword { get; set; } = string.Empty;

            public string Role { get; set; } =   string.Empty;

            public string LogoPath { get; set; } = string.Empty;
        }
    }
}