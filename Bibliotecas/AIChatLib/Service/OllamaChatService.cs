using AIChatLib.Interfaces;
using AIChatLib.Models;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Ollama;
namespace AIChatLib.Service
{
    public class OllamaChatService : IChatService
    {
        private readonly IChatCompletionService _chatCompletionService;
        private readonly Kernel _kernel;

        public OllamaChatService(IChatCompletionService chatCompletionService, Kernel kernel)
        {
            _chatCompletionService = chatCompletionService;
            _kernel = kernel;
        }

        public async Task<string> SendAsync(InputClass input)
        {
            // Add user input
            input.chatHistory.AddUserMessage(input.Message);

            // Get the response from the AI
            var result = await _chatCompletionService.GetChatMessageContentAsync(
                input.chatHistory,
                kernel: _kernel);

            // Add the message from the agent to the chat history
            input.chatHistory.AddMessage(result.Role, result.Content ?? string.Empty);

            return result.Content ?? string.Empty;
        }
    }
}
