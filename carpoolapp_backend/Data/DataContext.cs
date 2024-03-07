
using carpoolapp_backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace carpoolapp_backend.Data;

public class DataContext : DbContext {
    
    public DataContext() {
        
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options) {
        
    }

    public DbSet<TestEnt> TestEnts { get; set; }
}