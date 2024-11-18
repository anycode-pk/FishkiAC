namespace FishkiAC.Controllers;

using FishkiAC.DTOs;
using FishkiAC.Handlers.Deck;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/Deck")]
public class DeckController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeckController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("ById")]
    public async Task<IActionResult> GetById([FromQuery] GetDeckByIdQuery query)
    {
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet]
    public async Task<IEnumerable<DeckDto>> GetAll()
    {
        var result = await _mediator.Send(new GetAllDecksQuery());
        return result;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateDeckCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPut]
    public async Task<IActionResult> Put(Guid Id, [FromBody] DeckInDto deckInDto)
    {
        var result = await _mediator.Send(new UpdateDeckCommand { Id = Id, deckInDto = deckInDto });
        return result;
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(Guid Id)
    {
        var result = await _mediator.Send(new DeleteDeckCommand { Id = Id });
        return result;
    }
}
