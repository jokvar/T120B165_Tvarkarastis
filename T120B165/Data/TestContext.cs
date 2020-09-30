using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using T120B165.Models;


namespace T120B165.Data
{
    public class TestContext : DbContext
    {
        public TestContext (DbContextOptions<TestContext> options) : base(options)
        {

        }

        public DbSet<Test> Tests { get; set; }
    }
}
