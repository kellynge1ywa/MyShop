using Microsoft.EntityFrameworkCore;
using OrderService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add controller services
builder.Services.AddControllers();

//Add automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Configure base url for auth service
builder.Services.AddHttpClient("Users",k=>k.BaseAddress=new Uri(builder.Configuration.GetValue<string>("ServiceURL:CartService")));

//Connect to database
builder.Services.AddDbContext<AppDbContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myConnections"));
});

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


