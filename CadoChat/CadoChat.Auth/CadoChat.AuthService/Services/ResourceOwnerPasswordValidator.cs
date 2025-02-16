using IdentityServer8.Models;
using IdentityServer8.Validation;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public ResourceOwnerPasswordValidator(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var user = await _userManager.FindByNameAsync(context.UserName);
        if (user != null && await _userManager.CheckPasswordAsync(user, context.Password))
        {
            context.Result = new GrantValidationResult(
                subject: user.Id,
                authenticationMethod: "password",
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                });
        }
        else
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
        }
    }
}