var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AmazingBeer_Api>("amazingbeer-api");

builder.AddProject<Projects.AmazingBeer_Web>("amazingbeer-web");

builder.Build().Run();
