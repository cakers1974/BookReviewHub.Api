namespace BookReviewHub.Api.Models;

using System.Collections.Generic;

public record Book
{
    public int Id { get; init; }
    public string Title { get; init; } = default!;
    public int AuthorId { get; init; }
    public Author? Author { get; init; }
    public ICollection<Review> Reviews { get; init; } = new List<Review>();
}
