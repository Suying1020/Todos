using Microsoft.AspNetCore.Identity;

namespace Todos.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string UserFullName { get; set; }    
    }
}
