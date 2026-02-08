var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.board_game_Api>("api")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.board_game_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
