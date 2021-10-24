using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ToDoListAapp.View;
using ToDoListApp.Data;
using ToDoListApp.Services;

namespace ToDoListApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices((hostBuilderContext, services) =>
            {
                services.AddSingleton<UserService>();
                services.AddSingleton<ToDoListService>();
                services.AddSingleton<TaskService>();
                services.AddSingleton<IndexView>();
                services.AddSingleton<ToDoListStorage>();
                services.AddSingleton<TaskStorage>();
                services.AddSingleton<UserStorage>();
            }).UseConsoleLifetime().Build();

            var indexView = host.Services.GetRequiredService<IndexView>(); //authentification + interface
           indexView.Render();
            int x;
        }
    }
}
