using FishkiAC.Context;
using FishkiAC.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FishkiAC.Handlers.Deck;

public sealed record UpdateDeckCommand() : IRequest<IActionResult>
{
    public required Guid Id { get; init; }
    public required DeckInDto deckInDto { get; init; }
}

public class UpdateDeckCommandHandler : IRequestHandler<UpdateDeckCommand, IActionResult>
{
    private readonly ApplicationDbContext _context;

    public UpdateDeckCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Handle(UpdateDeckCommand request, CancellationToken cancellationToken)
    {
        var deck = await _context.Decks.FindAsync(request.Id);

        if (deck == null)
        {
            return new NotFoundObjectResult(request.Id);
        }

        deck.Name = request.deckInDto.Name;

        try
        {
            await _context.SaveChangesAsync();
            return new OkObjectResult(deck);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }
}
