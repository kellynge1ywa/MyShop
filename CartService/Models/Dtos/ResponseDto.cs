namespace CartService;

public class ResponseDto
{
     public string Error {get;set;} ="";
    public Object Result {get;set;} =default!;
    public bool Success {get;set;}= true;

}
