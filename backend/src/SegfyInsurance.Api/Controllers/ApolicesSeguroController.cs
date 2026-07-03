using Microsoft.AspNetCore.Mvc;
using SegfyInsurance.Application.UseCases.Apolices.AtualizarApolice;
using SegfyInsurance.Application.UseCases.Apolices.BuscarApolicePorId;
using SegfyInsurance.Application.UseCases.Apolices.CancelarApolice;
using SegfyInsurance.Application.UseCases.Apolices.CriarApolice;
using SegfyInsurance.Application.UseCases.Apolices.ListarApolices;
using SegfyInsurance.Application.UseCases.Apolices.ListarApolicesVencendoProximos30Dias;
using SegfyInsurance.Application.UseCases.Apolices.RemoverApolice;

namespace SegfyInsurance.Api.Controllers;

[ApiController]
[Route("api/apolices-seguro")]
public class ApolicesSeguroController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CriarApoliceResponse>> Criar(
        [FromBody] CriarApoliceRequisicao requisicao,
        [FromServices] CriarApoliceUseCase useCase,
        CancellationToken cancellationToken)
    {
        var resposta = await useCase.ExecutarAsync(requisicao, cancellationToken);

        return CreatedAtAction(nameof(BuscarPorId), new { id = resposta.Id }, resposta);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ListarApolicesResponse>>> Listar(
        [FromServices] ListarApolicesUseCase useCase,
        CancellationToken cancellationToken)
    {
        var resposta = await useCase.ExecutarAsync(cancellationToken);

        return Ok(resposta);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BuscarApolicePorIdResponse>> BuscarPorId(
        Guid id,
        [FromServices] BuscarApolicePorIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var resposta = await useCase.ExecutarAsync(id, cancellationToken);

        return resposta is null ? NotFound() : Ok(resposta);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(
        Guid id,
        [FromBody] AtualizarApoliceRequisicao requisicao,
        [FromServices] AtualizarApoliceUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.ExecutarAsync(id, requisicao, cancellationToken);

        return NoContent();
    }

    [HttpPatch("{id:guid}/cancelar")]
    public async Task<IActionResult> Cancelar(
        Guid id,
        [FromServices] CancelarApoliceUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.ExecutarAsync(id, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(
        Guid id,
        [FromServices] RemoverApoliceUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.ExecutarAsync(id, cancellationToken);

        return NoContent();
    }

    [HttpGet("vencendo-proximos-30-dias")]
    public async Task<ActionResult<IReadOnlyList<ListarApolicesVencendoProximos30DiasResponse>>> ListarVencendoProximos30Dias(
        [FromServices] ListarApolicesVencendoProximos30DiasUseCase useCase,
        CancellationToken cancellationToken)
    {
        var resposta = await useCase.ExecutarAsync(cancellationToken);

        return Ok(resposta);
    }
}
