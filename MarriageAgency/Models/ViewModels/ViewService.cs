using System;
using System.Collections.Generic;

namespace MarriageAgency.Models;

public partial class ViewService
{
    public DateTime? Date { get; set; }

    public string? ServiceName { get; set; }

    public string? Employee { get; set; }

    public string? Client { get; set; }

    public int? Cost { get; set; }
}
