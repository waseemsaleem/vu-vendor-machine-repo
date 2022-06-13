using VendorMachine.Core.ViewModels;

namespace VendorMachine.Core.Services.Interfaces
{
    public interface IJWTService
    {
        UserTokens GetUserTokens(UserLogins user);
        string ValidateJwtToken(string token);
    }
}
