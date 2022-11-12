using Microsoft.Extensions.Azure;
using MountHebronAppApi.Mapper;
using MountHebronAppApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CORS Origin
builder.Services.AddCors(option =>
{
    option.AddPolicy(name: "ApartmentOrigin", policy =>
        {
            policy.WithOrigins("http://localhost:3000");
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowCredentials();
        });
}); 

builder.Services.AddSingleton<IStorageServices, StorageService>();
builder.Services.AddSingleton<IApartmentMapper, ApartmentMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(x=> x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));

app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(name: "default", pattern: "{controller}/{action}/{id?}");
//});

app.MapControllers();

app.Run();