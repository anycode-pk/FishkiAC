namespace FishkiAC.Entities;

public class Flashcard
{
    public Guid Id { get; set; }
    public required string Question { get; set; }
    public required string Answer { get; set; }
    public required Deck Deck { get; set; }
}
