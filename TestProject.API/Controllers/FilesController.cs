using Microsoft.AspNetCore.Mvc;

namespace TestProject.Controllers;

public class FilesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostFile()
    {
        await Task.Delay(1000);
        return Ok();
    }
}
