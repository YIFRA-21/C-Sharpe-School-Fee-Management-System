using System;
using System.Collections.Generic;
using System.Text;

namespace ScoolFeeManagementSystem.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }

        public string AdmissionNo { get; set; }
        public string RollNo { get; set; }

        public string FullName { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // Navigation
        public ICollection<Payment> Payments { get; set; }
    }
}
