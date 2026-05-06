using Microsoft.AspNetCore.Identity;
using Task_manager.Models;

public class AuthService : IAuthService
{
    private readonly SignInManager<Users> _signInManager;

    public AuthService(SignInManager<Users> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}