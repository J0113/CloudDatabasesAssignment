namespace CloudDatabasesAssignment.Models.Responses;

public class CustomerResponse : BaseEntityResponse
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public int YearlyIncome { get; set; }
    public int MonthlyDebtPayments { get; set; }
    public int DownPayment { get; set; }
    public Guid ListingId { get; set; }
}