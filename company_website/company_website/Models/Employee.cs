using System;
using System.Collections.Generic;

namespace company_website.Models;

public partial class Employee
{
    public int Id { get; set; }

    public int? DepartmentId { get; set; }

    public int? UserAccountId { get; set; }

    public string? Fullname { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? IdNo { get; set; }
    public string? PhoneNumber { get; set; }
    public virtual Department? Department { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual UserAccount? UserAccount { get; set; }
}
