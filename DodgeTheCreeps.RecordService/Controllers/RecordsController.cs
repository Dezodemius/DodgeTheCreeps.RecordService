using System;
using System.Linq;
using DodgeTheCreeps.Core;
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
  
  [HttpPost(nameof(AddOrUpdateUserRecord))]
  public void AddOrUpdateUserRecord([FromBody] UserScore userScore)
  {
    var foundedUser = UserRecordsDbContext.Instance.UserRecords.SingleOrDefault(u => u.Name == userScore.Name);

    if (foundedUser == null)
      UserRecordsDbContext.Instance.UserRecords.Add(new UserScore(userScore.Name, userScore.Score));
    else
      foundedUser.Score = userScore.Score;

    UserRecordsDbContext.Instance.SaveChanges();
  }
  
  [HttpGet(nameof(GetUserRecord))]
  public IActionResult GetUserRecord(string name)
  {
    return Ok(UserRecordsDbContext.Instance.UserRecords.First(u => u.Name == name));
  }

  [HttpDelete(nameof(DeleteUserRecord))]
  public void DeleteUserRecord(string name)
  {
    var foundUser = UserRecordsDbContext.Instance.UserRecords.FirstOrDefault(u => u.Name == name);
    if (foundUser != null)
    {
      UserRecordsDbContext.Instance.UserRecords.Remove(foundUser);
      UserRecordsDbContext.Instance.SaveChanges();
    }
  }
}