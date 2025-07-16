// Controllers/AuthorsController.cs
namespace BookReviewHub.Api.Controllers;

using BookReviewHub.Api.Data;
using BookReviewHub.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

public class AuthorsController : ODataController
{
    private readonly ApplicationDbContext _db;
    public AuthorsController( ApplicationDbContext db ) => _db = db;

    // GET /odata/Authors
    [EnableQuery]
    public IQueryable<Author> Get() => _db.Authors;

    // POST /odata/Authors
    public async Task<IActionResult> Post( [FromBody] Author author )
    {
        if( !ModelState.IsValid )
            return BadRequest( ModelState );

        _db.Authors.Add( author );
        await _db.SaveChangesAsync();
        return Created( author );
    }

    // PATCH /odata/Authors(1)
    public async Task<IActionResult> Patch( [FromODataUri] int key, Delta<Author> changes )
    {
        var entity = await _db.Authors.FindAsync( key );
        if( entity == null )
            return NotFound();

        changes.Patch( entity );
        await _db.SaveChangesAsync();
        return Updated( entity );
    }

    // DELETE /odata/Authors(1)
    public async Task<IActionResult> Delete( [FromODataUri] int key )
    {
        var entity = await _db.Authors.FindAsync( key );
        if( entity == null )
            return NotFound();

        _db.Authors.Remove( entity );
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
