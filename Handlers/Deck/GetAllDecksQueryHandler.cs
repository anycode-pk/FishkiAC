namespace FishkiAC.Handlers.Deck;

using FishkiAC.Context;
using FishkiAC.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

public sealed record GetAllDecksQuery() : IRequest<IEnumerable<DeckDto>>
{
}

public class GetAllDecksQueryHandler : IRequestHandler<GetAllDecksQuery, IEnumerable<DeckDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllDecksQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DeckDto>> Handle(GetAllDecksQuery request, CancellationToken cancellationToken)
    {
        var decks = await _context.Decks.Include(f => f.Flashcards).ToListAsync();
        var decksDto = decks.Select(d => new DeckDto
        {
            Id = d.Id,
            Name = d.Name,
            Flashcards = d.Flashcards.Select(f => new SimpleFlashcardDto
            {
                Id = f.Id,
                Question = f.Question,
                Answer = f.Answer
            }).ToList()
        });
        return decksDto;
    }
}
