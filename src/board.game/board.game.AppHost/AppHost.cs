var builder = DistributedApplication.CreateBuilder(args);



var postgres = builder.AddPostgres("postgres");
var postgresdb = postgres.AddDatabase("boardgames");



var apiService = builder.AddProject<Projects.board_game_Api>("api")
    .WaitFor(postgresdb)
    .WithReference(postgresdb)
    .WithHttpHealthCheck("/health");



builder.AddProject<Projects.board_game_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
