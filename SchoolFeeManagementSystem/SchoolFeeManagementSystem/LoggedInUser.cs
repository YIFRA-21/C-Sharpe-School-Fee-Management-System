using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolFeeManagementSystem
{
        public static class LoggedInUser
        {
            public static int UserId { get; set; }

            public static string FullName { get; set; } =  string.Empty;

            public static string Username { get; set; } = string.Empty;

            public static string Role { get; set; } = string.Empty;

            public static string LogoPath { get; set; } =  string.Empty;
        }
    }


