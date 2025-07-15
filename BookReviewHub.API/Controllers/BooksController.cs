namespace BookReviewHub.Api.Controllers;

using BookReviewHub.Api.Data;
using BookReviewHub.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;


public class BooksController : ODataController
{
    private readonly ApplicationDbContext _db;
    public BooksController( ApplicationDbContext db ) => _db = db;

    [EnableQuery]
    public IQueryable<Book> Get() => _db.Books;
}