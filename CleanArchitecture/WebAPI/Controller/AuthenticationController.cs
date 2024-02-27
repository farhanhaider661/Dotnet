using Application.Features.Authenication.Commands;
using Application.Features.Authenication.Queries;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator mediator;
        public AuthenticationController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateProduct(RegisterUserCommand userCommand, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(userCommand, cancellationToken);
            return Ok(result);
        }
        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdateProduct(ChangePasswordCommand passwordCommand, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(passwordCommand, cancellationToken);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProfile(int id)
        {
            var result = await mediator.Send(new GetUserProfileQuery() { Id = id });
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new CheckUserCredentialsQuery
            {
                Name = request.Username,
                Password = request.Password
            };

            var result = await mediator.Send(query);
            if (result>0)
            {
                // Assuming you have a method to generate a token or similar authentication response
                var jwtToken = GenerateJwtToken(request.Username);
                return Ok(new { Token = jwtToken, Username = request.Username, ID=result });
            }
            else
            {
                return Unauthorized("Invalid username or password.");
            }
        }

        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aP4s!v3Gh8@qJr9#kL1^mN*0wXz&F2dS")); // Use a secure key
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, username)
        // Add more claims as needed
    };

            var token = new JwtSecurityToken(
                issuer: "myAuthServer.com",
                audience: "myApplicationID",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Token expiry time
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}