namespace BookReviewHub.Api.Controllers;

using BookReviewHub.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route( "api/[controller]" )]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    public AccountController( UserManager<ApplicationUser> userManager )
        => _userManager = userManager;

    [HttpPost( "register" )]
    public async Task<IActionResult> Register( RegisterModel model )
    {
        var user = new ApplicationUser {
            UserName = model.UserName,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync( user, model.Password );
        if( !result.Succeeded )
            return BadRequest( result.Errors );

        // Return the new user's Id so you can reference it in Review.ReviewerId
        return Ok( new { user.Id } );
    }
}
