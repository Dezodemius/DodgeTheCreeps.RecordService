using Microsoft.AspNetCore.Mvc;

namespace DodgeTheCreeps.RecordService.Controllers;

[ApiController]
[Route("[controller]")]
public class RecordsController : ControllerBase
{
    private List<UserRecord> _records = new();

    private readonly ILogger<RecordsController> _logger;

    public RecordsController(ILogger<RecordsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetUserRecords")]
    public IEnumerable<UserRecord> GetUserRecords()
    {
        return _records;
    }
    
    [HttpGet(Name = "SetOrUpdateUserRecord")]
    public void SetOrUpdateUserRecord(string userName, int record)
    {
        
    }
}