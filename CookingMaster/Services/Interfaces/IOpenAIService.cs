
namespace CookingMaster.Services.Interfaces
{
    public interface IOpenAIService
    {
        Task<string> GetAnswerAsync(string question);
    }
}
