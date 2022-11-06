using Azure.Storage.Queues;
using CloudDatabasesAssignment.Api.Services.Interfaces;
using CloudDatabasesAssignment.Models.Entities;
using Microsoft.Azure.Functions.Worker;

namespace CloudDatabasesAssignment.Functions;

public class SaveNewInquiries
{
    private readonly IMortgageInquiryService _mortgageInquiryService;

    public SaveNewInquiries(IMortgageInquiryService mortgageInquiryService)
    {
        _mortgageInquiryService = mortgageInquiryService;
    }

    [Function("SaveNewInquiries")]
    public async Task Run([QueueTrigger("newinquiries", Connection = "AzureWebJobsStorage")] MortgageInquiry newInquiry, FunctionContext context)
    {
        await _mortgageInquiryService.AddAsync(newInquiry);
    }
}