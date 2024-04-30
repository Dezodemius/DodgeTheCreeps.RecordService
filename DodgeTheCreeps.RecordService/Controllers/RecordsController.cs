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
      .OrderByDescending(u => u.Score)
      .Take(10)
      .ToList());
  }
  
  [HttpGet(nameof(GetIsUserExists))]
  public IActionResult GetIsUserExists(string name)
  {
    var user = UserRecordsDbContext.Instance.UserRecords.FirstOrDefault(u => u.Name == name);
    return Ok(user != null);
  }
  
  [HttpPost(nameof(UpdateUserRecord))]
  public void UpdateUserRecord([FromBody] UserScore userScore)
  {
    var foundStudent = UserRecordsDbContext.Instance.UserRecords.SingleOrDefault(r => r.Name == userScore.Name);
    if (foundStudent != null && foundStudent.Score < userScore.Score)
      foundStudent.Score = userScore.Score;

    UserRecordsDbContext.Instance.SaveChanges();
  }

  [HttpPost(nameof(AddUserRecord))]
  public void AddUserRecord([FromBody] UserScore userScore)
  {
    UserRecordsDbContext.Instance.UserRecords.Add(new UserScore(userScore.Name, userScore.Score));
    UserRecordsDbContext.Instance.SaveChanges();
  }
  
  [HttpGet(nameof(GetUserRecord))]
  public IActionResult GetUserRecord(string name)
  {
    return Ok(UserRecordsDbContext.Instance.UserRecords.First(u => u.Name == name));
  }
}