using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
