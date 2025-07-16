using BookReviewHub.Api.Data;
using BookReviewHub.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace BookReviewHub.Api.Controllers;

public class ReviewsController : ODataController
{
    private readonly ApplicationDbContext _db;
    public ReviewsController( ApplicationDbContext db ) => _db = db;

    // GET /odata/Reviews
    [EnableQuery]
    public IQueryable<Review> Get() => _db.Reviews;

    // POST /odata/Reviews
    public async Task<IActionResult> Post( [FromBody] Review review )
    {
        if( !ModelState.IsValid ) return BadRequest( ModelState );
        _db.Reviews.Add( review );
        await _db.SaveChangesAsync();
        return Created( review );
    }

    // PATCH /odata/Reviews(1)
    public async Task<IActionResult> Patch( [FromODataUri] int key, Delta<Review> changes )
    {
        var entity = await _db.Reviews.FindAsync( key );
        if( entity == null ) return NotFound();
        changes.Patch( entity );
        await _db.SaveChangesAsync();
        return Updated( entity );
    }

    // DELETE /odata/Reviews(1)
    public async Task<IActionResult> Delete( [FromODataUri] int key )
    {
        var entity = await _db.Reviews.FindAsync( key );
        if( entity == null ) return NotFound();
        _db.Reviews.Remove( entity );
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
