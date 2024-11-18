namespace FishkiAC.Handlers.Flashcard;

using FishkiAC.Context;
using FishkiAC.DTOs;
using FishkiAC.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public sealed record CreateFlashcardCommand() : IRequest<IActionResult>
{
    public required FlashcardInDto FlashcardInDto { get; init; }
}

public class CreateFlashcardCommandHandler : IRequestHandler<CreateFlashcardCommand, IActionResult>
{
    private readonly ApplicationDbContext _context;

    public CreateFlashcardCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Handle(CreateFlashcardCommand request, CancellationToken cancellationToken)
    {
        var deck = await _context.Decks.FindAsync(request.FlashcardInDto.DeckId);

        if (deck == null)
        {
            return new NotFoundObjectResult(request.FlashcardInDto);
        }

        var flashcard = new Flashcard
        {
            Question = request.FlashcardInDto.Question,
            Answer = request.FlashcardInDto.Answer,
            Deck = deck
        };
        deck.Flashcards.Add(flashcard);

        try
        {
            await _context.SaveChangesAsync();
            return new OkObjectResult(new FlashcardDto { Id = flashcard.Id, Question = flashcard.Question, Answer = flashcard.Answer, Deck = new SimpleDeckDto { Id = deck.Id, Name = deck.Name } });
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }
}
