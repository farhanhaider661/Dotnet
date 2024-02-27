using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voting_Management.Queries
{
    public class CheckElectionStatusQuery : IRequest<ElectionStatusDto>
    {
        public int ElectionId { get; set; }
    }
    public class CheckElectionStatusQueryHandler : IRequestHandler<CheckElectionStatusQuery, ElectionStatusDto>
    {
        private readonly IVotingSystemDbContext _context;

        public CheckElectionStatusQueryHandler(IVotingSystemDbContext context)
        {
            _context = context;
        }

        public async Task<ElectionStatusDto> Handle(CheckElectionStatusQuery request, CancellationToken cancellationToken)
        {
            var election = await _context.Elections
                .FirstOrDefaultAsync(e => e.ElectionId == request.ElectionId, cancellationToken);

            if (election == null)
            {
                return null;
            }

            var status = new ElectionStatusDto
            {
                ElectionId = election.ElectionId,
                IsOpen = election.StartDate <= DateTime.UtcNow && election.EndDate >= DateTime.UtcNow
            };

            return status;
        }
    }
    public class ElectionStatusDto
    {
        public int ElectionId { get; set; }
        public bool IsOpen { get; set; }
    }
}
