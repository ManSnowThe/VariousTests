using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace VariousTests.DAL.Entities
{
    public class AppUser : IdentityUser
    {
        public virtual UserProfile UserProfile { get; set; }
    }
}
