using FishkiAC.Entities;

namespace FishkiAC.DTOs;

public class DeckFlashcardDto
{
    public Guid Id { get; set; }
    public required string Question { get; set; }
    public required string Answer { get; set; }
}
