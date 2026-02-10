var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/health", () => Results.Ok());

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/auth/swagger/v1/swagger.json", "Auth API");
    options.SwaggerEndpoint("/dishes/swagger/v1/swagger.json", "Dishes API");
    options.SwaggerEndpoint("/orders/swagger/v1/swagger.json", "Orders API");

    options.RoutePrefix = "gateway-docs";
});

app.MapReverseProxy();

app.Run();