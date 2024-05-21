using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace company_website.Models;

public partial class Post
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public int? UserAccountId { get; set; }

    public string? Title { get; set; }

    public byte[]? Thumbnail { get; set; }

    public string? Content { get; set; }
    public string? Abstract { get; set; }
    //CREATE_DATE
    [Column("CREATE_DATE")]
    public DateOnly? CreateDate { get; set; }

    [Column("MODIFY_DATE")]
    public DateOnly? ModifyDate { get; set; }
    public virtual Category? Category { get; set; }

    public virtual UserAccount? UserAccount { get; set; }

    [NotMapped]
    public string? ThumbnailBase64 => Thumbnail != null ? Convert.ToBase64String(Thumbnail) : null;
}
