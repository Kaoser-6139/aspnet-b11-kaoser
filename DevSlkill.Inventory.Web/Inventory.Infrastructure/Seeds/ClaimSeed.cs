using Inventory.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Seeds
{
    public static class ClaimSeed
    {
        public static ApplicationUserClaim[] GetClaims()
        {
            return [
                new ApplicationUserClaim
                {
                    Id =-1,
                    UserId=new Guid("7b513c39-6291-4e09-982e-08ddcde9763a"),
                    ClaimType="create_user",
                    ClaimValue="allowed"

                },
                new ApplicationUserClaim
                {
                    Id = 1,
                    UserId = new Guid("3f636de0-27d4-4df9-982f-08ddcde9763a"),
                    ClaimType="create_userHr",
                    ClaimValue="allowedHr"
                }

                ];
        }
    }
}
