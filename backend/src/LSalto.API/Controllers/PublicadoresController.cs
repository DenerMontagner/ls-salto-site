using LSalto.Application.UseCases.Publicadores.Atualizar;
using LSalto.Application.UseCases.Publicadores.AtribuirCargo;
using LSalto.Application.UseCases.Publicadores.AtribuirPrivilegio;
using LSalto.Application.UseCases.Publicadores.Criar;
using LSalto.Application.UseCases.Publicadores.Excluir;
using LSalto.Application.UseCases.Publicadores.ListPorTipo;
using LSalto.Application.UseCases.Publicadores.ListTodos;
using LSalto.Application.UseCases.Publicadores.RemoverCargo;
using LSalto.Application.UseCases.Publicadores.RemoverPrivilegio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LSalto.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PublicadoresController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Anciao")]
    public async Task<IActionResult> ListarTodos(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ListPublicadoresQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("elegiveis")]
    public async Task<IActionResult> ListarElegiveis([FromQuery] int tipoDesignacaoId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ListPublicadoresPorTipoQuery(tipoDesignacaoId), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Anciao")]
    public async Task<IActionResult> Criar([FromBody] CriarPublicadorCommand command, CancellationToken cancellationToken)
    {
        var id = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(ListarTodos), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Anciao")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarPublicadorCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id) return BadRequest("Id da rota diferente do body.");
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Anciao")]
    public async Task<IActionResult> Excluir(int id, CancellationToken cancellationToken)
    {
        await mediator.Send(new ExcluirPublicadorCommand(id), cancellationToken);
        return NoContent();
    }

    // --- Cargo ---

    [HttpPost("{id:int}/cargo")]
    [Authorize(Roles = "Anciao")]
    public async Task<IActionResult> AtribuirCargo(int id, [FromBody] AtribuirCargoDto dto, CancellationToken cancellationToken)
    {
        await mediator.Send(new AtribuirCargoCommand(id, dto.IdCargo, dto.DataInicio), cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}/cargo")]
    [Authorize(Roles = "Anciao")]
    public async Task<IActionResult> RemoverCargo(int id, CancellationToken cancellationToken)
    {
        await mediator.Send(new RemoverCargoCommand(id), cancellationToken);
        return NoContent();
    }

    // --- Privilégio ---

    [HttpPost("{id:int}/privilegio")]
    [Authorize(Roles = "Anciao")]
    public async Task<IActionResult> AtribuirPrivilegio(int id, [FromBody] AtribuirPrivilegioDto dto, CancellationToken cancellationToken)
    {
        await mediator.Send(new AtribuirPrivilegioCommand(id, dto.IdPrivilegio, dto.DataInicio), cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}/privilegio")]
    [Authorize(Roles = "Anciao")]
    public async Task<IActionResult> RemoverPrivilegio(int id, CancellationToken cancellationToken)
    {
        await mediator.Send(new RemoverPrivilegioCommand(id), cancellationToken);
        return NoContent();
    }
}

public record AtribuirCargoDto(int IdCargo, DateOnly DataInicio);
public record AtribuirPrivilegioDto(int IdPrivilegio, DateOnly DataInicio);
