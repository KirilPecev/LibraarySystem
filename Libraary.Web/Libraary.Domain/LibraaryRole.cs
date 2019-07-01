namespace Libraary.Domain
{
    using Microsoft.AspNetCore.Identity;
    using System;

    public class LibraaryRole : IdentityRole
    {
        public LibraaryRole()
           : this(null)
        {
        }

        public LibraaryRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
