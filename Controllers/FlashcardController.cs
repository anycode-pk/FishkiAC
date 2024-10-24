namespace FishkiAC.Controllers;

using FishkiAC.Context;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetById(Guid Id)
    {
        return Ok($"Hello, World! id: {Id}");
    }

    [HttpGet]
    public IEnumerable<string> GetAll()
    {
        return new List<string> { "value1", "value2" };
    }

    [HttpPost]
    public IActionResult Post([FromBody] string value)
    {
        return Ok($"Hello, World! value: {value}");
    }

    [HttpPut]
    public IActionResult Put(Guid Id, [FromBody] string value)
    {
        return Ok($"Hello, World! id: {Id}, value: {value}");
    }

    [HttpDelete]
    public IActionResult Delete(Guid Id)
    {
        return Ok($"Hello, World! id: {Id}");
    }
}
