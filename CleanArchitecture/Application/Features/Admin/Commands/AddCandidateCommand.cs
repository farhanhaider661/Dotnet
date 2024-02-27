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
    public class AddCandidateCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PositionRunningFor { get; set; }
        public string ProfileDescription { get; set; }

        public string PhotoUrl { get; set; }

        public DateTime RegistrationDate { get; set; }
    }

    public class AddCandidateCommandHandler : IRequestHandler<AddCandidateCommand, int>
    {
        private readonly IVotingSystemDbContext _context;

        public AddCandidateCommandHandler(IVotingSystemDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(AddCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = new Candidate
            {
                UserId = request.UserId,
                Name = request.Name,
                PositionRunningFor = request.PositionRunningFor,
                ProfileDescription = request.ProfileDescription,
                PhotoUrl = request.PhotoUrl,
                RegistrationDate = request.RegistrationDate,
            };

            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();

            return candidate.CandidateId;
        }
    }

}
