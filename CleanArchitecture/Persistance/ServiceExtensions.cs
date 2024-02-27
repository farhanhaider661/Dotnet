using Application.Interfaces;
using Domain.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public static class ServiceExtensions
    {
        public static void AddPersistance(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<VotingSystemDbContext>(option => option.UseSqlServer(
                config.GetConnectionString("DefaultConnection")
                ));
            services.AddScoped<IVotingSystemDbContext, VotingSystemDbContext>();
        }

    }
}
