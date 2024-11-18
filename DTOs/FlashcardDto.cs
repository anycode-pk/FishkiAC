﻿using FishkiAC.Entities;

namespace FishkiAC.DTOs;

public class FlashcardDto
{
    public Guid Id { get; set; }
    public required string Question { get; set; }
    public required string Answer { get; set; }
    public required FlashcardDeckDto Deck { get; set; }
}
