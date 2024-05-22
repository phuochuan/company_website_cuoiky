using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace company_website.Models;

public partial class Schedule
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? TaskId { get; set; }

    public string? DetailsTask { get; set; }

    public string? Status { get; set; }

    [Column("START_TIME")]
    public DateTime? StartTime { get; set; }
    [Column("END_TIME")]
    public DateTime? EndTime { get; set; }

    public string? IdNo { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Task? Task { get; set; }
}
