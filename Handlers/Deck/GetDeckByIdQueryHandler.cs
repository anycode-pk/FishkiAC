using FishkiAC.Context;
using FishkiAC.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FishkiAC.Handlers.Deck;

public sealed record GetDeckByIdQuery() : IRequest<IActionResult>
{
    public required Guid Id { get; init; }
}

public class GetDeckByIdQueryHandler : IRequestHandler<GetDeckByIdQuery, IActionResult>
{
    private readonly ApplicationDbContext _context;

    public GetDeckByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Handle(GetDeckByIdQuery request, CancellationToken cancellationToken)
    {
        var deck = await _context.Decks.Include(f => f.Flashcards).Where(d => d.Id == request.Id).FirstOrDefaultAsync();
        if (deck == null)
        {
            return new NotFoundObjectResult(request.Id);
        }

        var deckDto = new DeckDto
        {
            Id = deck.Id,
            Name = deck.Name,
            Flashcards = deck.Flashcards.Select(f => new SimpleFlashcardDto
            {
                Id = f.Id,
                Question = f.Question,
                Answer = f.Answer
            }).ToList()
        };
        return new OkObjectResult(deckDto);
    }
}
