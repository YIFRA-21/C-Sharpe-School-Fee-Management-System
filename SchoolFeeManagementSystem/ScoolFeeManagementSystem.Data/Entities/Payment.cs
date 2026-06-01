using System;
using System.Collections.Generic;
using System.Text;

namespace ScoolFeeManagementSystem.Data.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public string ReceiptNo { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int FeeStructureId { get; set; }
        public FeeStructure FeeStructure { get; set; }

        public string Class { get; set; }

        public decimal TotalFee { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Balance { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; } // Cash / Online
        public string ReferenceNo { get; set; }

        public string Remarks { get; set; }
    }
}
