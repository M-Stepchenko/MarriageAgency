using System;
using System.Collections.Generic;

namespace MarriageAgency.Models;

public partial class Employee
{
    public int Id { get; set; }
    //public int? UserId { get; set; }

    public string? Name { get; set; }

    public string? Position { get; set; }

    public DateTime? Bithdate { get; set; }

    public virtual ICollection<ProvidedService> ProvidedServices { get; set; } = new List<ProvidedService>();

    //public virtual User? User { get; set; }
}
