namespace FishkiAC.Context;

using FishkiAC.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Deck> Decks { get; set; }
    public DbSet<Flashcard> Flashcards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Deck>()
            .HasMany(d => d.Flashcards)
            .WithOne(f => f.Deck)
            .HasForeignKey("DeckId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}

