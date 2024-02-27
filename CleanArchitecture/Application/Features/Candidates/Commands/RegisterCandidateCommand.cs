using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Candidates.Commands
{
    public class RegisterCandidateCommand : IRequest<int> // Returns the ID of the newly created candidate
    {
        public int UserId { get; set; } // Assuming a user can register as a candidate
        public string Name { get; set; }
        public string PositionRunningFor { get; set; }
        public string ProfileDescription { get; set; }

        public string PhotoUrl { get; set; }

        public DateTime RegistrationDate { get; set; }

    }

    public class RegisterCandidateCommandHandler : IRequestHandler<RegisterCandidateCommand, int>
    {
        private readonly IVotingSystemDbContext _context;

        public RegisterCandidateCommandHandler(IVotingSystemDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(RegisterCandidateCommand request, CancellationToken cancellationToken)
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

            return candidate.CandidateId; // Return the ID of the new candidate
        }
    }
}
