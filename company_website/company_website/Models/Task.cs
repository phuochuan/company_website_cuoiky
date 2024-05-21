using System;
using System.Collections.Generic;

namespace company_website.Models;

public partial class Task
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? TaskDescription { get; set; }

    public DateOnly? ExpectedEndDate { get; set; }

    public DateOnly? StartDate { get; set; }

    public int? TeamSize { get; set; }

    public string? Status { get; set; }

    public byte[]? Thumbail { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
