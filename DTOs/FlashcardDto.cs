using FishkiAC.Entities;

namespace FishkiAC.DTOs;

public class FlashcardDto
{
    public required string Question { get; set; }
    public required string Answer { get; set; }
    public required Deck Deck { get; set; }
}
