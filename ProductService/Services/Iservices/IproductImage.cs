namespace ProductService;

public interface IproductImage
{
    Task<string> AddProductImage(Guid Id, ProductImages productImages);

}
