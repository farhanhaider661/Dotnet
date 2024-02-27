using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authenication.Commands
{
    public class RegisterUserCommand: IRequest<bool>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime RegisterationDate  { get; set; }
    }
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IVotingSystemDbContext context;
        public RegisterUserCommandHandler(IVotingSystemDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> Handle(RegisterUserCommand request,CancellationToken cancellationToken)
        {
            var user = new User()
            {
                Username = request.Username,
                Password = request.Password,
                EmailAddress = request.Email,
                Role = request.Role,
                RegistrationDate = request.RegisterationDate
            };
            context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
