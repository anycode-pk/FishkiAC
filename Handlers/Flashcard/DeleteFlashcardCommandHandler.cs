namespace FishkiAC.Handlers.Flashcard;

using FishkiAC.Context;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public sealed record DeleteFlashcardCommand() : IRequest<IActionResult>
{
    public required Guid Id { get; init; }
}

public class DeleteFlashcardCommandHandler : IRequestHandler<DeleteFlashcardCommand, IActionResult>
{
    private readonly ApplicationDbContext _context;

    public DeleteFlashcardCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Handle(DeleteFlashcardCommand request, CancellationToken cancellationToken)
    {
        var flashcard = await _context.Flashcards.FindAsync(request.Id);

        if (flashcard == null)
        {
            return new NotFoundObjectResult(request.Id);
        }

        try
        {
            _context.Flashcards.Remove(flashcard);
            await _context.SaveChangesAsync();
            return new OkObjectResult(flashcard);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }
}

