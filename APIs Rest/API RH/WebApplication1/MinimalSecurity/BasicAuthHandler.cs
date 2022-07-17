using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using RecursosHumanos.API.Services;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace RecursosHumanos.API.MinimalSecurity
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private IUserService _USerService;
        public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, 
                                          ILoggerFactory logger,  UrlEncoder encoder,  
                                         ISystemClock clock, IUserService userService)  : base (options, logger,encoder, clock)
        {
            _USerService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Not Authorization header found");

            bool result = false;
            string email, pass;

            try
            {
                (email, pass) = GetCredentials(AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]));

                result = _USerService.IsUser(email, pass);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail("BadRequest, error \n"+ ex.Message);
            }

            if(!result)
                return  AuthenticateResult.Fail("Invalid User or Password");

            var ticket = GenerateTicket(email);
           
            return AuthenticateResult.Success(ticket);
        }



        //generamos el ticket
        private AuthenticationTicket GenerateTicket(string email)
        {
            string nombre = _USerService.GetName(email);
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, nombre)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);

            var principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return ticket;
        }




        //recogemos las credenciales
        private (string email, string pass) GetCredentials(AuthenticationHeaderValue authHeader)
        {
            var CredentialBytes = Convert.FromBase64String(authHeader.Parameter!);

            var credentials = Encoding.UTF8.GetString(CredentialBytes).Split(new[] { ':' }, 2);

            return (credentials[0], credentials[1]);

        }
    }
}
