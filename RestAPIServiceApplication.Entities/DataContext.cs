using System.Data.Entity;

namespace RestAPIServiceApplication.Entities
{
    public class DataContext : DbContext // problem with EF6.4
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        public DbSet<UserModel> Users { get; set; }
    }
}
