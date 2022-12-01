using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using Dinota.Core.Data;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.User;
using System.Linq;
using System.Linq.Expressions;

namespace Dinota.DataAccces.User
{
    public class SystemUserInfoRepo :  Repository<SystemUserInfo>, ISystemUserInfoRepo
    {
        public SystemUserInfoRepo(DomainContext domainContext) : base(domainContext)
        {
            var list = GetValue();
            
        }

        public override int Count(Core.Specification.ISpecification<SystemUserInfo> specification)
        {
            return GetValue().Count();
        }

        public override IEnumerable<SystemUserInfo> GetCollection<TKey>(Expression<System.Func<SystemUserInfo, TKey>> sortExpression, System.ComponentModel.ListSortDirection sortDirection, int pageSize, int pageIndex)
        {
            return GetValue().Skip(pageSize * pageIndex)
                .Take(pageSize);
        }

        private IEnumerable<SystemUserInfo> GetValue()
        {
            var list = new List<SystemUserInfo>();
            var ctx = new PrincipalContext(ContextType.Domain, ConfigurationManager.AppSettings["DomainName"]);
            var u = new UserPrincipal(ctx);
            
            var ps = new PrincipalSearcher(u);
            var results = ps.FindAll();
        
            foreach (UserPrincipal usr in results)
            {
                
                var info = new SystemUserInfo { SamAccountName = usr.SamAccountName, EmailAddress = usr.EmailAddress, Name = usr.Name, MiddleName = usr.MiddleName };
                list.Add(info);
            }

            return list;
        }
    }
}
