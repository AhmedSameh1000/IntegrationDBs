using Microsoft.EntityFrameworkCore;
namespace Integration.data.Data
{
    public class ToDbContext : DbContext
    {
        public ToDbContext(DbContextOptions<ToDbContext> options) : base(options)
        {
        }
    }


}

