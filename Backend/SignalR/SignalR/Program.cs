using MediatR;
using SignalR.Hub;
using SignalR.Hub.Groups;
using SignalR.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

const string corsAlllowedOrigin = "CorsAllowedOrigins";

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddSignalR(hubOptions =>
{
    hubOptions.EnableDetailedErrors = true;
});

services.AddScoped<INotificationService, NotificationService>();
services.AddSingleton<IHubGroupManager, HubGroupManager>();
//services.AddSingleton<ComunicationHub>();
services.AddSingleton<IComunicationHub, ComunicationHub>();

services
    .AddMediatR(Assembly.GetExecutingAssembly())
    .AddCors(options =>
        options.AddPolicy(corsAlllowedOrigin, policyBuilder =>
            policyBuilder.WithOrigins(builder.Configuration
                .GetValue<string>(corsAlllowedOrigin)!.Split(","))
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials())
    )
    .AddSwaggerGen(options =>
    {
        options.CustomSchemaIds(type => $"{type.ReflectedType?.Name}.{type.Name}");
        options.UseInlineDefinitionsForEnums();
    })
    .AddRouting(options => options.LowercaseUrls = true)
    .AddControllers();
services.AddEndpointsApiExplorer();


WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment()) 
{
    app.UseHttpsRedirection();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.Use(async (context, next) => {
//    var hubContext = context.RequestServices.GetRequiredService<IComunicationHub>();
//    if(next != null)
//    {
//        await next.Invoke();
//    }
//});


app.UseRouting()
    .UseCors(corsAlllowedOrigin)
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHub<ComunicationHub>(ComunicationHub.Endpoint);
    });

app.Run();
