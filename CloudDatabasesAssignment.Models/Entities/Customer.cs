using System.ComponentModel.DataAnnotations.Schema;

namespace CloudDatabasesAssignment.Models.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public int YearlyIncome { get; set; }
    public int MonthlyDebtPayments { get; set; }
    public int DownPayment { get; set; }
    public virtual MortgageInquiry? Inquiry { get; set; }
    public Guid ListingId { get; set; }
    [ForeignKey(nameof(ListingId))]
    public virtual Listing? Listing { get; set; }
}