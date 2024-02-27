using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admin.Queries
{
    public class GetVotingStatisticsQuery : IRequest<VotingStatisticsDto>
    {
        // Add properties to filter statistics as needed

        public class GetVotingStatisticsQueryHandler : IRequestHandler<GetVotingStatisticsQuery, VotingStatisticsDto>
        {
            private readonly IVotingSystemDbContext _context;

            public GetVotingStatisticsQueryHandler(IVotingSystemDbContext context)
            {
                _context = context;
            }

            public async Task<VotingStatisticsDto> Handle(GetVotingStatisticsQuery request, CancellationToken cancellationToken)
            {
                var statistics = new VotingStatisticsDto
                {
                    TotalVotes = await _context.Votes.CountAsync(),
                };

                return statistics;
            }
        }
    }

    public class VotingStatisticsDto
    {
        public int TotalVotes { get; set; }
        // Add other statistics as needed
    }

}
