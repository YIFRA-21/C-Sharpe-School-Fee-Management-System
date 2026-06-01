

namespace SchoolFeeManagementSystem.Forms
{
    using iText.Kernel.Pdf;
    using iText.Layout;
    using iText.Layout.Element;
    using static SchoolFeeManagemetSystem.API.DTOs.PaymentDTOs;

    public static class PdfHelper
    {
        public static void GenerateReceipt(PaymentDTO data)
        {
            string path = $"Receipt_{data.ReceiptNo}.pdf";

            using (var writer = new PdfWriter(path))
            using (var pdf = new PdfDocument(writer))
            using (var doc = new Document(pdf))
            {
                doc.Add(new Paragraph("SCHOOL FEE RECEIPT"));
                doc.Add(new Paragraph("----------------------------"));

                doc.Add(new Paragraph($"Receipt No: {data.ReceiptNo}"));
                doc.Add(new Paragraph($"Student: {data.StudentName}"));
                doc.Add(new Paragraph($"Class: {data.Class}"));
                doc.Add(new Paragraph($"Category: {data.FeeCategory}"));
                doc.Add(new Paragraph($"Total Fee: {data.TotalFee}"));
                doc.Add(new Paragraph($"Paid: {data.PaidAmount}"));
                doc.Add(new Paragraph($"Balance: {data.Balance}"));
                doc.Add(new Paragraph($"Date: {data.PaymentDate:d}"));
                doc.Add(new Paragraph($"Method: {data.PaymentMethod}"));
                doc.Add(new Paragraph($"Reference: {data.ReferenceNo}"));

                doc.Add(new Paragraph("----------------------------"));
                doc.Add(new Paragraph("Thank You"));
            }
        }
    }
}
