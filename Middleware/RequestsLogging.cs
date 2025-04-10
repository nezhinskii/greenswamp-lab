using System.Text;

public class RequestLogging
{
    private readonly RequestDelegate _next;
    private readonly string _logFilePath;

    public RequestLogging(RequestDelegate next, string logFilePath)
    {
        _next = next;
        _logFilePath = logFilePath;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        var statusCode = context.Response.StatusCode;

        string logLine = string.Format(
            "[{0}] {1} {2} {3} {4}",
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            context.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
            context.Request.Method,
            context.Request.Path,
            statusCode
        );

        using (StreamWriter writer = new StreamWriter(_logFilePath, true, Encoding.UTF8))
        {
            await writer.WriteLineAsync(logLine);
        }
    }
}