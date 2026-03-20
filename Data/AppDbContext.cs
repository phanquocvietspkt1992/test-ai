using Microsoft.EntityFrameworkCore;
using test_ai.Models;

namespace test_ai.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TodoItem> Todos => Set<TodoItem>();
}
