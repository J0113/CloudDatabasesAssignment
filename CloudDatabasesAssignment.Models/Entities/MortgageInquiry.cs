using System.ComponentModel.DataAnnotations.Schema;

namespace CloudDatabasesAssignment.Models.Entities;

public class MortgageInquiry : BaseEntity
{
    public DateTime CreationDate { get; set; }
    public double Interest { get; set; }
    public int FixedYears { get; set; }
    public int Value { get; set; }
    public int MonthlyPayment { get; set; }
    
    public int DownPayment { get; set; }

    public Guid? CustomerId { get; set; }
    
    [ForeignKey(nameof(CustomerId))]
    public virtual Customer? Customer { get; set; }

}