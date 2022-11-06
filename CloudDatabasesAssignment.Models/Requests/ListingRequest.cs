namespace CloudDatabasesAssignment.Models.Requests;
public class ListingRequest : BaseEntityRequest {
    public int Price { get; set; } 
    public string LinkToWebsite { get; set; }
}
