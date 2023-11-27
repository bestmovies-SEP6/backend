using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class DatabaseContext : DbContext {
    public DbSet<User> Users { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {
    }
}