using CloudDatabasesAssignment.Api.Services.Interfaces;
using CloudDatabasesAssignment.Models.Entities;
using Microsoft.Azure.Functions.Worker;

namespace CloudDatabasesAssignment.Functions;

public class SaveNewCustomers
{
    private readonly ICustomerService _customerService;

    public SaveNewCustomers(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [Function("SaveNewCustomers")]
    public async Task Run([QueueTrigger("newcustomers", Connection = "AzureWebJobsStorage")] Customer newCustomer, FunctionContext context)
    {
        await _customerService.AddAsync(newCustomer);
    }
}