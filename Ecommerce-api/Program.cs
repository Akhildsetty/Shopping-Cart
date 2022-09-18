using Ecommerce_api.Data;
using Ecommerce_api.Repositories;
using Ecommerce_api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("SiteCorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();

    });
});
builder.Services.AddDbContext<databaseContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IAccountRepo,AccountRepo>();
builder.Services.AddScoped<ISharedRepo, SharedRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("SiteCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
