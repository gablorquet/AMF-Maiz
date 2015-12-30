using AMF.Core.Model;

namespace AMF.Core.Authentication
{
    public interface IAuthenticationService
    {
        void SignOut();
        CurrentUser GetCurrentUser();
        T GetCurrent<T>() where T : User;
        bool IsAuthenticated();
        void SetCurrent<T>(T user) where T : User;
    }

}