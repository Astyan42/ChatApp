using System;
using BLL;
using BLL.Handlers;
using DAL.Repositories;
using DomainObjects.Interfaces;
using DomainObjects.Interfaces.Handlers;
using DomainObjects.Interfaces.Repositories;
using DomainObjects.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication;
using WebSocketManager;

public class Startup
{
    public static IConfiguration Configuration { get; set; }
    
    public Startup(IHostingEnvironment env)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false)
            .AddEnvironmentVariables();
        Configuration = builder.Build();
    }

    public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
    {
        app.UseCors(builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
        app.UseWebSockets();

        app.UseMvc(routes =>
        {
            routes.MapRoute(
                name: "default",
                template: "{controller}/{action}/{id?}"
            );
        });

        app.MapWebSocketManager("/notifications", serviceProvider.GetService<NotificationsMessageHandler>());
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddWebSocketManager();
        services.AddSingleton<IUserHandler, UserHandler>();
        services.AddSingleton<IChatHandler, ChatHandler>();
        services.AddSingleton<IMessageRepository, MessageRepository>();
        services.AddCors();
        services.Configure<ConnectionSettings>(options =>
        {
            options.ConnectionString = Configuration.GetConnectionString("ChatDatabase");
            options.Database = "ChatNG";
        });
    }
}