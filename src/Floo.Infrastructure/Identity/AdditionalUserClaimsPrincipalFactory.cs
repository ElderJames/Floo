using Floo.Core.Entities.Identity;
using Floo.Core.Entities.Identity.Users;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Floo.Infrastructure.Identity
{
	public class AdditionalUserClaimsPrincipalFactory
		 : UserClaimsPrincipalFactory<User, Role>
	{
		public AdditionalUserClaimsPrincipalFactory(
			UserManager<User> userManager,
			RoleManager<Role> roleManager,
			IOptions<IdentityOptions> optionsAccessor)
			: base(userManager, roleManager, optionsAccessor)
		{ }

		public async override Task<ClaimsPrincipal> CreateAsync(User user)
		{
			var principal = await base.CreateAsync(user);
			var identity = (ClaimsIdentity)principal.Identity;

			var claims = new List<Claim>();

            claims.Add(new Claim(JwtClaimTypes.Name, user.UserName));

            identity.AddClaims(claims);
			return principal;
		}
	}
}
