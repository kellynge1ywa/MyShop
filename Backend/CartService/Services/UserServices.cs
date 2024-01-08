using Newtonsoft.Json;

namespace CartService;

public class UserServices:Iuser
{
    private readonly IHttpClientFactory _httpClientFactory;
    public UserServices(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory=httpClientFactory;
    }
    public async Task<ShopUserDto> GetUserById(Guid Id)
    {
         var client=_httpClientFactory.CreateClient("Users");
        var response= await client.GetAsync($"{Id}");
        var content= await response.Content.ReadAsStringAsync();//We get content inform of a string
        var responseDto=JsonConvert.DeserializeObject<ResponseDto>(content);
        if(response.IsSuccessStatusCode){
            return JsonConvert.DeserializeObject<ShopUserDto>(Convert.ToString(responseDto.Result));
        }
        return new ShopUserDto();
    }

}
