namespace SchoolFeeManagemetSystem.API.DTOs
{
    public class SystemSettingDTOs
    {

        public class UpdateSystemSettingDTO
        {
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
}
