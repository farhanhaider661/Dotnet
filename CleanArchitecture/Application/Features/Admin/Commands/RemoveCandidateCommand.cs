using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admin.Commands
{
    public class RemoveCandidateCommand : IRequest<bool>
    {
        public int CandidateId { get; set; }
    }

    public class RemoveCandidateCommandHandler : IRequestHandler<RemoveCandidateCommand, bool>
    {
        private readonly IVotingSystemDbContext _context;

        public RemoveCandidateCommandHandler(IVotingSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(RemoveCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _context.Candidates.FindAsync(request.CandidateId);

            if (candidate == null)
            {
                return false;
            }

            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();

            return true; // Return true if the candidate was removed successfully
        }
    }

}
