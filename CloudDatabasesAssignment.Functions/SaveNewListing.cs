using CloudDatabasesAssignment.Api.Services.Interfaces;
using CloudDatabasesAssignment.Models.Entities;
using Microsoft.Azure.Functions.Worker;

namespace CloudDatabasesAssignment.Functions;

public class SaveNewListing
{
    private readonly IListingService _listingService;

    public SaveNewListing(IListingService listingService)
    {
        _listingService = listingService;
    }

    [Function("SaveNewListing")]
    public async Task Run([QueueTrigger("newlistings", Connection = "AzureWebJobsStorage")] Listing newListing, FunctionContext context)
    {
        await _listingService.AddAsync(newListing);
    }
}