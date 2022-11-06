using System.ComponentModel.DataAnnotations.Schema;

namespace CloudDatabasesAssignment.Models.Responses;

public class MortgageInquiryResponse : BaseEntityResponse
{
    public double Interest { get; set; }
    public int FixedYears { get; set; }
    public int Value { get; set; }
    public int MonthlyPayment { get; set; }
    public int DownPayment { get; set; }
    public virtual CustomerResponse Customer { get; set; }
}