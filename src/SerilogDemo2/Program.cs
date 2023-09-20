using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog((context, config) =>
{
    config.Enrich.WithMachineName();
    config.ReadFrom.Configuration(context.Configuration);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/log", (ILogger<Program> logger) =>
{
    var message = "test serilog logging 2.";
    logger.LogInformation(message);
    return Results.Ok(message);
})
.WithName("Log");

app.Run();