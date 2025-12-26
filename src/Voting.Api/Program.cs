using FluentValidation;
using Voting.Api;
using Voting.Api.Hubs;
using Voting.Application.Dtos;
using Voting.Application.UseCases.Candidates;
using Voting.Application.UseCases.Voters;
using Voting.Application.UseCases.Votes;
using Voting.Application.Validators;
using Voting.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policyBuilder =>
        {
            policyBuilder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<VotingDbContext>(options =>
    options.UseSqlite(connectionString));

// Use cases
builder.Services.AddScoped<IGetAllCandidatesUseCase, GetAllCandidatesUseCase>();
builder.Services.AddScoped<ICreateCandidateUseCase, CreateCandidateUseCase>();
builder.Services.AddScoped<IGetAllVotersUseCase, GetAllVotersUseCase>();
builder.Services.AddScoped<ICreateVoterUseCase, CreateVoterUseCase>();
builder.Services.AddScoped<ICastVoteUseCase, CastVoteUseCase>();

// Validators
builder.Services.AddScoped<IValidator<CreateCandidateRequest>, CreateCandidateRequestValidator>();
builder.Services.AddScoped<IValidator<CreateVoterRequest>, CreateVoterRequestValidator>();
builder.Services.AddScoped<IValidator<CastVoteRequest>, CastVoteRequestValidator>();

// SignalR
builder.Services.AddSignalR();

// Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Map SignalR hub
app.MapHub<VotingHub>("/hubs/voting");

// Seed database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<VotingDbContext>();
    await DatabaseSeeder.SeedAsync(dbContext);
}

app.Run();
