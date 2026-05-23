using LSalto.Application.UseCases.Designacoes.Criar;
using LSalto.Application.UseCases.Designacoes.Escalonar;
using LSalto.Application.UseCases.Designacoes.ListPorMes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LSalto.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DesignacoesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ListarPorMes([FromQuery] int ano, [FromQuery] int mes, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ListDesignacoesPorMesQuery(ano, mes), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Anciao")]
    public async Task<IActionResult> Criar([FromBody] CriarDesignacaoCommand command, CancellationToken cancellationToken)
    {
        var id = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(ListarPorMes), new { id }, new { id });
    }

    [HttpPost("escalonar")]
    [Authorize(Roles = "Anciao")]
    public async Task<IActionResult> Escalonar([FromBody] EscalonarMesCommand command, CancellationToken cancellationToken)
    {
        var total = await mediator.Send(command, cancellationToken);
        return Ok(new { total, mensagem = $"{total} designações criadas com sucesso." });
    }
}
