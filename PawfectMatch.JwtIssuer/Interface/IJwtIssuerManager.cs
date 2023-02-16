using PawfectMatch.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawfectMatch.JwtIssuer.Interface
{
    public interface IJwtIssuerManager
    {
        string GenerateAuthToken(ApplicationUser applicationUser);
    }
}
