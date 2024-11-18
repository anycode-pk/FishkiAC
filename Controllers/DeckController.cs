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
        var deck = await _context.Decks.Include(f => f.Flashcards).Where(d => d.Id == Id).FirstOrDefaultAsync();
        if (deck == null)
        {
            return NotFound();
        }

        var deckDto = new DeckDto
        {
            Id = deck.Id,
            Name = deck.Name,
            Flashcards = deck.Flashcards.Select(f => new DeckFlashcardDto
            {
                Id = f.Id,
                Question = f.Question,
                Answer = f.Answer
            }).ToList()
        };
        return Ok(deckDto);
    }

    [HttpGet]
    public async Task<IEnumerable<DeckDto>> GetAll()
    {
        var decks = await _context.Decks.Include(f => f.Flashcards).ToListAsync();
        var decksDto = decks.Select(d => new DeckDto
        {
            Id = d.Id,
            Name = d.Name,
            Flashcards = d.Flashcards.Select(f => new DeckFlashcardDto
            {
                Id = f.Id,
                Question = f.Question,
                Answer = f.Answer
            }).ToList()
        });
        return decksDto;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DeckInDto deckInDto)
    {
        var deck = new Deck { Name = deckInDto.Name };
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
    public async Task<IActionResult> Put(Guid Id, [FromBody] DeckInDto deckInDto)
    {
        var deck = await _context.Decks.FindAsync(Id);
        if (deck == null)
        {
            return NotFound();
        }
        deck.Name = deckInDto.Name;
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
