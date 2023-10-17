var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


//TODO: Injecter Mediator, Injecter en singleton BowlingThrowRepository
//TODO: Configurer la connection à MongoDb
//TODO: Tests unitaires
//TODO: Configurer les controllers

app.MapGet("/", () => "Hello World!");

app.Run();