using System;
using System.Collections.Generic;
using System.Text;

namespace ScoolFeeManagementSystem.Data.Entities
{
    public class SystemSetting
    {
        public int Id { get; set; }

        public string SchoolName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        public string CurrentSession { get; set; }
        public string Currency { get; set; }
        public string DateFormat { get; set; }
        public string Theme { get; set; }

        public bool EnableEmailNotification { get; set; }
        public bool EnableSMSNotification { get; set; }
    }
}
