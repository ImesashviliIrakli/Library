﻿using Application.Enums;
using Application.Interfaces.Services;
using Application.Models;
using Application.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authenticationService;
    private ResponseModel _response;
    public AuthController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
        _response = new ResponseModel(Status.Success, "Success");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthRequest request)
    {
        _response.Result = await _authenticationService.Login(request);
        return Ok(_response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationRequest request)
    {
        _response.Result = await _authenticationService.Register(request);
        return Ok(_response);
    }
}