using AuthService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add controller services
builder.Services.AddControllers();

//Configure identity framework
builder.Services.AddIdentity<ShopUser, IdentityRole>().AddEntityFrameworkStores<ShopDbContext>();

//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Connect to database
builder.Services.AddDbContext<ShopDbContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myConnections"));
});

//set cors policy
builder.Services.AddCors(options=>options.AddPolicy("policy1", build=>{
    build.AllowAnyOrigin();
    build.AllowAnyHeader();
    build.AllowAnyMethod();
}));

//Adding our services
builder.Services.AddScoped<IShopUser, ShopUserService>();
builder.Services.AddScoped<Ijwt,JwtService>();

//Configure jwt classes
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("policy1");



app.Run();

