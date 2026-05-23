using System.Security.Claims;
using LSalto.Application.UseCases.Dashboard;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LSalto.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController(IMediator mediator) : ControllerBase
{
    [HttpGet("me")]
    public async Task<IActionResult> GetMeuDashboard(CancellationToken cancellationToken)
    {
        var idPublicador = int.Parse(User.FindFirstValue("sub")!);
        var result = await mediator.Send(new GetDashboardQuery(idPublicador), cancellationToken);
        return Ok(result);
    }
}
