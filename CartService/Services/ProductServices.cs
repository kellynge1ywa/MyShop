
using Newtonsoft.Json;

namespace CartService;

public class ProductServices : IProduct
{
    private readonly IHttpClientFactory _httpClientFactory;
    public ProductServices(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory=httpClientFactory;
    }
    public async Task<ProductDto> GetOneProduct(Guid productId)
    {
         var client =_httpClientFactory.CreateClient("Products");
        var response=await client.GetAsync(productId.ToString());
        var content= await response.Content.ReadAsStringAsync();
        var responseDto= JsonConvert.DeserializeObject<ResponseDto>(content);
        if( response.IsSuccessStatusCode){
            return JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto.Result));
        }
        return new ProductDto();
    }
}
