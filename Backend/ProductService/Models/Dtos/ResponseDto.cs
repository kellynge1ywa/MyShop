namespace ProductService;

public class ResponseDto
{
     public object Result {get;set;}= default!;
    public string Error {get;set;} ="";
    public bool Success {get;set;}=true;

}
