using AIChatLib.Interfaces;
using AIChatLib.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace TesterDebug
{
    internal static class Program
    {
        private async static Task Main(string[] args)
        {
            Console.WriteLine("Hello, welcome to llama talk!");
            Console.WriteLine();
            Console.WriteLine();
            var host = CreateHostBuilder(args).Build();

            var chatService = host.Services.GetRequiredService<IChatService>();

            var chat = new AIChatLib.Models.InputClass
            {
                Message = "",
            };

            do
            {
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("Type your message...");
                var inputText = Console.ReadLine();

                if (inputText == "exit" || inputText == "quit")
                {
                    break;
                }
                else if (string.IsNullOrWhiteSpace(inputText))
                {
                    continue;
                }

                chat.Message = inputText;
                chat = await chatService.SendAsync(chat);

                Console.WriteLine("Response from chat service:" + chat.LastAnswer);
            } while (true);
        }

        private static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton(context.Configuration);
                services.AddScoped<IChatService, OllamaChatService>();

                services.AddTransient(sp => new Kernel(sp));

                // Create a kernel with Azure OpenAI chat completion
                var builder = Kernel.CreateBuilder().AddOllamaChatCompletion(
                    modelId: "llama3.1:latest",
                    endpoint: new Uri("http://localhost:11434")
                );

                // Add enterprise components
                builder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Information));

                // Build the kernel
                Kernel kernel = builder.Build();
                services.AddScoped(x => kernel.GetRequiredService<IChatCompletionService>());
            });
    }
}
