using Microsoft.AspNetCore.Mvc;

namespace FishkiAC.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class DeckController : ControllerBase
{
    public DeckController()
    {
    }

    [HttpGet]
    public IActionResult GetById(Guid Id)
    {
        return Ok($"Hello, World! id: {Id}");
    }

    [HttpGet]
    public IEnumerable<string> GetById()
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
