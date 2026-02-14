
using board.game.GameDb.Context;
using board.game.Services;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        
        // Add service defaults & Aspire client integrations.
        builder.AddServiceDefaults();

        //add the controllers
        builder.Services.AddControllers();

        //add di
        builder.Services.AddScoped<IGameServices, GameServices>();

        //Add the db
        builder.AddNpgsqlDbContext<GameDbContext>("boardgames");

        // Add services to the container.
        builder.Services.AddProblemDetails();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseExceptionHandler();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.MapControllers();
        app.MapDefaultEndpoints();

        app.Run();
    }
}