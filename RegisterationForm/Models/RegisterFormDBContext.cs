using Microsoft.EntityFrameworkCore;

namespace RegisterationForm.Models
{
    public class RegisterFormDBContext : DbContext
    {
        public RegisterFormDBContext(DbContextOptions<RegisterFormDBContext> options) : base(options) { }
        public DbSet<Registerations> RegisterationForms { get; set; }
    }
}
