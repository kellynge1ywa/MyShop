using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyShop;
using MyShop.Services.Auth;
using MyShop.Services.AuthProvider;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//Add localstorage
builder.Services.AddBlazoredLocalStorage();
//services
builder.Services.AddScoped<IuserAuth,UserAuthService>();

//configuration for auth provider
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider,CustomAuthProvider>();

await builder.Build().RunAsync();
