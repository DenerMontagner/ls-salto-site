using LSalto.Application.UseCases.Anuncios.Criar;
using LSalto.Application.UseCases.Anuncios.ListAnuncios;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LSalto.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AnunciosController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ListAnunciosQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Anciao")]
    public async Task<IActionResult> Criar([FromBody] CriarAnuncioCommand command, CancellationToken cancellationToken)
    {
        var id = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(Listar), new { id }, new { id });
    }
}
