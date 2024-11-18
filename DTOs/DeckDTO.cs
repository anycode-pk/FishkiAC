namespace FishkiAC.DTOs;

public class DeckDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<DeckFlashcardDto> Flashcards { get; set; } = new List<DeckFlashcardDto>();
}
