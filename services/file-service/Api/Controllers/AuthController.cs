using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Services;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;

    public AuthController(UserService userService)
    {
        _userService = userService;
    }

    //заменить контроллер для прокидыания AuthResult и других моделей


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        try
        {
            var user = await _userService.RegisterAsync(request.Email, request.Password);
            return Ok(user); //ждем тут возвращения с user service успешного AuthResult 
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message }); // 400
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message }); // 409
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Internal server error." }); // 500
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userService.LoginAsync(request.Email, request.Password);

        if (user == null)
            return Unauthorized("Invalid email or password.");

        return Ok(user);
    }
}

public record RegisterRequest(string Email, string Password);
public record LoginRequest(string Email, string Password);

