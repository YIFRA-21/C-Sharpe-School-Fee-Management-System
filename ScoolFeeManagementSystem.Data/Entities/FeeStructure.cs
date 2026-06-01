using System;
using System.Collections.Generic;
using System.Text;

namespace ScoolFeeManagementSystem.Data.Entities
{
    public class FeeStructure
    {
        public int Id { get; set; }

        public string Class { get; set; }

        public int FeeCategoryId { get; set; }
        public FeeCategory FeeCategory { get; set; }

        public decimal Amount { get; set; }
        public string Frequency { get; set; }

        public int DueDay { get; set; }
        public decimal LateFee { get; set; }
        public int GraceDays { get; set; }

        public string Description { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
