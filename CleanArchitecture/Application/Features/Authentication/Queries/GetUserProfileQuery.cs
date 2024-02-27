using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authenication.Queries
{
    public class GetUserProfileQuery: IRequest<User>
    {
        public int Id { get; set; }
    }
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, User>
    {

        private readonly IVotingSystemDbContext _context;
        public GetUserProfileQueryHandler(IVotingSystemDbContext context)
        {
            _context = context;
        }
        public async Task<User> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Users.Where(x => x.UserId == request.Id).FirstOrDefaultAsync(cancellationToken);
            return result;
        }
    }

}
