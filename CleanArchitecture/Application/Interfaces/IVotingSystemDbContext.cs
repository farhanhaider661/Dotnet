using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IVotingSystemDbContext
    {
        DbSet<Candidate> Candidates { get; set; }

        DbSet<Election> Elections { get; set; }

        DbSet<User> Users { get; set; }

        DbSet<Vote> Votes { get; set; }
        Task<int> SaveChangesAsync();
    }
}
