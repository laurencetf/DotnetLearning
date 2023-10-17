using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
;

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

//TODO: Injecter Mediator, Injecter en singleton BowlingThrowRepository
//TODO: Configurer la connection à MongoDb
//TODO: Tests unitaires
//TODO: Configurer les controllers
app.MapControllers();
app.Run();