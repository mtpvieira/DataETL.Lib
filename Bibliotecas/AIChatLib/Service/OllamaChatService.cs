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

        public async Task<InputClass> SendAsync(InputClass modelData)
        {
            // Add user input
            modelData.chatHistory.AddUserMessage(modelData.Message);

            // Get the response from the AI
            var result = await _chatCompletionService.GetChatMessageContentAsync(
                modelData.chatHistory,
                kernel: _kernel);

            // Add the message from the agent to the chat history
            modelData.chatHistory.AddMessage(result.Role, result.Content ?? string.Empty);
            modelData.LastAnswer = result.Content ?? string.Empty;

            return modelData;
        }
    }
}
