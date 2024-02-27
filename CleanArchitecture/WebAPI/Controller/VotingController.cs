using Application.Features.Voter.Commands;
using Application.Features.Voter.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VotingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("availableCandidates/{id}")]
        public async Task<IActionResult> GetAvailableCandidates(int id)
        {
            var candidates = await _mediator.Send(new GetVotingHistoryQuery(){ UserId = id });
            return Ok(candidates);
        }
        [HttpPost]
        public async Task<IActionResult> CastVote(CastVoteCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Unable to cast vote.");
            }
        }
    }
}
