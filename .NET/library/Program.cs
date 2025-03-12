using Microsoft.EntityFrameworkCore;
using OneBeyond.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Register LibraryContext with DI
builder.Services.AddDbContext<ILibraryContext, LibraryContext>(options => options.UseInMemoryDatabase(builder.Configuration.GetSection("DatabaseSettings:Name").Value ?? "DefaultDB"));

// Add services to the container.
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBorrowerRepository, BorrowerRepository>();
builder.Services.AddScoped<ICatalogueRepository, CatalogueRepository>();
builder.Services.AddTransient<SeedData>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed test data into memory DB
using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;
    var seedData = services.GetRequiredService<SeedData>();
    seedData.SetInitialData();
}

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
