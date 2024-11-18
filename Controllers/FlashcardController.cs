namespace FishkiAC.Controllers;

using FishkiAC.DTOs;
using FishkiAC.Handlers.Flashcard;
using MediatR;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("/api/v1/Flashcard")]
public class FlashcardController : ControllerBase
{
    private readonly IMediator _mediator;

    public FlashcardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("ById")]
    public async Task<IActionResult> GetById([FromQuery] GetFlashcardByIdQuery query)
    {
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet]
    public async Task<IEnumerable<FlashcardDto>> GetAll()
    {
        var result = await _mediator.Send(new GetAllFlashcardsQuery());
        return result;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateFlashcardCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPut]
    public async Task<IActionResult> Put(Guid Id, [FromBody] FlashcardInDto flashcardInDto)
    {
        var result = await _mediator.Send(new UpdateFlashcardCommand { Id = Id, FlashcardInDto = flashcardInDto });
        return result;
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(Guid Id)
    {
        var result = await _mediator.Send(new DeleteFlashcardCommand { Id = Id });
        return result;
    }
}
