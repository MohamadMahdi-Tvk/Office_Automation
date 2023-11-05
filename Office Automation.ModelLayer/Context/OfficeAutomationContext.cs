using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Office_Automation.ModelLayer.Context
{
    public class OfficeAutomationContext : DbContext
    {
        public DbSet<Role> roles { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Letter> letters { get; set; }
        public DbSet<Department> departments { get; set; }
    }
}
