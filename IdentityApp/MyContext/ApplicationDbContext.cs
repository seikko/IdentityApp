using IdentityApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.MyContext
{
    public class ApplicationDbContext:IdentityDbContext<AppUser,AppRole,string>//string guid 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
       
    }
}
