using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddControllers()
;

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

//TODO: Injecter Mediator, Injecter en singleton BowlingThrowRepository
//TODO: Configurer la connection à MongoDb
//TODO: Tests unitaires
//TODO: Configurer les controllers

app.Run();