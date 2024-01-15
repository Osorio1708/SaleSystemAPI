using System;
using System.Collections.Generic;

namespace SaleSystem.Model;

public partial class DocumentNumber
{
    public int IdDocumentNumber { get; set; }

    public int LastNumber { get; set; }

    public DateTime? RegistrationDate { get; set; }
}
