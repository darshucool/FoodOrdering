using System;
using System.ComponentModel.DataAnnotations;
using Dinota.Core.Data;

namespace Dinota.Domain.User
{
    public class SystemUserInfo : Entity
    {
        public string SamAccountName { get; set; }

        public string EmailAddress { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }
    }
}
