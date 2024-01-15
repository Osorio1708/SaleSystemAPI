using System;
using System.Collections.Generic;

namespace SaleSystem.Model;

public partial class Sale
{
    public int IdSale { get; set; }

    public string? DocumentNumber { get; set; }

    public string? PaymentType { get; set; }

    public decimal? Total { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}
