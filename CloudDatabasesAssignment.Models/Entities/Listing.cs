using System.ComponentModel.DataAnnotations.Schema;

namespace CloudDatabasesAssignment.Models.Entities;

public class Listing : BaseEntity
{
    public int Price { get; set; }
    public string LinkToWebsite { get; set; }
    public virtual ICollection<Customer> PotentialBuyers { get; set; }
}
