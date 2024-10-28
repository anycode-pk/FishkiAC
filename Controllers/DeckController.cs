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
    public async Task<IActionResult> Post([FromBody] DeckDto deckDto)
    {
        var deck = new Deck { Name = deckDto.Name };
        try
        {
            await _context.Decks.AddAsync(deck);
            await _context.SaveChangesAsync();
            return Ok(deck);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put(Guid Id, [FromBody] DeckDto deckDto)
    {
        var deck = await _context.Decks.FindAsync(Id);
        if (deck == null)
        {
            return NotFound();
        }
        deck.Name = deckDto.Name;
        try
        {
            await _context.SaveChangesAsync();
            return Ok(deck);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(Guid Id)
    {
        var deck = await _context.Decks.FindAsync(Id);
        if (deck == null)
        {
            return NotFound();
        }
        try
        {
            _context.Decks.Remove(deck);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
}
