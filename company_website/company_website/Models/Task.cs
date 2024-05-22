using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
    [Column("FINISH_DATE")]
    public DateOnly? FinishDate { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    [NotMapped]
    public string? ThumbnailBase64 => Thumbail != null ? Convert.ToBase64String(Thumbail) : null;
}
