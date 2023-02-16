using PawfectMatch.DataLayer;
using System.Security.Claims;

namespace PawfectMatch.DatabaseRepositoryManager.Interface
{
    public interface IRepositoryManager
    {
        public Task<string> LogUserInAsync(string username, string password);
        public Task<bool> CreateUserAsync(string username, string password);
        public Task<UserProfile> GetProfile(ClaimsPrincipal user);
        public Task<UserProfile> GetProfileLazy(ClaimsPrincipal user);
    }
}
