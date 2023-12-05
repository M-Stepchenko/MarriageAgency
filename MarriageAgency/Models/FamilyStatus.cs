using System;
using System.Collections.Generic;

namespace MarriageAgency.Models;

public partial class FamilyStatus
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
