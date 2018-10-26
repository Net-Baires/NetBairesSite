using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(NetBaires.Areas.Identity.IdentityHostingStartup))]
namespace NetBaires.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}