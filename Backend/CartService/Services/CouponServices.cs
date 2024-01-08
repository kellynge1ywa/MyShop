
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace CartService;

public class CouponServices : Icoupon
{
    private readonly IHttpClientFactory _httpClientFactory;
    public CouponServices(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory=httpClientFactory;
    }
    public async Task<CouponDto> GetCouponByCode(string CouponCode,string Token)
    {
        var client =_httpClientFactory.CreateClient("Coupons");
        client.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer",Token);
        var response=await client.GetAsync(CouponCode);
        var content= await response.Content.ReadAsStringAsync();
        var responseDto= JsonConvert.DeserializeObject<ResponseDto>(content);
        if(responseDto.Result!=null || response.IsSuccessStatusCode){
            return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto.Result));
        }
        return new CouponDto();
    }
}
