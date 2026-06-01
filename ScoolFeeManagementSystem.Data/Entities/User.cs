using System;
using System.Collections.Generic;
using System.Text;

namespace ScoolFeeManagementSystem.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }=String.Empty;
        public string PasswordHash { get; set; }=String.Empty;

        public string FullName { get; set; }=String.Empty;
        public string Role { get; set; } = String.Empty; // Admin, Staff
        public bool IsActive { get; set; }
    }
}
