using CloudDatabasesAssignment.Api.Context;
using CloudDatabasesAssignment.Api.Services;
using CloudDatabasesAssignment.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s =>
    {
        var serverVersion = new MySqlServerVersion(new Version(5, 7, 32));
        s.AddDbContext<ApiDbContext>(options => options.UseMySql(Environment.GetEnvironmentVariable("MySQL"), serverVersion));
        
        s.AddTransient<ICustomerService, CustomerService>();
        s.AddTransient<IListingService, ListingService>();
        s.AddTransient<IMortgageInquiryService, MortgageInquiryService>();
        
        s.AddAzureClients(b =>
        {
            b.AddQueueServiceClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage"))
                .ConfigureOptions(c => c.MessageEncoding = Azure.Storage.Queues.QueueMessageEncoding.Base64);
        });

    })
    .Build();

host.Run();