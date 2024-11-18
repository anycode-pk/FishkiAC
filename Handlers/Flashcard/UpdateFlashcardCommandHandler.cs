namespace FishkiAC.Handlers.Flashcard;

using FishkiAC.Context;
using FishkiAC.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public sealed record UpdateFlashcardCommand() : IRequest<IActionResult>
{
    public required Guid Id { get; init; }
    public required FlashcardInDto FlashcardInDto { get; init; }
}

public class UpdateFlashcardCommandHandler : IRequestHandler<UpdateFlashcardCommand, IActionResult>
{
    private readonly ApplicationDbContext _context;

    public UpdateFlashcardCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Handle(UpdateFlashcardCommand request, CancellationToken cancellationToken)
    {
        var existingFlashcard = await _context.Flashcards.FindAsync(request.Id);

        if (existingFlashcard == null)
        {
            return new NotFoundObjectResult(request.Id);
        }

        existingFlashcard.Question = request.FlashcardInDto.Question;
        existingFlashcard.Answer = request.FlashcardInDto.Answer;
        existingFlashcard.Deck = await _context.Decks.FindAsync(request.FlashcardInDto.DeckId);

        try
        {
            await _context.SaveChangesAsync();
            return new OkObjectResult(request.FlashcardInDto);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }
}
