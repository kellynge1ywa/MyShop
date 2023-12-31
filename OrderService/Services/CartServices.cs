using Models;
using Newtonsoft.Json;

namespace OrderService;

public class CartServices : ICart
{
    private readonly IHttpClientFactory _httpClientFactory;
    public CartServices(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory=httpClientFactory;
    }
    public async Task<Cart> GetCartById(Guid CartId)
    {
          var client =_httpClientFactory.CreateClient("Carts");
        var response=await client.GetAsync($"{CartId}");
        var content= await response.Content.ReadAsStringAsync();
        var responseDto= JsonConvert.DeserializeObject<ResponseDto>(content);
        if( response.IsSuccessStatusCode){
            return JsonConvert.DeserializeObject<Cart>(Convert.ToString(responseDto.Result));
        }
        return new Cart();
       
    }
}
