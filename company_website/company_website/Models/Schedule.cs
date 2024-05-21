using System;
using System.Collections.Generic;

namespace company_website.Models;

public partial class Schedule
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? TaskId { get; set; }

    public string? DetailsTask { get; set; }

    public string? Status { get; set; }

    public string? IdNo { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Task? Task { get; set; }
}
