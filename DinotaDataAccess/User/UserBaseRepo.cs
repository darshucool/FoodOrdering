using System.Collections.Generic;
using System.Linq;
using Dinota.Core.Specification;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.User;

namespace Dinota.DataAccces.User
{
    public class UserBaseRepo : CrudRepository<UserBase>, IUserBaseRepo
    {
        public UserBaseRepo(DomainContext domainContext) : base(domainContext)
        {
        }

        //public void Delete(UserBase user)
        //{
        //    DbSet.Remove(user);
        //}

        
    }
}
