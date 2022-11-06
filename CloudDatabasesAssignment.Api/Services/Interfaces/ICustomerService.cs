using System.Linq.Expressions;
using CloudDatabasesAssignment.Models.Entities;

namespace CloudDatabasesAssignment.Api.Services.Interfaces;

public interface ICustomerService
{
    public Task<Customer> AddAsync(Customer inquiry);
    public Task<IEnumerable<Customer>> GetAllAsync();
    public Task<IEnumerable<Customer>> FindAll(Expression<Func<Customer, bool>> expression);
    public Task<Customer> GetByIdAsync(Guid id);
}