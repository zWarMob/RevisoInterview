using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Tests
{
    public partial class BasicIntegrationTests
    {
        public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
        {
            public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
                : base(options, logger, encoder, clock)
            {
            }

            protected override Task<AuthenticateResult> HandleAuthenticateAsync()
            {
                //move claim values to secure string storage or store in table as testing credentials
                var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, "01fc30d0-f152-4661-a108-87d2ae5514b1"),
                    new Claim(ClaimTypes.Name, "petyo@yahoo.com"),
                    new Claim("AspNet.Identity.SecurityStamp", "HESS6IAJHWC5UWDBDRHT524JPAJ3VGLW"),
                    new Claim("amr", "pwd")
                };

                var identity = new ClaimsIdentity(claims, "Identity.Application");
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, principal.Identity.AuthenticationType);

                var result = AuthenticateResult.Success(ticket);

                return Task.FromResult(result);
            }
        }
    }
}
