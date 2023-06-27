namespace FinalProject.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string TokenValue { get; set; }
        public string TokenPurpose { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
