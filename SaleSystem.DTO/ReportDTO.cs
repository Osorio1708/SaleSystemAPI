using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleSystem.DTO
{
    public class ReportDTO
    {
        public string? DocumentNumber {  get; set; }
        public string? PaymentType { get; set; }
        public string? RegistrationDate { get; set; }
        public string? TotalSale { get; set; }
        public string? Product {  get; set; }    
        public int? Quantity {  get; set; }
        public string? Price {  get; set; } 
        public string? Total {  get; set; }  
    }
}
