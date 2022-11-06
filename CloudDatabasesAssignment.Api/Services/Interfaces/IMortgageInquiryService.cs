using CloudDatabasesAssignment.Models.Entities;
using CloudDatabasesAssignment.Models.Enums;

namespace CloudDatabasesAssignment.Api.Services.Interfaces;

public interface IMortgageInquiryService
{
    public Task<MortgageInquiry> AddAsync(MortgageInquiry inquiry);
    public Task<IEnumerable<MortgageInquiry>> GetAllAsync();
    public Task<MortgageInquiry> GetByIdAsync(Guid id);
    public Task<MortgageInquiry> GenerateInquiry(Customer customer);
    public double CalculateRate(RateClass rateClass, int years);
    public int CalculateValue(int age, int yearlyIncome, int debtPayments);
    public int CalculateMonthlyPayment(double interest, int fixedYears, int value);
}