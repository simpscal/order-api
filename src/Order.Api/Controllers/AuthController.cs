using MediatR;

using Microsoft.AspNetCore.Mvc;

using Order.Application.Auth.Commands.Register;
using Order.Application.Auth.Queries.Login;

namespace Order.Api.Controllers;

[Route("api/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public Task<LoginDto> Login([FromBody] LoginQuery request)
    {
        return mediator.Send(request);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand request)
    {
        await mediator.Send(request);

        return Ok();
    }
}