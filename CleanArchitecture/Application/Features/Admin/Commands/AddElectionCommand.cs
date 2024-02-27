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
    public class AddElectionCommand : IRequest<int>
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PositionsContested { get; set; }
    }

    public class AddElectionCommandHandler : IRequestHandler<AddElectionCommand, int>
    {
        private readonly IVotingSystemDbContext _context;

        public AddElectionCommandHandler(IVotingSystemDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(AddElectionCommand request, CancellationToken cancellationToken)
        {
            var election = new Election
            {
                Title = request.Title,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                PositionsContested = request.PositionsContested,
            };

            _context.Elections.Add(election);
            await _context.SaveChangesAsync();

            return election.ElectionId; // Return the ID of the newly added election
        }
    }

}
