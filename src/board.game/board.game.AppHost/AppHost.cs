using Projects;

var builder = DistributedApplication.CreateBuilder(args);



var postgres = builder.AddPostgres("postgres");
var postgresdb = postgres.AddDatabase("boardgames");

var migrations = builder.AddProject<board_game_MigrationService>("migrations")
    .WithReference(postgresdb)
    .WaitFor(postgresdb);


var apiService = builder.AddProject<board_game_Api>("api")
    .WithReference(postgresdb)
    .WithReference(migrations)
    .WaitForCompletion(migrations)
    .WithHttpHealthCheck("/health");



builder.AddProject<board_game_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);


builder.Build().Run();
