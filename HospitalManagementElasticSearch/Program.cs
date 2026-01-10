using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using HospitalManagementElasticSearch.Application.AutoMapping;
using HospitalManagementElasticSearch.Application.Configuration;
using HospitalManagementElasticSearch.Application.Interfaces;
using HospitalManagementElasticSearch.Application.Services;
using HospitalManagementElasticSearch.Infrastructure.Repository;
using HospitalManagementSystem.Infrastructure.Data;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

// Configure Elasticsearch settings
builder.Services.Configure<ElasticsearchSettings>(
    builder.Configuration.GetSection("Elasticsearch")
);

// Register Elasticsearch client
builder.Services.AddSingleton(sp =>
{
    var options = sp.GetRequiredService<IOptions<ElasticsearchSettings>>().Value;

    var settings = new ElasticsearchClientSettings(new Uri(options.Uri))
        .Authentication(new BasicAuthentication(options.Username, options.Password))
        .ServerCertificateValidationCallback((sender, cert, chain, errors) => true);

    return new ElasticsearchClient(settings);
});

// Register ElasticDB
builder.Services.AddSingleton<ElasticDB>();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMap).Assembly);

// Register Repositories
builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();

// Register Services
builder.Services.AddScoped<IHospitalService, HospitalService>();
var app = builder.Build();

// Create Elasticsearch indices on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ElasticDB>();
    var result = await db.CreateAllIndicesAsync();
    if (result)
    {
        Console.WriteLine("Elasticsearch indices created successfully on startup.");
    }
    else
    {
        Console.WriteLine("Failed to create Elasticsearch indices on startup.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
