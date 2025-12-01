using AIChatLib.Models;

namespace AIChatLib.Interfaces
{
    public interface IChatService
    {
        Task<InputClass> SendAsync(InputClass input);
    }
}