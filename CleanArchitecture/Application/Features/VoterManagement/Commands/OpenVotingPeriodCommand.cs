using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voting_Management.Commands
{
    public class OpenVotingPeriodCommand : IRequest<bool>
    {
        public int ElectionId { get; set; }
    }

    public class OpenVotingPeriodCommandHandler : IRequestHandler<OpenVotingPeriodCommand, bool>
    {
        private readonly IVotingSystemDbContext _context;

        public OpenVotingPeriodCommandHandler(IVotingSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(OpenVotingPeriodCommand request, CancellationToken cancellationToken)
        {
            var election = await _context.Elections.FindAsync(request.ElectionId);
            if (election == null) return false;

            election.StartDate = DateTime.UtcNow; // Open the voting period by setting the start date to now
            await _context.SaveChangesAsync();

            return true;
        }
    }


}
