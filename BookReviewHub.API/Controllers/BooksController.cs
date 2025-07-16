using BookReviewHub.Api.Data;
using BookReviewHub.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace BookReviewHub.Api.Controllers;

public class BooksController : ODataController
{
    private readonly ApplicationDbContext _db;
    public BooksController( ApplicationDbContext db ) => _db = db;

    [EnableQuery]
    public IQueryable<Book> Get() => _db.Books;

    public async Task<IActionResult> Post( [FromBody] Book book )
    {
        if( !ModelState.IsValid ) return BadRequest( ModelState );
        _db.Books.Add( book );
        await _db.SaveChangesAsync();
        return Created( book );
    }

    public async Task<IActionResult> Patch( [FromODataUri] int key, Delta<Book> changes )
    {
        var entity = await _db.Books.FindAsync( key );
        if( entity == null ) return NotFound();
        changes.Patch( entity );
        await _db.SaveChangesAsync();
        return Updated( entity );
    }

    public async Task<IActionResult> Delete( [FromODataUri] int key )
    {
        var entity = await _db.Books.FindAsync( key );
        if( entity == null ) return NotFound();
        _db.Books.Remove( entity );
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
