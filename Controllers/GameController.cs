namespace FishkiAC.Controllers;

using FishkiAC.Handlers.Game;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/Game")]
public class GameController : ControllerBase
{
    private readonly IMediator _mediator;

    public GameController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetRandomFlashcards([FromQuery] Guid DeckId, [FromQuery] int amount)
    {
        var result = await _mediator.Send(new GetRandomFlashcardsCommand { DeckId = DeckId, Amount = amount });
        return result;
    }
}
