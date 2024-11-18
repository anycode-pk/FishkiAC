namespace FishkiAC.Handlers.Flashcard;

using FishkiAC.Context;
using FishkiAC.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

public sealed record GetAllFlashcardsQuery() : IRequest<IEnumerable<FlashcardDto>>
{
}

public class GetAllFlashcardsQueryHandler : IRequestHandler<GetAllFlashcardsQuery, IEnumerable<FlashcardDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllFlashcardsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FlashcardDto>> Handle(GetAllFlashcardsQuery request, CancellationToken cancellationToken)
    {
        var flashcards = await _context.Flashcards
            .Include(d => d.Deck)
            .ToListAsync();

        return flashcards.Select(f => new FlashcardDto
        {
            Id = f.Id,
            Question = f.Question,
            Answer = f.Answer,
            Deck = new SimpleDeckDto
            {
                Id = f.Deck.Id,
                Name = f.Deck.Name
            }
        });
    }
}
