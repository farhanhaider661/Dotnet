using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Election
{
    public int ElectionId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string PositionsContested { get; set; } = null!;

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
