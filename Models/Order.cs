namespace Models;

public class Order
{
    public Guid Id {get;set;}
    public Guid UserId {get;set;}
    public Guid CartId {get;set;}
    public decimal Total {get;set;}
    public string? StripeSessionId { get; set; }

    public string Status { get; set; } = "Pending";

    public string PaymentIntent { get; set; } = "";

}
