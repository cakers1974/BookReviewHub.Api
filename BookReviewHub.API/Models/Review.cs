namespace BookReviewHub.Api.Models;

using System;

public record Review
{
    public int Id { get; init; }
    public int BookId { get; init; }
    public Book? Book { get; init; }
    public string ReviewerId { get; init; } = default!;   // FK to ApplicationUser.Id
    public ApplicationUser? Reviewer { get; init; }
    public int Rating { get; init; }                      // e.g. 1–5
    public string? Comment { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
