using FishkiAC.Context;
using FishkiAC.DTOs;
using FishkiAC.Entities;
using FishkiAC.Handlers.Deck;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FishkiAC.Handlers.Game;

public sealed record GetRandomFlashcardsCommand() : IRequest<IActionResult>
{
    public Guid DeckId { get; init; }
    public int Amount { get; init; }
}

public class GetRandomFlashcardsCommandHandler : IRequestHandler<GetRandomFlashcardsCommand, IActionResult>
{
    private readonly ApplicationDbContext _context;

    public GetRandomFlashcardsCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Handle(GetRandomFlashcardsCommand request, CancellationToken cancellationToken)
    {
        var deck = await _context.Decks.FindAsync(request.DeckId);
        if (deck == null)
        {
            return new NotFoundObjectResult(request.DeckId);
        }

        var flashcards = await _context.Flashcards
            .Where(f => f.Deck.Id == request.DeckId)
            .OrderBy(f => Guid.NewGuid())
            .Take(request.Amount)
            .ToListAsync();

        var flashcardsDto = flashcards.Select(f => new SimpleFlashcardDto
        {
            Id = f.Id,
            Question = f.Question,
            Answer = f.Answer
        });

        return new OkObjectResult(flashcardsDto);
    }
}
