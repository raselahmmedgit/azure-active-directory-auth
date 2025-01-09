using Microsoft.AspNetCore.Identity;

namespace lab.azure_active_directory_auth.Core.Identity
{
    public class ApplicationUser : IdentityUser<string>
    {
        public ApplicationUser() { }
    }
}
