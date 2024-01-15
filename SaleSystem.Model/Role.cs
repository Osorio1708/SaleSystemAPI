using System;
using System.Collections.Generic;

namespace SaleSystem.Model;

public partial class Role
{
    public int IdRole { get; set; }

    public string? Name { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<MenuRole> MenuRoles { get; set; } = new List<MenuRole>();
}
