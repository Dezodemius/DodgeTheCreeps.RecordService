using DodgeTheCreeps.Core;
using Microsoft.EntityFrameworkCore;

namespace DodgeTheCreeps.RecordService;

public sealed class UserRecordsDbContext : DbContext
{
  private static UserRecordsDbContext _instance = null!;
  private static readonly object Padlock = new ();
  public static UserRecordsDbContext Instance
  {
    get
    {
      if (_instance == null)
      {
        lock (Padlock)
        {
          _instance ??= new UserRecordsDbContext();
        }
      }
      return _instance;
    }
  }
  public DbSet<UserScore> UserRecords => Set<UserScore>();

  protected override void OnConfiguring(DbContextOptionsBuilder builder)
  {
    builder.UseSqlite("Filename=records.db");
  }

  private UserRecordsDbContext()
  {
    Database.EnsureCreated();
  }
}