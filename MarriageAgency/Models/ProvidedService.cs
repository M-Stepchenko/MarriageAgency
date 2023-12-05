using System;
using System.Collections.Generic;

namespace MarriageAgency.Models;

public partial class ProvidedService
{
    public int Id { get; set; }

    public int? ClientId { get; set; }

    public DateTime? Date { get; set; }

    public int? ServiceId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual AllService? Service { get; set; }
}
