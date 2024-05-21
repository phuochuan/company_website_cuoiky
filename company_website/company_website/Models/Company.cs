using System;
using System.Collections.Generic;

namespace company_website.Models;

public partial class Company
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Located { get; set; }

    public string? Facebook { get; set; }

    public string? X { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? FocusedfIeld { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
