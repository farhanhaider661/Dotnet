using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voter.Queries
{
    public class GetVotingHistoryQuery : IRequest<List<Vote>>
    {
        public int UserId { get; set; }
        internal class GetVotingHistoryQueryHandler : IRequestHandler<GetVotingHistoryQuery, List<Vote>>
        {
            private readonly IVotingSystemDbContext _context;

            public GetVotingHistoryQueryHandler(IVotingSystemDbContext context)
            {
                _context = context;
            }

            public async Task<List<Vote>> Handle(GetVotingHistoryQuery request, CancellationToken cancellationToken)
            {
                var votes = await _context.Votes
            .Where(v => v.UserId == request.UserId)
            .Select(v => new Vote
            {
                VoteId = v.VoteId,
                UserId = v.UserId,
                CandidateId = v.CandidateId,
                ElectionId = v.ElectionId,
                Timestamp = v.Timestamp
            })
            .ToListAsync(cancellationToken);
             return votes;
            }
        }
    }
}
