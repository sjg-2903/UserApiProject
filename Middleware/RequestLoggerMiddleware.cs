namespace UserApiProject.Middleware
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"[Request] {context.Request.Method} {context.Request.Path}");
            await _next(context);
        }
    }

    public static class RequestLoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggerMiddleware>();
        }
    }
}
