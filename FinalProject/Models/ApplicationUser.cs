using Microsoft.AspNetCore.Identity;

namespace FinalProject.Models
{
    public class ApplicationUser:IdentityUser
    {
        public int WalletId { get; set; }
    }
}
