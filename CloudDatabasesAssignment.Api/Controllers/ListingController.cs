using System.Collections;
using AutoMapper;
using Azure.Storage.Queues;
using CloudDatabasesAssignment.Api.Services.Interfaces;
using CloudDatabasesAssignment.Models.Entities;
using CloudDatabasesAssignment.Models.Requests;
using CloudDatabasesAssignment.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CloudDatabasesAssignment.Api.Controllers;

[ApiController]
[Route("listings")]
public class ListingController : ControllerBase
{
    
    private readonly IMapper _mapper;
    private readonly IListingService _listingService;
    private readonly QueueServiceClient _queueServiceClient;
    
    public ListingController(IMapper mapper, QueueServiceClient queueServiceClient, IListingService listingService)
    {
        _mapper = mapper;
        _queueServiceClient = queueServiceClient;
        _listingService = listingService;
    }
    
    [HttpPost(Name = "New listing")]
    public async Task<ListingResponse> CreateNewListing(ListingRequest listingRequest)
    {
        Listing listing = _mapper.Map<Listing>(listingRequest);
        listing.Id = Guid.NewGuid();
        
        await _queueServiceClient.GetQueueClient("newlistings").SendMessageAsync(JsonConvert.SerializeObject(listing));

        ListingResponse listingResponse = _mapper.Map<ListingResponse>(listing);
        return listingResponse;
    }

    [HttpGet(Name = "Get all listings")]
    public async Task<IEnumerable<ListingResponse>> GetAllListings()
    {
        var listings = await _listingService.GetAllAsync();
        return _mapper.Map<IEnumerable<ListingResponse>>(listings);
    }

    [HttpGet("{id}", Name = "Get listing by ID")]
    public async Task<ListingResponse> GetListing(Guid id)
    {
        var listing = await _listingService.GetByIdAsync(id);
        return _mapper.Map<ListingResponse>(listing);
    }
    
}