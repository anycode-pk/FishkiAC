namespace FishkiAC.Controllers;

using FishkiAC.Context;
using FishkiAC.DTOs;
using FishkiAC.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("/api/v1/Flashcard")]
public class FlashcardController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public FlashcardController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById(Guid Id)
    {
        var flashcard = await _context.Flashcards
            .Include(d => d.Deck)
            .Where(d => d.Id == Id)
            .FirstOrDefaultAsync();

        if (flashcard == null)
        {
            return NotFound();
        }

        var flashcardDto = new FlashcardDto
        {
            Id = flashcard.Id,
            Question = flashcard.Question,
            Answer = flashcard.Answer,
            Deck = new FlashcardDeckDto
            {
                Id = flashcard.Deck.Id,
                Name = flashcard.Deck.Name
            }
        };

        return Ok(flashcardDto);
    }

    [HttpGet]
    public async Task<IEnumerable<FlashcardDto>> GetAll()
    {
        var flashcards = await _context.Flashcards
            .Include(d => d.Deck)
            .ToListAsync();

        return flashcards.Select(f => new FlashcardDto
        {
            Id = f.Id,
            Question = f.Question,
            Answer = f.Answer,
            Deck = new FlashcardDeckDto
            {
                Id = f.Deck.Id,
                Name = f.Deck.Name
            }
        });
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] flashcardInDto flashcardDto)
    {
        var deck = await _context.Decks.FindAsync(flashcardDto.DeckId);

        if (deck == null)
        {
            return NotFound();
        }

        var flashcard = new Flashcard
        {
            Question = flashcardDto.Question,
            Answer = flashcardDto.Answer,
            Deck = deck
        };
        deck.Flashcards.Add(flashcard);

        try
        {
            await _context.SaveChangesAsync();
            return Ok(new FlashcardDto { Id = flashcard.Id, Question = flashcard.Question, Answer = flashcard.Answer, Deck = new FlashcardDeckDto { Id = deck.Id, Name = deck.Name } });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    [HttpPut]
    public async Task<IActionResult> Put(Guid Id, [FromBody] flashcardInDto flashcardInDto)
    {
        var existingFlashcard = await _context.Flashcards.FindAsync(Id);

        if (existingFlashcard == null)
        {
            return NotFound();
        }
        existingFlashcard.Question = flashcardInDto.Question;
        existingFlashcard.Answer = flashcardInDto.Answer;
        existingFlashcard.Deck = await _context.Decks.FindAsync(flashcardInDto.DeckId);
        try
        {
            await _context.SaveChangesAsync();
            return Ok(flashcardInDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(Guid Id)
    {
        var flashcard = await _context.Flashcards.FindAsync(Id);

        if (flashcard == null)
        {
            return NotFound();
        }

        try
        {
            _context.Flashcards.Remove(flashcard);
            await _context.SaveChangesAsync();
            return Ok(flashcard);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
