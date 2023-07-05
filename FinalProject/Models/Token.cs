namespace FinalProject.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        
        public string PublicToken { get; set; }
        public string PrivateToken { get; set; }
        public bool PublicTokenStatus { get; set; }
        public bool PrivateTokenStatus { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
