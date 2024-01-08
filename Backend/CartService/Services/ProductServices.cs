
using System.Net.Http.Headers;
using Models;
using Newtonsoft.Json;

namespace CartService;

public class ProductServices : IProduct
{
    private readonly IHttpClientFactory _httpClientFactory;
    public ProductServices(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory=httpClientFactory;
    }
    public async Task<Product> GetOneProduct(Guid productId, string Token)
    {
         var client =_httpClientFactory.CreateClient("Products");
         client.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer",Token);
        var response=await client.GetAsync($"{productId}");
        var content= await response.Content.ReadAsStringAsync();
        var responseDto= JsonConvert.DeserializeObject<ResponseDto>(content);
        if( response.IsSuccessStatusCode){
            return JsonConvert.DeserializeObject<Product>(Convert.ToString(responseDto.Result));
        }
        return new Product();
    }
}
