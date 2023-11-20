using wrssolutions.Domain.Entities.Auth;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using IdentityServer4.Extensions;

namespace wrssolutions.IoC.Identity
{
    public class IdentityWithAdditionalClaimsProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityWithAdditionalClaimsProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();

            var user = await _userManager.FindByIdAsync(sub);

            if (user == null)
                return;

            var principal = await _claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();

            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            if (!string.IsNullOrEmpty(user.UserName))
                claims.Add(new Claim(JwtClaimTypes.GivenName, user.UserName));

            claims.Add(new Claim(JwtClaimTypes.Role, "dataEventRecords.user"));
            claims.Add(new Claim(JwtClaimTypes.Role, "dataEventRecords"));
            claims.Add(new Claim(JwtClaimTypes.Role, "securedFiles.user"));
            claims.Add(new Claim(JwtClaimTypes.Role, "securedFiles"));

            if (!string.IsNullOrEmpty(user.UserName))
                claims.Add(new Claim(JwtClaimTypes.GivenName, user.UserName));

            if (user.IsAdmin)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "admin"));
            }
            else
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "user"));
            }

            if (user.TwoFactorEnabled)
            {
                claims.Add(new Claim("amr", "mfa"));
            }
            else
            {
                claims.Add(new Claim("amr", "pwd")); ;
            }

            if (!string.IsNullOrEmpty(user.Email))
                claims.Add(new Claim(IdentityServerConstants.StandardScopes.Email, user.Email));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
