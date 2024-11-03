using Microsoft.EntityFrameworkCore;
namespace Integration.data.Data
{
    public class FromDbContext : DbContext
    {
        public FromDbContext(DbContextOptions<FromDbContext> options) : base(options)
        {
        }
    }


}

