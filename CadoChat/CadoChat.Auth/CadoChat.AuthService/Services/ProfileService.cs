using IdentityServer8.Extensions;
using IdentityServer8.Models;
using IdentityServer8.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

public class ProfileService : IProfileService
{
    private readonly UserManager<IdentityUser> _userManager;

    public ProfileService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            context.IssuedClaims.AddRange(claims);
        }
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var user = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());
        context.IsActive = user != null;
    }
}
