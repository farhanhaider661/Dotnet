using Application.Features.Candidates.Commands;
using Application.Features.Candidates.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> RegisterCandidate(RegisterCandidateCommand command)
        {
            int candidateId = await _mediator.Send(command);
            return Ok(candidateId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Candidate>> GetCandidateDetails(int id)
        {
            var candidate = await _mediator.Send(new GetCandidateDetailsQuery { CandidateId = id });
            return Ok(candidate);
        }
    }
}
