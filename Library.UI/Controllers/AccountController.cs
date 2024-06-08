using Library.UI.Interfaces;
using Library.UI.Models.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace Library.UI.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _authService;
    private readonly ITokenProvider _tokenProvider;
    public AccountController(IAccountService authService, ITokenProvider tokenProvider)
    {
        _authService = authService;
        _tokenProvider = tokenProvider;
    }

    [HttpGet]
    public IActionResult Login()
    {
        LoginRequestDto loginRequestDto = new();
        return View(loginRequestDto);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
    {
        ResponseDto login = await _authService.LoginAsync(loginRequestDto);

        if (login != null && login.Status == 0)
        {
            string result = Convert.ToString(login.Result);

            LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(result);

            await SignInAsync(loginResponseDto);

            _tokenProvider.SetToken(loginResponseDto.Token);

            return RedirectToAction("Index", "Home");
        }
        else
        {
            TempData["error"] = login.Message;
            return View(loginRequestDto);
        }
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
    {
        registrationRequestDto.UserName = registrationRequestDto.Email;
        ResponseDto register = await _authService.RegisterAsync(registrationRequestDto);

        if (register != null && register.Status == 0)
        {
            TempData["success"] = "Registration was successful";

            return RedirectToAction(nameof(Login));
        }
        else
        {
            TempData["error"] = register.Message;
        }

        return View(registrationRequestDto);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        _tokenProvider.ClearToken();

        return RedirectToAction("Index", "Home");
    }

    private async Task SignInAsync(LoginResponseDto loginResponseDto)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(loginResponseDto.Token);

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

        // Helper method to safely add claims
        void AddClaimIfPresent(string claimType, string jwtClaimType)
        {
            var claimValue = jwtToken.Claims.FirstOrDefault(x => x.Type == jwtClaimType)?.Value;
            if (!string.IsNullOrEmpty(claimValue))
            {
                identity.AddClaim(new Claim(claimType, claimValue));
            }
        }

        AddClaimIfPresent(ClaimTypes.Email, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
        AddClaimIfPresent(ClaimTypes.Name, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
        AddClaimIfPresent(ClaimTypes.NameIdentifier, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("LibraryCookie", principal);
    }

}

