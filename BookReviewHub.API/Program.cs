using BookReviewHub.Api.Data;
using BookReviewHub.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder( args );

builder.Services.AddDbContext<ApplicationDbContext>( options =>
    options.UseSqlServer( builder.Configuration.GetConnectionString( "DefaultConnection" ) ) );

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>( options => {
        // optional: tweak password/user options here
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
    } )
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<ApplicationDbContext>( options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString( "DefaultConnection" ),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 1,
            maxRetryDelay: TimeSpan.FromSeconds( 10 ),
            errorNumbersToAdd: null
        )
    )
);

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// MVC controllers, Swagger
builder.Services.AddControllers()
    .AddOData( opt => opt
        .AddRouteComponents( "odata", GetEdmModel() )
        .Select()
        .Filter()
        .OrderBy()
        .Expand()
        .SetMaxTop( 100 )
        .Count()
    ); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors( o => o.AddDefaultPolicy( policy =>
    policy
      .WithOrigins( "https://bookreviewhub-client-bndfg5hfcrdqbfev.centralus-01.azurewebsites.net" )
      .AllowAnyHeader()
      .AllowAnyMethod()
) );

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI( c => {
    c.SwaggerEndpoint( "/swagger/v1/swagger.json", "BookReviewHub API V1" );
} );

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseCors();
app.MapControllers();

static IEdmModel GetEdmModel()
{
    var odataBuilder = new ODataConventionModelBuilder();
    odataBuilder.EntitySet<Author>( "Authors" );
    odataBuilder.EntitySet<Book>( "Books" );
    odataBuilder.EntitySet<Review>( "Reviews" );
    odataBuilder.EntitySet<ApplicationUser>( "Users" );

    // unbound function
    odataBuilder
      .Function( "TopRatedBooks" )
      .ReturnsCollectionFromEntitySet<Book>( "Books" )
      .Parameter<int>( "count" );

    // bound function on Book
    odataBuilder
      .EntityType<Book>()
      .Function( "AverageRating" )
      .Returns<double>();

    return odataBuilder.GetEdmModel();
}

app.Run();
