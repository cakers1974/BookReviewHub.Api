namespace BookReviewHub.Api.Controllers;

using BookReviewHub.Api.Data;
using BookReviewHub.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

[Route( "odata/[controller]" )]
[ApiController]
public class ReviewsController : ODataController
{
    private readonly ApplicationDbContext _db;
    public ReviewsController( ApplicationDbContext db ) => _db = db;

    [EnableQuery]
    public IQueryable<Review> Get() => _db.Reviews;
}
