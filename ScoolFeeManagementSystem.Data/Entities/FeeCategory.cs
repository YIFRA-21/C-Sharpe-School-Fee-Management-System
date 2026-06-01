using System;
using System.Collections.Generic;
using System.Text;

namespace ScoolFeeManagementSystem.Data.Entities
{
    public class FeeCategory
    {
        public int Id { get; set; }

        public string CategoryCode { get; set; } = String.Empty;// FEE-001
        public string CategoryName { get; set; }=String.Empty;

        public string Description { get; set; }= String.Empty;
        public string Frequency { get; set; }= String.Empty; // Monthly, Yearly, OneTime

        public bool IsActive { get; set; }

        public ICollection<FeeStructure> FeeStructures { get; set; }= new List<FeeStructure>();
    }
}
