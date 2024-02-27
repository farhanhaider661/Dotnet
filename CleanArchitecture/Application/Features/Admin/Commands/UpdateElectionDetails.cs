using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admin.Commands
{
    public class UpdateElectionDetailsCommand : IRequest<bool>
    {
        public int ElectionId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PositionsContested { get; set; } = null!;

        public class UpdateElectionDetailsCommandHandler : IRequestHandler<UpdateElectionDetailsCommand, bool>
        {
            private readonly IVotingSystemDbContext _context;

            public UpdateElectionDetailsCommandHandler(IVotingSystemDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(UpdateElectionDetailsCommand request, CancellationToken cancellationToken)
            {
                var election = await _context.Elections.FindAsync(request.ElectionId);

                if (election == null) return false;

                election.Title = request.Title;
                election.StartDate = request.StartDate;
                election.EndDate = request.EndDate;
                election.PositionsContested = request.PositionsContested;

                await _context.SaveChangesAsync();

                return true;
            }
        }
    }

}
