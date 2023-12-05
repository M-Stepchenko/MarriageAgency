using System;
using System.Collections.Generic;

namespace MarriageAgency.Models;

public partial class Staff
{
    public string? Name { get; set; }

    public string? Position { get; set; }

    public int? Age { get; set; }

    public int? CountOfProvidedServices { get; set; }
}
