namespace BookReviewHub.Api.Controllers;

using BookReviewHub.Api.Data;
using BookReviewHub.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;


public class ReviewsController : ODataController
{
    private readonly ApplicationDbContext _db;
    public ReviewsController( ApplicationDbContext db ) => _db = db;

    public IQueryable<Review> Get() => _db.Reviews;

    public async Task<IActionResult> Post( [FromBody] Review review )
    {
        if( !ModelState.IsValid )
            return BadRequest( ModelState );

        _db.Reviews.Add( review );
        await _db.SaveChangesAsync();
        return Created( review );
    }
}