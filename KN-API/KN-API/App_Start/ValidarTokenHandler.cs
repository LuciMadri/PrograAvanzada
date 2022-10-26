using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Web;

namespace KN_API.App_Start
{
    public class ValidarTokenHandler : DelegatingHandler
    {

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string tokenRequest;

            if (!TryRetrieveToken(request, out tokenRequest))
            {
                statusCode = HttpStatusCode.Unauthorized;
                return base.SendAsync(request, cancellationToken);
            }

            try
            {
                var llave = "c3e59tjnx02ovqdd51nwjjyzmmylbdfh";
                byte[] key = Encoding.UTF8.GetBytes(llave);

                SecurityToken securityToken;
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param) =>
                    {
                        if (expires != null)
                        {
                            return expires > DateTime.UtcNow;
                        }
                        return false;
                    },
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                // COMPRUEBA LA VALIDEZ DEL TOKEN
                Thread.CurrentPrincipal = tokenHandler.ValidateToken(tokenRequest,
                                                                     validationParameters,
                                                                     out securityToken);
                HttpContext.Current.User = tokenHandler.ValidateToken(tokenRequest,
                                                                      validationParameters,
                                                                      out securityToken);

                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception)
            {
                statusCode = HttpStatusCode.InternalServerError;
            }

            return Task<HttpResponseMessage>.Factory.StartNew(() =>
                        new HttpResponseMessage(statusCode) { });
        }

        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) ||
                                              authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ?
                    bearerToken.Substring(7) : bearerToken;
            return true;
        }

    }
}