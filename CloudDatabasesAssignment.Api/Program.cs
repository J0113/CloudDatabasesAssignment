using System.Text.Json.Serialization;
using CloudDatabasesAssignment.Api.Context;
using CloudDatabasesAssignment.Api.Services;
using CloudDatabasesAssignment.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var serverVersion = new MySqlServerVersion(new Version(5, 7, 32));

builder.Services.AddDbContext<ApiDbContext>(options =>
{
    options.UseMySql(Environment.GetEnvironmentVariable("MySQL"), serverVersion);
    options.EnableSensitiveDataLogging();
});

builder.Services.AddAzureClients(b =>
{
    b.AddQueueServiceClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage"))
        .ConfigureOptions(c => c.MessageEncoding = Azure.Storage.Queues.QueueMessageEncoding.Base64);
});

// Dependency injection
builder.Services.AddLogging();
builder.Services.AddTransient<IMortgageInquiryService, MortgageInquiryService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IListingService, ListingService>();
builder.Services.AddAutoMapper(typeof(AutoMappingProfile));

var app = builder.Build();

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