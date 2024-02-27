using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voting_Management.Commands
{
    public class CloseVotingPeriodCommand : IRequest<bool>
    {
        public int ElectionId { get; set; }
    }

    public class CloseVotingPeriodCommandHandler : IRequestHandler<CloseVotingPeriodCommand, bool>
    {
        private readonly IVotingSystemDbContext _context;

        public CloseVotingPeriodCommandHandler(IVotingSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CloseVotingPeriodCommand request, CancellationToken cancellationToken)
        {
            var election = await _context.Elections.FindAsync(request.ElectionId);
            if (election == null) return false;

            election.EndDate = DateTime.UtcNow; // Close the voting period by setting the end date to now
            await _context.SaveChangesAsync();

            return true;
        }
    }

}
