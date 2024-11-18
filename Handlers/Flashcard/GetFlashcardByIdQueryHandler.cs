namespace FishkiAC.Handlers.Flashcard;

using FishkiAC.Context;
using FishkiAC.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


public sealed record GetFlashcardByIdQuery() : IRequest<IActionResult>
{
    public required Guid Id { get; init; }
}

public class GetFlashcardByIdQueryHandler : IRequestHandler<GetFlashcardByIdQuery, IActionResult>
{
    private readonly ApplicationDbContext _context;

    public GetFlashcardByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Handle(GetFlashcardByIdQuery request, CancellationToken cancellationToken)
    {
        var flashcard = await _context.Flashcards
            .Include(d => d.Deck)
            .Where(d => d.Id == request.Id)
            .FirstOrDefaultAsync();

        if (flashcard == null)
        {
            return new NotFoundObjectResult(request.Id);
        }

        var flashcardDto = new FlashcardDto
        {
            Id = flashcard.Id,
            Question = flashcard.Question,
            Answer = flashcard.Answer,
            Deck = new SimpleDeckDto
            {
                Id = flashcard.Deck.Id,
                Name = flashcard.Deck.Name
            }
        };

        return new OkObjectResult(flashcardDto);
    }
}
