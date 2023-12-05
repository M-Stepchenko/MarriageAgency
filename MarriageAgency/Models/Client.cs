using System;
using System.Collections.Generic;

namespace MarriageAgency.Models;

public partial class Client
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public string? Name { get; set; }

    public string? Sex { get; set; }

    public string? Job { get; set; }

    public DateTime? Bithdate { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }

    public int? Children { get; set; }

    public string? BadHabbits { get; set; }

    public string? Hobby { get; set; }

    public int? ZodiacSignId { get; set; }

    public int? NationalityId { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Passport { get; set; }

    public string? DesiredPartner { get; set; }

    public string? Photo { get; set; }

    public int? FamilyStatusId { get; set; }

    public virtual FamilyStatus? FamilyStatus { get; set; }

    public virtual Nationality? Nationality { get; set; }
    
    public virtual User? User { get; set; }

    public virtual ICollection<ProvidedService> ProvidedServices { get; set; } = new List<ProvidedService>();

    public virtual ZodiacSign? ZodiacSign { get; set; }
}
