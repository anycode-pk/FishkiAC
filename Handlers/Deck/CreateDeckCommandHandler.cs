namespace FishkiAC.Handlers.Deck;

using FishkiAC.Context;
using FishkiAC.DTOs;
using FishkiAC.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public sealed record CreateDeckCommand() : IRequest<IActionResult>
{
    public required DeckInDto deckInDto { get; init; }
}

public class CreateDeckCommandHandler : IRequestHandler<CreateDeckCommand, IActionResult>
{
    private readonly ApplicationDbContext _context;

    public CreateDeckCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Handle(CreateDeckCommand request, CancellationToken cancellationToken)
    {
        var deck = new Deck { Name = request.deckInDto.Name };
        try
        {
            await _context.Decks.AddAsync(deck);
            await _context.SaveChangesAsync();
            return new OkObjectResult(deck);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }
}
