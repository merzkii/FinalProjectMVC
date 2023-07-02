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
            string connectionString = _db.GetConnectionString("DefaultConnection");
            string sql = "INSERT INTO Tokens (UserId, PublicToken) " +
                         "VALUES (@UserId, @PublicToken)";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, new { UserId = userId, PublicToken = publicToken });
            }
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
