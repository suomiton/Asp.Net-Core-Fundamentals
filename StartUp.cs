using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
namespace HelloWorld
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            this.ConfigureMapping(app);
            //app.Run(context => context.Response.WriteAsync("Hello world"));

            app.Use(async (context, next) =>
            {
                await FirstResponse(context);
                await next.Invoke();
            });

            app.Run(async context =>
            {
                await SecondResponse(context);
            });
        }

        public static async Task FirstResponse(HttpContext context)
        {
            await context.Response.WriteAsync("Hello from the first handler<br />");
        }
        public static async Task SecondResponse(HttpContext context)
        {
            await context.Response.WriteAsync("Hello from the second handler");
        }

        private static void HandleMapTest(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test Successful");
            });
        }

        public void ConfigureMapping(IApplicationBuilder app)
        {
            app.Map("/test", HandleMapTest);
        }
    }
}