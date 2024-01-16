using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleSystem.DTO
{
    public class SaleDetailDTO
    {
        public int? IdProduct { get; set; }

        public string? productDescription { get; set; }

        public int? Quantity { get; set; }

        public string? Price { get; set; }

        public string? Total { get; set; }
    }
}
