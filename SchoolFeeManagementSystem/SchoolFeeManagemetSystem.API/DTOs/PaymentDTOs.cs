using System;

namespace SchoolFeeManagemetSystem.API.DTOs
{
    public class PaymentDTOs
    {
        // ================= CREATE PAYMENT DTO =================

        public class CreatePaymentDTO
        {
            public string ReceiptNo { get; set; } = string.Empty;

            public int StudentId { get; set; }

            public int FeeStructureId { get; set; }

            public string Class { get; set; } = string.Empty;

            public decimal TotalFee { get; set; }

            public decimal PaidAmount { get; set; }

            public decimal Balance { get; set; }

            public DateTime PaymentDate { get; set; }

            public string PaymentMethod { get; set; } = string.Empty;

            public string ReferenceNo { get; set; } = string.Empty;

            public string Remarks { get; set; } = string.Empty;
        }

        // ================= UPDATE PAYMENT DTO =================

        public class UpdatePaymentDTO
        {
            public int Id { get; set; }

            public string ReceiptNo { get; set; } = string.Empty;

            public int StudentId { get; set; }

            public int FeeStructureId { get; set; }

            public string Class { get; set; } = string.Empty;

            public decimal TotalFee { get; set; }

            public decimal PaidAmount { get; set; }

            public decimal Balance { get; set; }

            public DateTime PaymentDate { get; set; }

            public string PaymentMethod { get; set; } = string.Empty;

            public string ReferenceNo { get; set; } = string.Empty;

            public string Remarks { get; set; } = string.Empty;
        }

        // ================= PAYMENT DTO =================

        public class PaymentDTO
        {
            public int Id { get; set; }

            public string ReceiptNo { get; set; } = string.Empty;

            public int StudentId { get; set; }

            public int FeeStructureId { get; set; }
            public string StudentName { get; set; } = string.Empty;

            public string FeeCategory { get; set; } = string.Empty;

            public string Class { get; set; } = string.Empty;

            public decimal TotalFee { get; set; }

            public decimal PaidAmount { get; set; }

            public decimal Balance { get; set; }

            public DateTime PaymentDate { get; set; }

            public string PaymentMethod { get; set; } = string.Empty;

            public string ReferenceNo { get; set; } = string.Empty;

            public string Remarks { get; set; } = string.Empty;
        }
    }
}