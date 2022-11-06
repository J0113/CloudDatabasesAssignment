using System.Linq.Expressions;
using CloudDatabasesAssignment.Api.Context;
using CloudDatabasesAssignment.Api.Services.Interfaces;
using CloudDatabasesAssignment.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudDatabasesAssignment.Api.Services;

public class ListingService : IListingService
{
    private readonly ApiDbContext _dbContext;

    public ListingService(ApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Listing> AddAsync(Listing listing)
    {
        await _dbContext.Set<Listing>().AddAsync(listing);
        await _dbContext.SaveChangesAsync();
        return listing;
    }
    
    public async Task<IEnumerable<Listing>> GetAllAsync()
    {
        return await _dbContext.Set<Listing>()
            .Include(x => x.PotentialBuyers)
            .ToListAsync();
    }

    public async Task<IEnumerable<Listing>> FindAll(Expression<Func<Listing, bool>> expression)
    {
        return await _dbContext.Set<Listing>()
            .Where(expression)
            .Include(x => x.PotentialBuyers)
            .ToListAsync();
    }

    public async Task<Listing> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<Listing>()
            .Include(x => x.PotentialBuyers)
            .Where(e => e.Id == id).FirstAsync();
    }
    
}