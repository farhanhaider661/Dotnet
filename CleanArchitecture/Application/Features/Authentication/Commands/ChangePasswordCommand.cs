using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authenication.Commands
{
    public class ChangePasswordCommand: IRequest<bool>
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }
    }
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IVotingSystemDbContext _context;

        public ChangePasswordCommandHandler(IVotingSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.UserId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Password = request.NewPassword;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
