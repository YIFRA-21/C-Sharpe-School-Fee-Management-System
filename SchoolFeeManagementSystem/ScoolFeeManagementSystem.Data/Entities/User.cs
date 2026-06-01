namespace ScoolFeeManagementSystem.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = String.Empty;
        public string Password { get; set; }= String.Empty;
        public string PasswordHash { get; set; } = String.Empty;

        public string FullName { get; set; } = String.Empty;

        public string Role { get; set; } = String.Empty;

        public bool IsActive { get; set; }

        // ADD THIS
        public string LogoPath { get; set; } = String.Empty;
    }
}