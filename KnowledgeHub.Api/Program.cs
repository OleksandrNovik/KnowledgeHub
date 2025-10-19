using AutoMapper;
using KnowledgeHub.Api.Helpers;
using KnowledgeHub.Application;
using KnowledgeHub.Application.User.Authorization;
using KnowledgeHub.Application.User.Registration;
using KnowledgeHub.Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi()
    .RegisterServices()
    .RegisterRepositories()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyMarker.Assembly));

var config = new MapperConfiguration(cfg => { cfg.AddMaps(KnowledgeHub.Infrastructure.AssemblyMarker.Assembly); },
    new LoggerFactory());

builder.Services.AddSingleton(config.CreateMapper());

builder.ConfigureDbContext()
    .ConfigureJwtAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    await using var scope = app.Services.CreateAsyncScope();
    await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.EnsureCreatedAsync();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/v1/user/register",
    ([FromBody] UserRegistrationCommand command, ISender sender) =>
    {
        return MiddlewareHelper.HandleAsync(sender, command);
    });

app.MapPost("/v1/user/authorization",
    ([FromBody] UserAuthorizationCommand command, ISender sender) =>
    {
        return MiddlewareHelper.HandleAsync(sender, command);
    });

app.Run();