using Microsoft.AspNetCore.Identity;

namespace ChoreLore.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int TotalGold { get; set; }
    }

}
