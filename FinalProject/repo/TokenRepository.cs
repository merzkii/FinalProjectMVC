using Dapper;
using FinalProject.Interfaces;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace FinalProject.repo
{
    public class TokenRepository:ITokenRepository
    {
        private readonly IConfiguration _db;


        public TokenRepository(IConfiguration db)
        {
            _db = db;
        }

        public async Task<string> GetToken(string userId)
        {
            using (var connection = new SqlConnection(_db.GetConnectionString("DefaultConnection")))
            {
                var query = "SELECT PublicToken FROM Tokens WHERE UserId = @UserId";
                var parameters = new { UserId = userId };
                return await connection.QuerySingleOrDefaultAsync<string>(query, parameters);
            }
        }

        public async Task SaveToken(string userId, string publicToken)
        {
            string privateToken = GeneratePrivateToken();
            DateTime expireDate = DateTime.UtcNow.AddMinutes(30); // Example: Set an expiration date/time for the public token

            string connectionString = _db.GetConnectionString("DefaultConnection");
            string sql = "INSERT INTO Tokens (UserId, PublicToken, PrivateToken, PrivateTokenStatus,PublicTokenStatus) " +
                         "VALUES (@UserId, @PublicToken, @PrivateToken,@IsActive,@IsActive)";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, new { UserId = userId, PublicToken = publicToken, PrivateToken = privateToken, PrivateTokenStatus = true,PublicTokenStatus=true,IsActive=true });
            }
        }
        private string GeneratePrivateToken()
        {
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const int tokenLength = 16; // Adjust the length of the token as needed

            StringBuilder tokenBuilder = new StringBuilder();
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenBytes = new byte[tokenLength];
                rng.GetBytes(tokenBytes);

                foreach (byte b in tokenBytes)
                {
                    tokenBuilder.Append(allowedChars[b % allowedChars.Length]);
                }
            }

            return tokenBuilder.ToString();
        }
    

        public string GeneratePublicToken()
        {
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const int tokenLength = 16; // Adjust the length of the token as needed

            StringBuilder tokenBuilder = new StringBuilder();
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenBytes = new byte[tokenLength];
                rng.GetBytes(tokenBytes);

                foreach (byte b in tokenBytes)
                {
                    tokenBuilder.Append(allowedChars[b % allowedChars.Length]);
                }
            }

            return tokenBuilder.ToString();
        }
    }
}
