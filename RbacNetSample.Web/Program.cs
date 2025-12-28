using AcAuthNetSample.Core.Application;
using Microsoft.AspNetCore.Mvc.Filters;
using RbacNetSample.Web.Configuration.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers(options => {
    options.Filters.Add<ApiExceptionFilter>();
});

builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "RBAC_NET_Sample";
});
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddCors(options => {
    options.AddPolicy("DevCors", policy => {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("DevCors");

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
