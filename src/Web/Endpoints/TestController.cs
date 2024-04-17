using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SampleProject.Web.Endpoints;
[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet("GetNumberFromROute")]
    public ActionResult<int> GetNumberUpdated2()
    {
        return Ok(5);
    }
}
