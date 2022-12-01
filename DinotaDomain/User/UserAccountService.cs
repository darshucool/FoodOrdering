using System.Collections.Generic;
using Dinota.Core.Data;
using Dinota.Core.Specification;

namespace Dinota.Domain.User
{
    public class UserAccountService : EntityCrudService<UserAccount, IUserAccountRepo>
    {
        public UserAccountService(IUserAccountRepo repository) : base(repository)
        {
        }

        public virtual ISpecification<UserAccount> GetAllSpecification()
        {
            return new Specification<UserAccount>(m => true);
        }

    }
}
