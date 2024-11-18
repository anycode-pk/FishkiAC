namespace FishkiAC.DTOs;

public class FlashcardInDto
{
    public required string Question { get; set; }
    public required string Answer { get; set; }
    public Guid DeckId { get; set; }
}
