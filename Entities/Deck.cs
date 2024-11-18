namespace FishkiAC.Entities;

public class Deck
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
}
