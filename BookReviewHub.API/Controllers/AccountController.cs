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
    public async Task<IActionResult> Register( [FromBody] RegisterModel model )
    {
        if( !ModelState.IsValid )
            return BadRequest( ModelState );

        var user = new ApplicationUser {
            UserName = model.UserName,  // use the supplied UserName
            Email = model.Email
        };

        var result = await _userManager.CreateAsync( user, model.Password );
        if( !result.Succeeded ) {
            foreach( var error in result.Errors )
                ModelState.AddModelError( error.Code, error.Description );
            return ValidationProblem( ModelState );
        }

        return Ok( new { user.Id } );
    }
}
