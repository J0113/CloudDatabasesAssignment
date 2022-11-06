using System;
using System.Net;
using System.Net.Mail;
using Azure.Storage.Queues;
using CloudDatabasesAssignment.Api.Services.Interfaces;
using CloudDatabasesAssignment.Models.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CloudDatabasesAssignment.Functions;

public class SendEmails
{
    private readonly IMortgageInquiryService _mortgageInquiryService;
    private readonly SmtpClient _smtpClient;

    public SendEmails(IMortgageInquiryService mortgageInquiryService)
    {
        _mortgageInquiryService = mortgageInquiryService;

        _smtpClient = new SmtpClient(Environment.GetEnvironmentVariable("SmtpHost"), int.Parse(Environment.GetEnvironmentVariable("SmtpPort")))
        {
            Credentials = new NetworkCredential(Environment.GetEnvironmentVariable("SmtpUser"), Environment.GetEnvironmentVariable("SmtpPass")),
            EnableSsl = false
        };
    }

    [Function("SendEmails")]
    public async Task Run([QueueTrigger("sendinquiries", Connection = "AzureWebJobsStorage")] Guid guid, FunctionContext context)
    {
        MortgageInquiry inquiry = await _mortgageInquiryService.GetByIdAsync(guid);
        await SendInquiryEmail(inquiry);
    }

    private async Task SendInquiryEmail(MortgageInquiry inquiry)
    {
        var msg = "Your mortgage inquiry is ready. View it online!";
        msg += "\n\n";
        msg += $"\nRate: {inquiry.Interest}";
        msg += $"\nMonthly payments: {inquiry.MonthlyPayment}";
        msg += $"\nYears: {inquiry.FixedYears}";

        _smtpClient.Send(
            Environment.GetEnvironmentVariable("SmtpFrom"), 
            inquiry.Customer.Email, 
            "Your mortgage inquiry",
            msg);
    }
    
}