using Microsoft.AspNetCore.Mvc;

using EarthColors.Domain.Entities;
using EarthColors.Domain.Interfaces;
using EarthColors.Infrastructure.Repository;

using System.Linq;  

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<CountriesRepository>();
builder.Services.AddSingleton<VotesRepository>();
builder.Services.Configure<MongoConnectionSettings>(
    builder.Configuration.GetSection("EarthColorsDatabase"));


var app = builder.Build();

app.MapGet("/countries", ([FromServices] CountriesRepository repo) =>
{
    return repo.GetAll();
});

app.MapGet("/countries/{id}", ([FromServices] CountriesRepository repo, Guid id) =>
{
    var country = repo.GetById(id);
    return country is not null ? Results.Ok(country) : Results.NotFound(new { Message = "Country not found."});
});

app.MapPost("/countries/{id}", ([FromServices] CountriesRepository repo, Country country) =>
{
    repo.Create(country);
    return Results.Created($"/countries/{country.Id}", country);
});

app.MapPut("/countries/{id}", ([FromServices] CountriesRepository repo, Country updatedCountry) => 
{
    var country = repo.GetById(updatedCountry.Id);
    if(country is null)
    {
        return Results.NotFound();
    }
    repo.Update(updatedCountry);
    return Results.Ok(updatedCountry);
});

app.MapDelete("/countries/{id}", ([FromServices] CountriesRepository repo, Guid id) =>
{
    repo.Delete(id);
    return Results.Ok();
});

app.MapGet("/votes", ([FromServices] VotesRepository repo) =>
{
    return repo.GetAll();
});

app.MapGet("/votes/{id}", ([FromServices] VotesRepository repo, Guid id) =>
{
    var vote = repo.GetById(id);
    return vote is not null ? Results.Ok(vote) : Results.NotFound(new { Message = "Vote not found."});
});

app.MapPost("/votes/{id}", ([FromServices] VotesRepository repo, [FromServices] CountriesRepository countriesRepository, Vote vote) =>
{
    if(countriesRepository.GetById(vote.CountryId) is null)
        return Results.NotFound(new { Message = $"Country not found for this vote.", vote.Id, vote.CountryId });

    repo.Create(vote);
    
    return Results.Created($"/votes/{vote.Id}", vote);
});

app.MapPut("/votes/{id}", ([FromServices] VotesRepository repo, Vote updatedVote) => 
{
    var vote = repo.GetById(updatedVote.Id);
    if(vote is null)
    {
        return Results.NotFound(new { Message = "Vote not found."});
    }
    repo.Update(updatedVote);
    return Results.Ok(updatedVote);
});

app.MapDelete("/votes/{id}", ([FromServices] VotesRepository repo, Guid id) =>
{
    repo.Delete(id);
    return Results.Ok();
});

app.MapGet("/country/{id}/rank", ([FromServices] VotesRepository repo, Guid id) =>
{
    return repo.GetAll()
        .Where(vote => vote.CountryId == id)
        .GroupBy(vote => vote.Color)
        .Select(votes => new { Count = votes.Count(), votes.First().Color} )
        .OrderByDescending(votesInColor => votesInColor.Count)
        .Take(10)
        .ToList();
});

app.MapGet("/country/{id}/votes", ([FromServices] VotesRepository repo, Guid id) =>
{
    return repo.GetAll()
        .Where(vote => vote.CountryId == id)
        .ToList();
});

app.Run();