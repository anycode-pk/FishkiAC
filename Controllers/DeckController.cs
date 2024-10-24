namespace FishkiAC.Controllers;

using FishkiAC.Context;
using FishkiAC.DTOs;
using FishkiAC.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("/api/v1/Deck")]
public class DeckController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DeckController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById(Guid Id)
    {
        var deck = await _context.Decks.FindAsync(Id);
        if (deck == null)
        {
            return NotFound();
        }
        return Ok(deck);
    }

    [HttpGet]
    public async Task<IEnumerable<Deck>> GetAll()
    {
        var decks = await _context.Decks.ToListAsync();
        return decks;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DeckDTO deckDTO)
    {
        var deck = new Deck { Name = deckDTO.Name };
        await _context.Decks.AddAsync(deck);
        await _context.SaveChangesAsync();
        return Ok(deck);
    }

    [HttpPut]
    public async Task<IActionResult> Put(Guid Id, [FromBody] string value)
    {
        var deck = await _context.Decks.FindAsync(Id);
        if (deck == null)
        {
            return NotFound();
        }
        deck.Name = value;
        await _context.SaveChangesAsync();
        return Ok(deck);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid Id)
    {
        var deck = await _context.Decks.FindAsync(Id);
        if (deck == null)
        {
            return NotFound();
        }
        _context.Decks.Remove(deck);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
