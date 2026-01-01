using AcAuthNetSample.Core.Application;
using RbacNetSample.Web.Configuration.Converters;
using RbacNetSample.Web.Configuration.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers(options => {
    options.Filters.Add<ApiExceptionFilter>();
})
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
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
