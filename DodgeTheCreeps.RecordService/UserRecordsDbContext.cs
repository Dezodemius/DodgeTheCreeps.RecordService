using Microsoft.EntityFrameworkCore;

namespace DodgeTheCreeps.RecordService;

public class UserRecordsDbContext : DbContext
{
  private static UserRecordsDbContext _instance;
  private static readonly object padlock = new ();
  public static UserRecordsDbContext Instance
  {
    get
    {
      if (_instance == null)
      {
        lock (padlock)
        {
          _instance ??= new UserRecordsDbContext();
        }
      }
      return _instance;
    }
  }
  public DbSet<UserRecord> UserRecords => Set<UserRecord>();

  protected override void OnConfiguring(DbContextOptionsBuilder builder)
  {
    builder.UseSqlite("Filename=records.db");
  }

  private UserRecordsDbContext()
  {
    Database.EnsureCreated();
  }
}