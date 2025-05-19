using System.IdentityModel.Tokens.Jwt;

namespace WebAPI.Middlewares
{
    public class JwtLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JwtLoggingMiddleware> _logger;
        public JwtLoggingMiddleware(RequestDelegate next, ILogger<JwtLoggingMiddleware> logger)
        {
            _next = next; 
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                var token = authHeader.ToString();
                if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
                {
                    token = token.Substring("Bearer ".Length).Trim();

                    _logger.LogInformation("Jwt Token: {token}", token);
                }
                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(token);

                    _logger.LogInformation("JWT Claims:");
                    foreach (var claim in jwtToken.Claims)
                    {
                        _logger.LogInformation("Claim Type: {Type}, Value: {Value}", claim.Type, claim.Value);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to decode jwt token ");
                }
            }
            else
            {
                _logger.LogWarning("No authorization header found!"); 
            }
            await _next(context);
        }
    }
    public static class JwtLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UserJwtLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtLoggingMiddleware>();
        }
    }
}
