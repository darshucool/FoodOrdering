using System.Web.Security;
using Dinota.Security.Membership;

namespace MIMS.Security
{
    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        User GetUser(bool userIsOnline);
        User GetUser(string userName, bool userIsOnline);
    }
}