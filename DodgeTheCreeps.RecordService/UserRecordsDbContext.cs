using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DodgeTheCreeps.RecordService;

public class UserRecordsDbContext : DbContext
{
    public DbSet<UserRecord> UserRecords => Set<UserRecord>();

    public void AddOrUpdateUserRecord(UserRecord userRecord)
    {
        if (UserRecords.Contains(userRecord))
        {
            var foundStudent = UserRecords.SingleOrDefault(r => r.Username == userRecord.Username);
            if (foundStudent != null && foundStudent.RecordValue < userRecord.RecordValue)
                foundStudent.RecordValue = userRecord.RecordValue;
        }
        else
        {
            UserRecords.Add(userRecord);
        }
            
        SaveChanges();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlite("Filename=records.db");
    }

    public UserRecordsDbContext()
    {
        Database.EnsureCreated();
    }

}