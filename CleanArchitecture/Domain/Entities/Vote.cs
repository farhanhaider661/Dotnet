using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Vote
{
    public int VoteId { get; set; }

    public int UserId { get; set; }

    public int CandidateId { get; set; }

    public int ElectionId { get; set; }

    public DateTime Timestamp { get; set; }

    public virtual Candidate Candidate { get; set; } = null!;

    public virtual Election Election { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
