using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawfectMatch.DatabaseRepositoryManager.Interface;
using PawfectMatch.DataLayer;

namespace PawfectMatchAPI.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IRepositoryManager repositoryManager;     

        public TestController(IRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
        }

        [HttpGet]
        public async Task<ActionResult<UserProfile>> Get()
        {   
            return await repositoryManager.GetProfile(User);
        }

        [HttpPost("LoginUser")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> LoginUserAsync(string userName, string password)
        {
            return Ok(await repositoryManager.LogUserInAsync(userName, password));
        }

        [HttpPost("CreateUser")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> CreateateAccountAsync(string userName, string password)
        {
            return Ok(await repositoryManager.CreateUserAsync(userName, password));
        }
    }
}
