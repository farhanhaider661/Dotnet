using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Candidates.Queries
{
    public class GetCandidateDetailsQuery : IRequest<Candidate>
    {
        public int CandidateId { get; set; }
    }

    public class GetCandidateDetailsQueryHandler : IRequestHandler<GetCandidateDetailsQuery, Candidate>
    {
        private readonly IVotingSystemDbContext _context;

        public GetCandidateDetailsQueryHandler(IVotingSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Candidate> Handle(GetCandidateDetailsQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _context.Candidates
                .Where(c => c.CandidateId == request.CandidateId)
                .Select(c => new Candidate
                {
                    CandidateId = c.CandidateId,
                    UserId = c.UserId,
                    Name = c.Name,
                    PositionRunningFor = c.PositionRunningFor,
                    // ... map other properties to the DTO
                })
                .SingleOrDefaultAsync(cancellationToken);

            if (candidate == null)
            {
                return null;
            }

            return candidate;
        }
    }
}
