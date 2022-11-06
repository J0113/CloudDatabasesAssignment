using System;
using CloudDatabasesAssignment.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudDatabasesAssignment.Api.Context;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MortgageInquiry>()
            .HasOne<Customer>(x => x.Customer)
            .WithOne(y => y.Inquiry);

        modelBuilder.Entity<Customer>()
            .HasOne<Listing>(x => x.Listing)
            .WithMany(y => y.PotentialBuyers);
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Listing> Listings { get; set; }
    public DbSet<MortgageInquiry> MortgageInquiries { get; set; }

}