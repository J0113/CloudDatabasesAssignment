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
[Route("customers")]
public class CustomerController : ControllerBase
{

    private readonly IMapper _mapper;
    private readonly ICustomerService _customerService;
    private readonly QueueServiceClient _queueServiceClient;
    
    public CustomerController(IMapper mapper, ICustomerService customerService, QueueServiceClient queueServiceClient)
    {
        _mapper = mapper;
        _customerService = customerService;
        _queueServiceClient = queueServiceClient;
    }

    [HttpPost(Name = "New customer")]
    public async Task<CustomerResponse> CreateNewCustomer(CustomerRequest customerRequest)
    {
        Customer customer = _mapper.Map<Customer>(customerRequest);
        customer.Id = Guid.NewGuid();
        
        await _queueServiceClient.GetQueueClient("newcustomers").SendMessageAsync(JsonConvert.SerializeObject(customer));

        CustomerResponse customerResponse = _mapper.Map<CustomerResponse>(customer);
        return customerResponse;
    }

    [HttpGet(Name = "List all customers")]
    public async Task<IEnumerable<CustomerResponse>> ListAllCustomers()
    {
        var customers = await _customerService.GetAllAsync();
        return _mapper.Map<IEnumerable<CustomerResponse>>(customers);
    }

    [HttpGet("{id}", Name = "Get customer by ID")]
    public async Task<CustomerResponse> GetCustomer(Guid id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        return _mapper.Map<CustomerResponse>(customer);
    }
    
}