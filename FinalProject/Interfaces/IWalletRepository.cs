using FinalProject.Models;

namespace FinalProject.Interfaces
{
    public interface IWalletRepository
    {
        Task<decimal> GetBalanceAsync(string userId);
        Task AddMoneyToWallet(string userId, decimal amount);
        Task WithdrawMoneyFromWallet(string userId, decimal amount);
        Task <int> CreateWallet(string userId);
        Task<List<Transaction>> GetTransactionHistory(string userId, DateTime fromDate, DateTime toDate);
        Task RecordTransaction(string userId, decimal amount, string transactionType);
    }
}
