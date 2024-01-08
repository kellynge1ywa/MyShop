using CartService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add controller services
builder.Services.AddControllers();

//Add automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Services for dependency injection
builder.Services.AddScoped<Icart,CartServices>();
builder.Services.AddScoped<IProduct, ProductServices>();
builder.Services.AddScoped<Icoupon, CouponServices>();

//Configure base url for  product services
builder.Services.AddHttpClient("Products",k=>k.BaseAddress=new Uri(builder.Configuration.GetValue<string>("ServiceURL:ProductService")));
//Configure base url for  coupon services
builder.Services.AddHttpClient("Coupons",k=>k.BaseAddress=new Uri(builder.Configuration.GetValue<string>("ServiceURL:CouponService")));
//Configure base url for auth service
builder.Services.AddHttpClient("Users",k=>k.BaseAddress=new Uri(builder.Configuration.GetValue<string>("ServiceURL:AuthService")));

//Connect to database
builder.Services.AddDbContext<ShopDbContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myConnections"));
});

builder.Services.AddHttpContextAccessor();

//Add swagger extension
builder.AddSwaggenGenExtension();

//Add authentication bearer extension
builder.AddAuth();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseMigrations();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


