using Microsoft.AspNetCore.Mvc;

namespace SampleProject.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    public ActionResult<string> GetStringName()
    {
        return Ok(String.Empty);
    }
}
