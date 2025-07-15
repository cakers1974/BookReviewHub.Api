namespace BookReviewHub.Api.Models;

public record RegisterModel
{
    public string UserName { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
}