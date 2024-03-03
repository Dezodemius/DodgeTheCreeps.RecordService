using Microsoft.EntityFrameworkCore;

namespace DodgeTheCreeps.RecordService;

[PrimaryKey(nameof(Username))]
public class UserRecord
{
    public string Username { get; set; }
    public int RecordValue { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is UserRecord record)
            return record.Username == Username;
        return false;
    }

    public UserRecord(string username, int recordValue)
    {
        Username = username;
        RecordValue = recordValue;
    }
}