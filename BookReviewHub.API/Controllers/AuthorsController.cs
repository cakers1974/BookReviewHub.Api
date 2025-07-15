namespace BookReviewHub.Api.Controllers;

using BookReviewHub.Api.Data;
using BookReviewHub.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;


public class AuthorsController : ODataController
{
    private readonly ApplicationDbContext _db;
    public AuthorsController( ApplicationDbContext db ) => _db = db;

    [EnableQuery]
    public IQueryable<Author> Get() => _db.Authors;
}