using MediatR;

using Microsoft.AspNetCore.Mvc;

using Order.Application.Auth.Queries;

namespace Order.Api.Controllers;

[Route("api/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<LoginDto> Login(LoginQuery request)
    {
        var result = await mediator.Send(request);

        return result;
    }
}