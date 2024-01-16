using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleSystem.DTO
{
    public class SaleDTO
    {
        public int IdSale { get; set; }

        public string? DocumentNumber { get; set; }

        public string? PaymentType { get; set; }

        public string? Total { get; set; }

        public string? RegistrationDate { get; set; }

        public virtual ICollection<SaleDetailDTO>? SaleDetails { get; set; }
    }
}
