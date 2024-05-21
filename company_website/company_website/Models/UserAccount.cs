using System;
using System.Collections.Generic;

namespace company_website.Models;

public partial class UserAccount
{
    public int Id { get; set; }

    public int? RoleId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }
    public string? status { get; set; }
    public DateTime? CreateAt { get; set; }
    public virtual Employee? Employee { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual Role? Role { get; set; }
}
