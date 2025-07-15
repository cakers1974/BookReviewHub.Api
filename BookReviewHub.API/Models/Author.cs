namespace BookReviewHub.Api.Models;

using System.Collections.Generic;

public record Author
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public ICollection<Book> Books { get; init; } = new List<Book>();
}
