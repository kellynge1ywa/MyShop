using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
// using Blazored.LocalStorage;
using System.Text.Json;

namespace MyShopUi.Services.AuthProvider;

public class CustomAuthProvider:AuthenticationStateProvider
{
    private ILocalStorageService _localStorage;
    private readonly HttpClient http;
    public CustomAuthProvider(ILocalStorageService localStorageService, HttpClient httpClient)
    {
        _localStorage=localStorageService;
        http=httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        //read token from localstorage
        string AuthToken=await _localStorage.GetItemAsStringAsync("AuthToken");
        var identity= new ClaimsIdentity();
        http.DefaultRequestHeaders.Authorization=null;
        if(!string.IsNullOrEmpty(AuthToken))
        {
            try
            {
                identity=new ClaimsIdentity(ParseClaimsFromJwt(AuthToken),"jwt");
                http.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer",AuthToken);

            } catch(Exception ex)
            {
                await _localStorage.RemoveItemAsync("AuthToken");
                identity= new ClaimsIdentity();

            }

        }
        var user= new ClaimsPrincipal(identity);
        var state=new  AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(state));
        return state;
    }

    private IEnumerable<Claim>? ParseClaimsFromJwt(string jwt)
    {
        var claims=new List<Claim>();
        var payload=jwt.Split('.')[1];

        var jsonbytes=ParseBase64WithoutPadding(payload);
        var keyValuePairs=JsonSerializer.Deserialize<Dictionary<string, object>>(jsonbytes);
        claims.AddRange(keyValuePairs.Select(kvp=> new Claim(kvp.Key,kvp.Value.ToString())));
        return claims;
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch(base64.Length % 4)
        {
            case 2:base64 += "=="; break;
            case 3:base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}
