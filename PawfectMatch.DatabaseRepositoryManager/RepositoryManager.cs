using Microsoft.EntityFrameworkCore;
using PawfectMatch.DatabaseContextManager;
using PawfectMatch.DatabaseRepositoryManager.Interface;
using PawfectMatch.DataLayer;
using System.Security.Claims;
using PawfectMatch.ExceptionHandling;
using PawfectMatch.JwtIssuer.Interface;
using PawfectMatch.JwtIssuer.JwtClaims;

namespace PawfectMatch.DatabaseRepositoryManager
{
    public class RepositoryManager: IRepositoryManager
    {
        private readonly ApplicationDbContext _applicationDb = null!;
        private readonly IJwtIssuerManager _jwtIssuerManager;

        public RepositoryManager(ApplicationDbContext applicationDb, IJwtIssuerManager jwtIssuerManager)
        {
            _applicationDb = applicationDb;
            _jwtIssuerManager = jwtIssuerManager;
           
        }

        public async Task<bool> CreateUserAsync(string username, string password)
        {
            ApplicationUser applicationUser = new()
            {
                Password= password,
                UserName= username,
            };
            _applicationDb.Users.Add(applicationUser);
            return  await _applicationDb.SaveChangesAsync() > 0;
        }

        public async Task<UserProfile> GetProfile(ClaimsPrincipal user)
        {
            if (int.TryParse(user.FindFirstValue(JwtClaimsNames.UserId), out int user_id))
            {
                var up = (await _applicationDb.Users.Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == user_id))?.Profile ?? null;
                if (up == null)
                {
                  //  throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                return up!;
            }
            throw new CustomException("",null,601);
            //throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
        }

        public Task<UserProfile> GetProfileLazy(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public async Task<string> LogUserInAsync(string username, string password)
        {
            var applicationUser = await _applicationDb.Users.FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);
            if (applicationUser == null) { throw new Exception(); }
            return (_jwtIssuerManager.GenerateAuthToken(applicationUser));           
        }
    }
}