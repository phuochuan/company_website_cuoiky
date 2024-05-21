using System;
using System.Collections.Generic;

namespace company_website.Models;

public partial class Department
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }

    public string? Description { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
