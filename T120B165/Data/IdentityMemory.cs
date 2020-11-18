using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace T120B165.Data
{
    public class IdentityMemory : IdentityDbContext
    {
        public IdentityMemory(DbContextOptions<IdentityMemory> options) : base(options) { }
    }
}
