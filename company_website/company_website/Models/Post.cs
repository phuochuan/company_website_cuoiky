using System;
using System.Collections.Generic;

namespace company_website.Models;

public partial class Post
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public int? UserAccountId { get; set; }

    public string? Title { get; set; }

    public byte[]? Thumbnail { get; set; }

    public string? Content { get; set; }

    public virtual Category? Category { get; set; }

    public virtual UserAccount? UserAccount { get; set; }
}
