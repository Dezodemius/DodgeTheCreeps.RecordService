using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DodgeTheCreeps.RecordService.Controllers;

[ApiController]
[Route("[controller]")]
public class RecordsController : ControllerBase
{
  private readonly ILogger<RecordsController> _logger;

  public RecordsController(ILogger<RecordsController> logger)
  {
    _logger = logger;
  }

  [HttpGet(nameof(GetTopRecords))]
  public IActionResult GetTopRecords()
  {
    using var dbContext = new UserRecordsDbContext();
    return Ok(dbContext.UserRecords.ToList());
  }
  
  [HttpGet(nameof(GetIsUserExists))]
  public IActionResult GetIsUserExists(string name)
  {
    using var dbContext = new UserRecordsDbContext();
    var user = dbContext.UserRecords.FirstOrDefault(u => u.Username == name);
    return Ok(user != null);
  }
  
  [HttpPost(nameof(UpdateUserRecord))]
  public void UpdateUserRecord([FromBody] UserRecord userRecord)
  {
    using var dbContext = new UserRecordsDbContext();

    var foundStudent = dbContext.UserRecords.SingleOrDefault(r => r.Username == userRecord.Username);
    if (foundStudent != null && foundStudent.RecordValue < userRecord.RecordValue)
      foundStudent.RecordValue = userRecord.RecordValue;

    dbContext.SaveChanges();
  }

  [HttpPost(nameof(AddUserRecord))]
  public void AddUserRecord([FromBody] UserRecord userRecord)
  {
    using var dbContext = new UserRecordsDbContext();
    dbContext.UserRecords.Add(new UserRecord(userRecord.Username, userRecord.RecordValue));
    dbContext.SaveChanges();
  }
  
  [HttpGet(nameof(GetUserRecord))]
  public IActionResult GetUserRecord(string name)
  {
    using var dbContext = new UserRecordsDbContext();
    return Ok(dbContext.UserRecords.First(u => u.Username == name));
  }
}