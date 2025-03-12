using Microsoft.EntityFrameworkCore;
using OneBeyond.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Register LibraryContext with DI
builder.Services.AddDbContext<LibraryContext>(options => options.UseInMemoryDatabase("OneBeyondDB"));

// Add services to the container.
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBorrowerRepository, BorrowerRepository>();
builder.Services.AddScoped<ICatalogueRepository, CatalogueRepository>();
builder.Services.AddScoped<ILibraryContext, LibraryContext>();

// Seed test data into memory DB
SeedData.SetInitialData();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
