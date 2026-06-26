using Microsoft.EntityFrameworkCore;
using SimpleLALPrint;

namespace TestServer;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
    }

    public DbSet<ItemToDo> Items => Set<ItemToDo>();
}