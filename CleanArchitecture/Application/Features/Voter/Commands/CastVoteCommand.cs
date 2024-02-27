using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voter.Commands
{
    public class CastVoteCommand: IRequest<bool>
    {
        public int UserId { get; set; }
        public int CandidateId { get; set; }
        public int ElectionId { get; set; }
        public DateTime Timestamp { get; set; }
    }
    public class CastVoteCommandHandler : IRequestHandler<CastVoteCommand, bool>
    {
        private readonly IVotingSystemDbContext _context;

        public CastVoteCommandHandler(IVotingSystemDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(CastVoteCommand request, CancellationToken cancellationToken)
        {
            var vote = new Vote
            {
                UserId = request.UserId,
                CandidateId = request.CandidateId,
                ElectionId = request.ElectionId,
                Timestamp = DateTime.UtcNow // Record the time the vote was cast
            };
            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
