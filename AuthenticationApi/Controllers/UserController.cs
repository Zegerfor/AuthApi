using AuthenticationApi.Dtos;
using AuthenticationApi.Extensions;
using AuthenticationApi.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace AuthenticationApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public UserController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        Result<string> result;
        if (!User.Identity.IsAuthenticated)
        {
            result = await _authenticationService.Login(request);
        }
        else
        {
            result = Result.Fail("Пользователь уже авторизован");
        }
        return GetResult(result);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _authenticationService.Register(request);
        return GetResult(result);
    }

    private IActionResult GetResult(Result<string> result)
    {
        var ResultDto = result.ToResultDto();

        if (!ResultDto.IsSuccess)
        {
            return BadRequest(ResultDto);
        }

        return Ok(ResultDto);
    }
}