using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authenication.Queries
{
    public class CheckUserCredentialsQuery: IRequest<int>
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
    public class CheckUserCredentialsQueryHandler : IRequestHandler<CheckUserCredentialsQuery, int>
    {
        private readonly IVotingSystemDbContext _context;

        public CheckUserCredentialsQueryHandler(IVotingSystemDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CheckUserCredentialsQuery request, CancellationToken cancellationToken)
        {
            var user=await _context.Users.FirstOrDefaultAsync(u=>u.Username==request.Name && u.Password== request.Password);
            if (user != null)
            {
                return user.UserId;
            }
            else { return 0; }
            
        }
    }
}
