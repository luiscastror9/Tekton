using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tekton.Domain.Base;

namespace Tekton.API.Base
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public RequestMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger(typeof(RequestMiddleware));
        }

        public async Task Invoke(HttpContext context)
        {
            

        Guid traceId = Guid.NewGuid();
            _logger.LogDebug($"Request {traceId} iniciada");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();


            await _next(context);

            //Despues de la request
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            _logger.LogDebug($"La request {traceId} ha llevado {elapsedTime} ");
            LogWriter.LogWrite($"La request {traceId} ha llevado {elapsedTime} ");
        }
    }
    public static class RequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware< RequestMiddleware> ();
        }
    }
}
