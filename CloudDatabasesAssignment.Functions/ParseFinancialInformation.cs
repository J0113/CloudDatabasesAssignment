using Azure.Storage.Queues;
using CloudDatabasesAssignment.Api.Services.Interfaces;
using CloudDatabasesAssignment.Models.Entities;
using CloudDatabasesAssignment.Models.Enums;
using Microsoft.Azure.Functions.Worker;
using Newtonsoft.Json;

namespace CloudDatabasesAssignment.Functions;

public class ParseFinancialInformation
{
    private readonly IMortgageInquiryService _mortgageInquiryService;
    private readonly ICustomerService _customerService;
    private readonly QueueServiceClient _queueServiceClient;


    public ParseFinancialInformation(IMortgageInquiryService mortgageInquiryService, ICustomerService customerService, QueueServiceClient queueServiceClient)
    {
        _mortgageInquiryService = mortgageInquiryService;
        _customerService = customerService;
        _queueServiceClient = queueServiceClient;
    }

    [Function("ParseFinancialInformation")]
    public async Task Run([TimerTrigger("0 * * * * *")] FunctionContext context)
    {
        var result = await _customerService.FindAll(x => x.Inquiry == null);
        var customers = result.ToList();
        
        if (customers.Any())
        {
            foreach (Customer customer in customers)
            {
                var inquiry = await _mortgageInquiryService.GenerateInquiry(customer);
                await _queueServiceClient.GetQueueClient("sendinquiries").SendMessageAsync(inquiry.Id.ToString());
            }
        }
    }
}
