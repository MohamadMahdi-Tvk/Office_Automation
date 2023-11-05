using Office_Automation.ModelLayer;
using Office_Automation.ModelLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office_Automation.RepositoryLayer
{
    public class RolesRepository : GenericRepository<Role>, IRolesRepository
    {
        public RolesRepository(OfficeAutomationContext context) : base(context)
        {
        }
    }
}
