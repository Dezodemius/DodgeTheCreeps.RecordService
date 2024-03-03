using Microsoft.AspNetCore.Mvc;

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

    [HttpGet(Name = "GetUserRecords")]
    public List<UserRecord> GetUserRecords()
    {
        using var dbContext = new UserRecordsDbContext();
        return dbContext.UserRecords.ToList();
    }
    
    [HttpPost(Name = "SetOrUpdateUserRecord")]
    public void SetOrUpdateUserRecord(string userName, int record)
    {
        using var dbContext = new UserRecordsDbContext();
        dbContext.AddOrUpdateUserRecord(new UserRecord(userName, record));
    }
}