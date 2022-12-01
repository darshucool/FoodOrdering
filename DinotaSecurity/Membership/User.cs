using System;
using System.Web.Security;
using Dinota.Domain.User;


namespace Dinota.Security.Membership
{
    [System.Diagnostics.DebuggerDisplay("UserName = {UserName}")]
    public class User : MembershipUser
    {
        public User(string providername,
                        string email,
                        string passwordQuestion,
                        DateTime lastLoginDate,
                        DateTime lastActivityDate,
                        UserBase userAccount) :
            base(providername,
                            userAccount.UserName,
                            userAccount.Id,
                            email,
                            passwordQuestion,
                            string.Empty,
                            true,
                            false,
                            userAccount.CreationDate,
                            lastLoginDate,
                            lastActivityDate,
                            new DateTime(),
                            new DateTime())
        {
            UserAccount = userAccount;
        }

        public UserBase UserAccount { get; private set; }
    }
}
