using ex_nhibernate.Domain;
using Microsoft.AspNetCore.Mvc;
using NHibernate;

namespace ex_nhibernate.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<User>> Get([FromServices] NHibernate.ISession _session)
    {
        // Add new User
        var newUser = new User
        {
            Name = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.Now,
        };

        ITransaction transaction = _session.BeginTransaction();
        try
        {
            await _session.SaveAsync(newUser);
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            await transaction.RollbackAsync();
        }
        finally
        {
            transaction?.Dispose();
        }

        // Get created User
        var result = await _session.GetAsync<User>(newUser.Id);

        return Ok(result);
    }
}
