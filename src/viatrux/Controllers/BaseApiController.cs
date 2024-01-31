using Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace viatrux.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result is null)
        {
            return NotFound();
        }

        if (result.IsSuccess && result.Value is not null)
        {
            return Ok(result.Value);
        }

        if (result.IsSuccess && result.Value is null)
        {
            return NotFound();
        }

        return BadRequest(result.Error);
    }
}