
using Microsoft.SemanticKernel.ChatCompletion;

namespace AIChatLib.Models
{
    public class InputClass
    {
        public string Message { get; set; }
        public string LastAnswer { get; set; }
        public ChatHistory chatHistory { get; set; } = new ChatHistory();

    }
}
