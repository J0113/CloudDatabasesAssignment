using System.Linq.Expressions;
using CloudDatabasesAssignment.Models.Entities;

namespace CloudDatabasesAssignment.Api.Services.Interfaces;

public interface IListingService
{
    public Task<Listing> AddAsync(Listing listing);
    public Task<IEnumerable<Listing>> GetAllAsync();
    public Task<IEnumerable<Listing>> FindAll(Expression<Func<Listing, bool>> expression);
    public Task<Listing> GetByIdAsync(Guid id);
}