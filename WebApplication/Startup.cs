using System;
using BLL;
using BLL.Handlers;
using DomainObjects.Interfaces;
using DomainObjects.Interfaces.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebApplication;
using WebSocketManager;

public class Startup
{
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
        services.AddCors();
    }
}