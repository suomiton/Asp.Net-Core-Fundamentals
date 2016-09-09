using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HelloWorld
{
    public class TimerMiddleware
    {
        private readonly RequestDelegate _next;
        public TimerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            Stopwatch timer = Stopwatch.StartNew();

            context.Response.OnStarting(state =>
            {
                timer.Stop();
                context.Response.Headers.Add("x-timer", $"{timer.ElapsedMilliseconds} ms");
                return Task.FromResult(0);
            }, null);

            await _next.Invoke(context);
        }
    }

    public static class TimerMiddlewareExtensions
    {
        public static IApplicationBuilder UseTimer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TimerMiddleware>();
        }
    }
}
