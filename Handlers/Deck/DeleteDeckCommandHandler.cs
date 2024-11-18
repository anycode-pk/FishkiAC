namespace FishkiAC.Handlers.Deck;

using FishkiAC.Context;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public sealed record DeleteDeckCommand() : IRequest<IActionResult>
{
    public required Guid Id { get; init; }
}

public class DeleteDeckCommandHandler : IRequestHandler<DeleteDeckCommand, IActionResult>
{
    private readonly ApplicationDbContext _context;

    public DeleteDeckCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Handle(DeleteDeckCommand request, CancellationToken cancellationToken)
    {
        var deck = await _context.Decks.FindAsync(request.Id);

        if (deck == null)
        {
            return new NotFoundObjectResult(request.Id);
        }

        try
        {
            _context.Decks.Remove(deck);
            await _context.SaveChangesAsync();
            return new OkObjectResult(deck);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }
}

