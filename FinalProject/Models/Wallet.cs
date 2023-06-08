using Microsoft.AspNetCore.Identity;

namespace FinalProject.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }=0;
        public object UserId { get; set; }
    }
}
