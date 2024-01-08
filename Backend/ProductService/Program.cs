using Microsoft.EntityFrameworkCore;
using ProductService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add controller services
builder.Services.AddControllers();

//Add automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//
builder.Services.AddScoped<Iproduct,ProductsService>();
builder.Services.AddScoped<IproductImage,ProductImageService>();

//Connect to database
builder.Services.AddDbContext<ShopDbContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myConnections"));
});
//Add swagger extension
builder.AddSwaggenGenExtension();
//Custome services - from extensions folder
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


