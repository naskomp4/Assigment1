using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ToDoListAapp.View;
using ToDoListApp.Data;
using ToDoListApp.Services;
using ToDoListApp.View.ViewHelper;

namespace ToDoListApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices((hostBuilderContext, services) =>
            {
                services.AddSingleton<UserService>();
                services.AddSingleton<UserStorage>();
                services.AddSingleton<ToDoListService>();
                services.AddSingleton<ToDoListStorage>();
                services.AddSingleton<TaskService>();
                services.AddSingleton<TaskStorage>();
                services.AddSingleton<ConsoleWriter>();
                services.AddSingleton<IndexView>();
            }).UseConsoleLifetime().Build();

            var indexView = host.Services.GetRequiredService<IndexView>();
           indexView.Render();
        }
    }
}
