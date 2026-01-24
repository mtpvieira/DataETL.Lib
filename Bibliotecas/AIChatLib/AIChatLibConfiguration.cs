using AIChatLib.Interfaces;
using AIChatLib.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;


namespace AIChatLib
{
    public static class AIChatLibConfiguration
    {
        public static IKernelBuilder AddChatKernel(this IKernelBuilder services, IConfiguration config)
        {
            var builder = Kernel.CreateBuilder().AddOllamaChatCompletion(
                modelId: config["AI_CONFIG:model"]!,
                endpoint: new Uri(config["AI_CONFIG:endpoint"]!)
            );

            return builder;
        }
    
        public static IServiceCollection AddCharService(IServiceCollection services)
        {
            // Register your chat service implementations here
            services.AddSingleton<IChatService, OllamaChatService>();
            return services;
        }
    }
}
