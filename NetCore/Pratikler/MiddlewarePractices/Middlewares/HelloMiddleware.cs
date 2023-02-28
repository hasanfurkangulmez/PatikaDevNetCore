using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MiddlewarePractices.Middlewares
{//https://app.patika.dev/courses/net-core/1-middleware-nedir
    public class HelloMiddleware
    {
        private readonly RequestDelegate _next;
        public HelloMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            System.Console.WriteLine("Hello World");
            await _next.Invoke(context);
            System.Console.WriteLine("Bye World!");
        }
    }
    static public class HelloMiddlewareExtension
    {
        public static IApplicationBuilder UseHello(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HelloMiddleware>();
        }
    }
}