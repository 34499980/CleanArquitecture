using NetCore7.Core;
using NetCore7.Core.Services.Contracts.Security;
using System.IdentityModel.Tokens.Jwt;

namespace NetCore7.API.Middleware
{
    public class TokenRefreshMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IContextProvider _contextProvider;

        public TokenRefreshMiddleware(RequestDelegate next, IContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAuthService tokenService)
        {
            var token = context.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
            if (token != null)
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                if (jwtTokenHandler.CanReadToken(token))
                {
                    var jwtToken = jwtTokenHandler.ReadJwtToken(token);
                    var expDate = jwtToken.ValidTo;
                    var currentDate = DateTime.UtcNow;

                    // Si el token sigue siendo válido pero está cerca de expirar (ej. menos de 5 minutos),
                    // renovamos el token
                    if (expDate.Subtract(currentDate).TotalMinutes <= 5)
                    {
                        // Aquí puedes obtener los datos del usuario que ya están en el token y generar un nuevo token
                        var newToken = tokenService.GenerateToken(_contextProvider.UserId, _contextProvider.Email); // Pasa los datos adecuados del usuario
                        context.Response.Headers.Add("New-Token", newToken);
                    }
                }
            }

            await _next(context);
        }
    }

}
