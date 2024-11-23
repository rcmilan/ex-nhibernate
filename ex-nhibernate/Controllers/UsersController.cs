using ex_nhibernate.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ex_nhibernate.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<User>> Get()
    {
        return Ok(new User());
    }
}
