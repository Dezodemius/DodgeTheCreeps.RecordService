using System;
using Microsoft.EntityFrameworkCore;

namespace DodgeTheCreeps.RecordService;

[PrimaryKey(nameof(Name))]
public class UserScore
{
    public string Name { get; set; }
    public int Score { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is UserScore record)
            return record.Name == Name;
        return false;
    }

    protected bool Equals(UserScore other)
    {
      return Name == other.Name;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Name);
    }

    public UserScore(string name, int score)
    {
        Name = name;
        Score = score;
    }
}