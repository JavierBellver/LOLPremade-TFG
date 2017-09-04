using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using System.Text;
using lolpremade.Utils;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Principal;
using lolpremade.Utils.CustomTokenAuthProvider;
using lolpremade.Data;
using lolpremade.DAL;
using lolpremade.Models;

namespace lolpremade
{
    public partial class Startup
    {
        public SymmetricSecurityKey signupKey;
        private LolpremadeContext context;

        private void ConfigureAuth(IApplicationBuilder app, LolpremadeContext _context)
        {
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("TokenAuthentication:SecretKey").Value));
            context = _context;

            var tokenProviderOptions = new TokenProviderOptions
            {
                Path = Configuration.GetSection("TokenAuthentication:TokenPath").Value,
                Audience = Configuration.GetSection("TokenAuthentication:Audience").Value,
                Issuer = Configuration.GetSection("TokenAuthentication:Issuer").Value,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                IdentityResolver = GetIdentity
            };

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = Configuration.GetSection("TokenAuthentication:Issuer").Value,
                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = Configuration.GetSection("TokenAuthentication:Audience").Value,
                // Validate the token expiry
                ValidateLifetime = true,
                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(tokenProviderOptions));
        }

        private Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            string hashedPassword = password;
            UnitOfWork unitofWork = new UnitOfWork(context);
            IEnumerable<User> _userToFindList = new List<User>();

            _userToFindList = unitofWork.UserRepository.Get((q => q.Username == username));

            foreach(User u in _userToFindList)
            {
                hashedPassword = CryptoUtils.HashWithSHA256(password, u.Salt);
                if (u.Username == username && u.Password == hashedPassword)
                {
                    return Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }));
                }
            }

            // Account doesn't exists
            return Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
