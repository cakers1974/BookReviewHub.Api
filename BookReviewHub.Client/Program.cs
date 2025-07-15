using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder( args );
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Register a named HttpClient pointing at your API
builder.Services.AddHttpClient( "API", client => {
    client.BaseAddress = new Uri( "https://localhost:5001" ); // adjust port if needed
} );

var app = builder.Build();
// … the rest of the default Blazor Server boilerplate …
app.MapBlazorHub();
app.MapFallbackToPage( "/_Host" );
app.Run();