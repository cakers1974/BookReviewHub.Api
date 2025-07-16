// Controllers/FunctionsController.cs
namespace BookReviewHub.Api.Controllers;

using BookReviewHub.Api.Data;
using BookReviewHub.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

public class FunctionsController : ODataController
{
    private readonly ApplicationDbContext _db;
    public FunctionsController( ApplicationDbContext db ) => _db = db;

    // map GET /odata/TopRatedBooks(count={count})
    [HttpGet( "odata/TopRatedBooks(count={count})" )]
    [EnableQuery]
    public IQueryable<Book> TopRatedBooks( [FromODataUri] int count )
        => _db.Books
              .Where( b => b.Reviews.Any() )
              .OrderByDescending( b => b.Reviews.Average( r => r.Rating ) )
              .Take( count );
}
