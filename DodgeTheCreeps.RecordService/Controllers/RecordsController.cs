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
    return Ok(UserRecordsDbContext.Instance.UserRecords
      .OrderByDescending(u => u.RecordValue)
      .Take(10)
      .ToList());
  }
  
  [HttpGet(nameof(GetIsUserExists))]
  public IActionResult GetIsUserExists(string name)
  {
    var user = UserRecordsDbContext.Instance.UserRecords.FirstOrDefault(u => u.Username == name);
    return Ok(user != null);
  }
  
  [HttpPost(nameof(UpdateUserRecord))]
  public void UpdateUserRecord([FromBody] UserRecord userRecord)
  {
    var foundStudent = UserRecordsDbContext.Instance.UserRecords.SingleOrDefault(r => r.Username == userRecord.Username);
    if (foundStudent != null && foundStudent.RecordValue < userRecord.RecordValue)
      foundStudent.RecordValue = userRecord.RecordValue;

    UserRecordsDbContext.Instance.SaveChanges();
  }

  [HttpPost(nameof(AddUserRecord))]
  public void AddUserRecord([FromBody] UserRecord userRecord)
  {
    UserRecordsDbContext.Instance.UserRecords.Add(new UserRecord(userRecord.Username, userRecord.RecordValue));
    UserRecordsDbContext.Instance.SaveChanges();
  }
  
  [HttpGet(nameof(GetUserRecord))]
  public IActionResult GetUserRecord(string name)
  {
    return Ok(UserRecordsDbContext.Instance.UserRecords.First(u => u.Username == name));
  }
}