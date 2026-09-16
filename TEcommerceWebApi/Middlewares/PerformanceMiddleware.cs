using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TEcommerceWebApi.Middlewares
{
    public class PerformanceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PerformanceMiddleware> _logger;

        public PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 1. Start Stopwatch before controller runs
            var stopwatch = Stopwatch.StartNew();

            // 2. Pass request down the pipeline to the Controller
            await _next(context);

            // 3. Stop Stopwatch after controller finishes
            stopwatch.Stop();
            var elapsedMs = stopwatch.ElapsedMilliseconds;

            // 4. Log slow queries if it took longer than 500ms
            if (elapsedMs > 500)
            {
                _logger.LogWarning("⚠️ SLOW ENDPOINT: {Method} {Path} took {Elapsed}ms",
                    context.Request.Method, context.Request.Path, elapsedMs);
            }
            else
            {
                _logger.LogInformation("⚡ {Method} {Path} completed in {Elapsed}ms",
                    context.Request.Method, context.Request.Path, elapsedMs);
            }
        }
    }
}