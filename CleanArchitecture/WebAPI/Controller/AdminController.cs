using Application.Features.Admin.Commands;
using Application.Features.Admin.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddCandidate")]
        public async Task<IActionResult> AddCandidate([FromBody] AddCandidateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result); // result might be the ID of the newly added candidate
        }

        [HttpPost("AddElection")]
        public async Task<IActionResult> AddElection(AddElectionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result); // result might be the ID of the newly added election
        }

        [HttpDelete("RemoveCandidate/{candidateId}")]
        public async Task<IActionResult> RemoveCandidate(int candidateId)
        {
            var command = new RemoveCandidateCommand { CandidateId = candidateId };
            var result = await _mediator.Send(command);
            return Ok(result); // result might be a boolean indicating success
        }

        [HttpPut("UpdateElectionDetails")]
        public async Task<IActionResult> UpdateElectionDetails(UpdateElectionDetailsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result); // result might be a boolean indicating success
        }

        [HttpGet("GetVotingStatistics")]
        public async Task<IActionResult> GetVotingStatistics()
        {
            var query = new GetVotingStatisticsQuery(); // Add any necessary properties to the query
            var result = await _mediator.Send(query);
            return Ok(result); // result would be the voting statistics
        }

    }
}
