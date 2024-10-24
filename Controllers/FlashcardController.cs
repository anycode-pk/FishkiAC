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
        var flashcard = await _context.Flashcards.FindAsync(Id);
        if (flashcard == null)
        {
            return NotFound();
        }
        return Ok(flashcard);
    }

    [HttpGet]
    public async Task<IEnumerable<Flashcard>> GetAll()
    {
        var flashcards = await _context.Flashcards.ToListAsync();
        return flashcards;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromQuery] FlashcardDto flashcard)
    {
        try
        {
            await _context.AddAsync(flashcard);
            await _context.SaveChangesAsync();
            return Ok($"Added new flashcard: {flashcard}");
        } 
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Flashcard flashcard)
    {
        var existingFlashcard = await _context.Flashcards.FindAsync(flashcard.Id);
        if (existingFlashcard == null)
        {
            return NotFound();
        }
        try
        { 
            _context.Flashcards.Update(flashcard);
            await _context.SaveChangesAsync();
            return Ok($"Updated flashcard: {flashcard}");
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
            return Ok($"Deleted flashcard: {flashcard}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
