using Application.Trucks;
using Microsoft.AspNetCore.Mvc;

namespace viatrux.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class TrucksController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        return HandleResult(await Mediator.Send(new GetAll.Query(), ct));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id, CancellationToken ct)
    {
        return HandleResult(await Mediator.Send(new Get.Query() { Id = id }, ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create(Add.Command command, CancellationToken ct)
    {
        return HandleResult(await Mediator.Send(command, ct));
    }

    [HttpPut]
    public async Task<IActionResult> Update(Edit.Command command, CancellationToken ct)
    {
        return HandleResult(await Mediator.Send(command, ct));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Delete.Command command, CancellationToken ct)
    {
        return HandleResult(await Mediator.Send(command, ct));
    }
}
