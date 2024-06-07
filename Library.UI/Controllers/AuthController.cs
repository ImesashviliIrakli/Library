using Library.UI.Interfaces;
using Library.UI.Models.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace Library.UI.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly ITokenProvider _tokenProvider;
    public AuthController(IAuthService authService, ITokenProvider tokenProvider)
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

        if (login != null && login.IsSuccess)
        {
            string result = Convert.ToString(login.Result);

            LoginResponseDto loginResponseDto = JsonSerializer.Deserialize<LoginResponseDto>(result);

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
        ResponseDto register = await _authService.RegisterAsync(registrationRequestDto);

        if (register != null && register.IsSuccess)
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

        var jwt = handler.ReadJwtToken(loginResponseDto.Token);

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub).Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name).Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.FamilyName, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.FamilyName).Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.GivenName, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.GivenName).Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.UniqueName, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.UniqueName).Value));
        identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value));
        identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(x => x.Type == "role").Value));

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("PortalCookie", principal);
    }
}

