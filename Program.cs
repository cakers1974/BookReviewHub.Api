using BookReviewHub.Api.Data;
using BookReviewHub.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder( args );

// 1) EF Core: register your DbContext
builder.Services.AddDbContext<ApplicationDbContext>( options =>
    options.UseSqlServer( builder.Configuration.GetConnectionString( "DefaultConnection" ) ) );

// 2) Identity: register Identity with EF stores
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

// 3) Authentication & Authorization (for future JWT or cookie setup)
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// 4) MVC controllers, Swagger
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

var app = builder.Build();

// middleware pipeline
if( app.Environment.IsDevelopment() ) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.UseEndpoints( endpoints => {
    endpoints.MapControllers();
} );

app.MapControllers();
static IEdmModel GetEdmModel()
{
    var odataBuilder = new ODataConventionModelBuilder();
    odataBuilder.EntitySet<Author>( "Authors" );
    odataBuilder.EntitySet<Book>( "Books" );
    odataBuilder.EntitySet<Review>( "Reviews" );
    return odataBuilder.GetEdmModel();
}

app.Run();
