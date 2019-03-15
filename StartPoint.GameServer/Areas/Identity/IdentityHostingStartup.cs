using System;
using DAL;
using DomainEntities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(StartPoint.GameServer.Areas.Identity.IdentityHostingStartup))]
namespace StartPoint.GameServer.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ApiDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration["DefaultConnection:ConnectionString"]));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<ApiDbContext>();
            });
        }
    }
}