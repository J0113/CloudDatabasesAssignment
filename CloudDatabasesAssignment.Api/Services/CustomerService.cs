using System.Linq.Expressions;
using CloudDatabasesAssignment.Api.Context;
using CloudDatabasesAssignment.Api.Services.Interfaces;
using CloudDatabasesAssignment.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudDatabasesAssignment.Api.Services;

public class CustomerService : ICustomerService
{

    private readonly ApiDbContext _dbContext;

    public CustomerService(ApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Customer> AddAsync(Customer inquiry)
    {
        await _dbContext.Set<Customer>().AddAsync(inquiry);
        await _dbContext.SaveChangesAsync();
        return inquiry;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _dbContext.Set<Customer>()
            .Include(x => x.Inquiry)
            .Include(x => x.Listing)
            .ToListAsync();
    }

    public async Task<IEnumerable<Customer>> FindAll(Expression<Func<Customer, bool>> expression)
    {
        return await _dbContext.Set<Customer>()
            .Where(expression)
            .Include(x => x.Inquiry)
            .Include(x => x.Listing)
            .ToListAsync();
    }

    public async Task<Customer> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<Customer>()
            .Include(x => x.Inquiry)
            .Include(x => x.Listing)
            .Where(e => e.Id == id).FirstAsync();
    }
}