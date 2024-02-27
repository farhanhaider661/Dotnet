using Application.Features.Voting_Management.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotingManagementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VotingManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{electionId}/status")]
        public async Task<ActionResult<ElectionStatusDto>> CheckElectionStatus(int electionId)
        {
            var query = new CheckElectionStatusQuery { ElectionId = electionId };
            var status = await _mediator.Send(query);
            return Ok(status);
        }
    }
}
