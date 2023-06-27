using Dapper;
using FinalProject.Interfaces;
using Microsoft.Data.SqlClient;

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
                var query = "SELECT TokenValue FROM Tokens WHERE UserId = @UserId";
                var parameters = new { UserId = userId };

                return await connection.QuerySingleOrDefaultAsync<string>(query, parameters);
            }
        }
    

        public async Task SaveToken(string userId, string token)
            {
                string connectionString = _db.GetConnectionString("DefaultConnection");
                string sql = "INSERT INTO Tokens (UserId, TokenValue, TokenPurpose, ExpirationDate) " +
                             "VALUES (@UserId, @TokenValue, 'ResetPassword', DATEADD(DAY, 1, GETDATE()))";

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.ExecuteAsync(sql, new { UserId = userId, TokenValue = token });
                }
            }
        
    }
}
