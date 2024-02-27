using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Candidate
{
    public int CandidateId { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string PositionRunningFor { get; set; } = null!;

    public string? ProfileDescription { get; set; }

    public string? PhotoUrl { get; set; }

    public DateTime RegistrationDate { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
