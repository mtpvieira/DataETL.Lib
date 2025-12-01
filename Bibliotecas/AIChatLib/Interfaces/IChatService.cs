using AIChatLib.Models;

namespace AIChatLib.Interfaces
{
    public interface IChatService
    {
        Task<string> SendAsync(InputClass input);
    }
}