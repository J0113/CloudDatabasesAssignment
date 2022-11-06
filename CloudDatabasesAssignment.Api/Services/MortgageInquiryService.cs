using CloudDatabasesAssignment.Api.Context;
using CloudDatabasesAssignment.Api.Services.Interfaces;
using CloudDatabasesAssignment.Models.Entities;
using CloudDatabasesAssignment.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CloudDatabasesAssignment.Api.Services;

public class MortgageInquiryService : IMortgageInquiryService
{

    private readonly ApiDbContext _dbContext;

    public MortgageInquiryService(ApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MortgageInquiry> AddAsync(MortgageInquiry inquiry)
    {
        await _dbContext.Set<MortgageInquiry>().AddAsync(inquiry);
        await _dbContext.SaveChangesAsync();
        return inquiry;
    }

    public async Task<IEnumerable<MortgageInquiry>> GetAllAsync()
    {
        return await _dbContext.Set<MortgageInquiry>()
            .Include(x => x.Customer)
            .Where(x => x.CreationDate > DateTime.Today.AddDays(-7))
            .ToListAsync();
    }

    public async Task<MortgageInquiry> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<MortgageInquiry>()
            .Include(x => x.Customer)
            .Where(x => x.CreationDate > DateTime.Today.AddDays(-7))
            .Where(e => e.Id == id).FirstAsync();
    }

    public async Task<MortgageInquiry> GenerateInquiry(Customer customer)
    {
        var listing = customer.Listing;
        var downPayment = customer.DownPayment;
        var value = listing.Price - customer.DownPayment;
        var maxValue = CalculateValue(customer.Age, customer.YearlyIncome, customer.MonthlyDebtPayments);
        var years = 20;
        
        if (value < 0) value = 0;
        
        if (maxValue < value)
        {
            value = maxValue;
            downPayment = listing.Price - value;
        }

        RateClass rateClass = RateClass.ABOVE90P;
        var loanRatio = (double)value / listing.Price * 100;
        if (loanRatio < 90) rateClass = RateClass.BETWEEN67AND90P;
        if (loanRatio < 67) rateClass = RateClass.BELOW67P;

        var rate = CalculateRate(rateClass, years);

        var inquiry = new MortgageInquiry
        {
            CreationDate = DateTime.Now,
            Interest = rate,
            FixedYears = years,
            Value = value,
            MonthlyPayment = CalculateMonthlyPayment(rate, years, value),
            DownPayment = downPayment,
            CustomerId = customer.Id,
            Customer = customer
        };

        return await AddAsync(inquiry);
    }

    public double CalculateRate(RateClass rateClass, int years)
    {
        if (years < 1) years = 1;
        if (years > 30) years = 30;
        double baseRate = (1 + (years / 90.0)) * 3.62;
        double extraRate = (int)rateClass * 0.18;
        return Math.Round(baseRate + extraRate, 2);
    }

    public int CalculateValue(int age, int yearlyIncome, int debtPayments)
    {
        double agePenalty = age > 63 ? 0.8 : 1;
        int amount = (int)(agePenalty * yearlyIncome * 5 - 25000 - debtPayments * 50);
        if (amount < 10000) amount = 0;
        return amount;
    }

    public int CalculateMonthlyPayment(double interest, int fixedYears, int value)
    {
        var rateOfInterest = interest / 1200;
        var numberOfPayments = fixedYears * 12;

        var paymentAmount = (rateOfInterest * value) / (1 - Math.Pow(1 + rateOfInterest, numberOfPayments * -1));

        return (int)paymentAmount;
    }
}