using FinalProject.Interfaces;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using FinalProject.Models;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.repo
{
    public class WalletRepository : IWalletRepository
    {
        private readonly IConfiguration _db;


        public WalletRepository(IConfiguration db)
        {
            _db = db;
        }
        public async Task<int> CreateWallet(string userId)
        {

            const string query = "INSERT INTO Wallet (Balance,UserId) VALUES (@Balance,@userId); SELECT CAST(SCOPE_IDENTITY() as int)";
            using (var connection = new SqlConnection(_db.GetConnectionString("DefaultConnection")))
            {
                var wallet = new Wallet
                {
                    UserId = userId,
                    Balance = 0
                };

                var walletId = await connection.QuerySingleOrDefaultAsync<int>(query, wallet);
                return walletId;

            }

        }

        public async Task<decimal> GetBalanceAsync(string userId)
        {
            using var connection = new SqlConnection(_db.GetConnectionString("DefaultConnection"));
            const string query = "SELECT Balance FROM Wallet WHERE UserId = @UserId";
            var parameters = new { UserId = userId };

            var balance = await connection.QuerySingleOrDefaultAsync<decimal>(query, parameters);

            return balance;
        }
        public async Task AddMoneyToWallet(string userId, decimal amount)
        {
            using var connection = new SqlConnection(_db.GetConnectionString("DefaultConnection"));
            var sql = "UPDATE Wallet SET Balance = Balance + @Amount WHERE UserId = @UserId";
            await connection.ExecuteAsync(sql, new { UserId = userId, Amount = amount });
        }

        public async Task WithdrawMoneyFromWallet(string userId, decimal amount)
        {
            using var connection = new SqlConnection(_db.GetConnectionString("DefaultConnection"));
            var sql = "UPDATE Wallet SET Balance = Balance - @Amount WHERE UserId = @UserId";
            await connection.ExecuteAsync(sql, new { UserId = userId, Amount = amount });
        }

        public async Task RecordTransaction(string userId, decimal amount, string transactionType)
        {
            using var connection = new SqlConnection(_db.GetConnectionString("DefaultConnection"));
            var sql = "INSERT INTO TransactionHistory (UserId, Amount, TransactionType, TransactionDate) VALUES (@UserId, @Amount, @TransactionType, GETDATE())";
            await connection.ExecuteAsync(sql, new { UserId = userId, Amount = amount, TransactionType = transactionType });

        }

        public async Task<List<Transaction>> GetTransactionHistory(string userId, DateTime fromDate, DateTime toDate)
        {
            using var connection = new SqlConnection(_db.GetConnectionString("DefaultConnection"));
            var sql = "SELECT * FROM TransactionHistory WHERE UserId = @UserId AND TransactionDate >= @FromDate AND TransactionDate <= @ToDate";
            var parameters = new { UserId = userId, FromDate = fromDate, ToDate = toDate };
            var transactionHistory = await connection.QueryAsync<Transaction>(sql, parameters);
            return transactionHistory.ToList();
        }
    
    }

}

