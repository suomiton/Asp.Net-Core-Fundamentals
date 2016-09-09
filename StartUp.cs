using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace HelloWorld
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseTimer();

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = new PathString("/images"),
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"images"))
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                RequestPath = new PathString("/images"),
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"images"))
            });

            app.Run(context => context.Response.WriteAsync("Hello world"));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDirectoryBrowser();
        }
    }
}