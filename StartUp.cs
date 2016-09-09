using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
namespace HelloWorld
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context => context.Response.WriteAsync("Hello world"));
        }
    }
}