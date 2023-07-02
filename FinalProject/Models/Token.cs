namespace FinalProject.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        
        public Guid PublicToken { get; set; }
        public Guid PrivateToken { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
