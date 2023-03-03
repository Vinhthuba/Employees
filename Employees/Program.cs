
using Employees;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString
    ("DefaultConnetion")));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


async Task<List<Employ>> GetAllEmploy(DataContext context) => await context.Employ.ToListAsync();
app.MapGet("/Employ", async (DataContext context) => await context.Employ.ToListAsync());
app.MapGet("/Employ/{id}", async (DataContext context, int id) => await context.Employ.FindAsync(id) is Employ Item ? Results.Ok(Item) : Results.NotFound("Item not found"));
app.MapPost("App/Employ", async (DataContext context, Employ Item) =>
{
    context.Employ.Add(Item);
    await context.SaveChangesAsync();
    return Results.Ok(await GetAllEmploy(context));

});
app.MapPut("/Employ/{id}", async (DataContext context, Employ Item, int id) =>
{
    var Employitem = await context.Employ.FindAsync(id);
    if (Employitem == null) return Results.NotFound("Items not Found");
    Employitem.name = Item.name;
    await context.SaveChangesAsync();
    return Results.Ok(await GetAllEmploy(context));     
});
app.MapDelete("/Delete/{id}", async (DataContext context, int id) =>
{
    var Employitem = context.Employ.FindAsync(id);
    if (Employitem == null) return Results.NotFound("Items not Found");
    context.Remove(Employitem);
    await context.SaveChangesAsync();
    return Results.Ok(await GetAllEmploy(context));

});

app.Run();