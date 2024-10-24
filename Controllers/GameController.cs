namespace FishkiAC.Controllers;

using FishkiAC.Context;
using FishkiAC.DTOs;
using FishkiAC.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("/api/v1/Game")]
public class GameController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public GameController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetRandomFlashcards([FromQuery] Guid DeckId, [FromQuery] int amount)
    {
        var deck = await _context.Decks.FindAsync(DeckId);
        if (deck == null)
        {
            return NotFound();
        }

        var flashcards = await _context.Flashcards
            .Where(f => f.Deck.Id == DeckId)
            .OrderBy(f => Guid.NewGuid())
            .Take(amount)
            .ToListAsync();

        
        return Ok(flashcards);
    }
}
