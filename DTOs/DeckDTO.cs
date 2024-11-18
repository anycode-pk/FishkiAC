namespace FishkiAC.DTOs;

public class DeckDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<SimpleFlashcardDto> Flashcards { get; set; } = new List<SimpleFlashcardDto>();
}
