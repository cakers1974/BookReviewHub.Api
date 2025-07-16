namespace BookReviewHub.Api.Controllers;

using BookReviewHub.Api.Data;
using BookReviewHub.Api.Models;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

public class UsersController : ODataController
{
    private readonly ApplicationDbContext _db;
    public UsersController( ApplicationDbContext db ) => _db = db;

    [EnableQuery]
    public IQueryable<ApplicationUser> Get() => _db.Users;
}

