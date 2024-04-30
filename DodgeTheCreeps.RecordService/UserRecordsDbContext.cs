using Microsoft.EntityFrameworkCore;

namespace DodgeTheCreeps.RecordService;

public class UserRecordsDbContext : DbContext
{
    public DbSet<UserRecord> UserRecords => Set<UserRecord>();

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlite("Filename=records.db");
    }

    public UserRecordsDbContext()
    {
        Database.EnsureCreated();
    }

}