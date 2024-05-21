using System;
using System.Collections.Generic;

namespace company_website.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
}
