using System;
using System.Collections.Generic;
using System.Text;

namespace ScoolFeeManagementSystem.Data.Entities
{
    public class SystemSetting
    {
        public int Id { get; set; }

        public string SchoolName { get; set; }=String.Empty;
        public string Address { get; set; }= String.Empty;
        public string PhoneNo { get; set; }= String.Empty;
        public string Email { get; set; }= String.Empty;
        public string Website { get; set; }

        public string CurrentSession { get; set; }
        public string Currency { get; set; }
        public string DateFormat { get; set; }
        public string Theme { get; set; }= String.Empty;

        public bool EnableEmailNotification { get; set; }
        public bool EnableSMSNotification { get; set; }
    }
}
