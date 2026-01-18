using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CSCSProofOfConcept.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<CSCSProofOfConceptContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CSCSProofOfConceptContext") ?? throw new InvalidOperationException("Connection string 'CSCSProofOfConceptContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<CSCSProofOfConceptContext>();
    context.Database.EnsureCreated();
    SeedData.Initialize(context);
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
