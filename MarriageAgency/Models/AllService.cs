using System;
using System.Collections.Generic;

namespace MarriageAgency.Models;

public partial class AllService
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? Cost { get; set; }

    public virtual ICollection<ProvidedService> ProvidedServices { get; set; } = new List<ProvidedService>();
}
