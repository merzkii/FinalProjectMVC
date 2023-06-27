namespace FinalProject.Interfaces
{
    public interface ITokenRepository
    {
        Task<string> GetToken(string userId);
        Task SaveToken(string userId, string token);
    }
}
