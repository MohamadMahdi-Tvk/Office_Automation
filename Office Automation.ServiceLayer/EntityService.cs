using Office_Automation.ModelLayer;
using Office_Automation.ModelLayer.Context;
using Office_Automation.RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office_Automation.ServiceLayer
{
    public class EntityService<T> : GenericRepository<T> where T : BaseEntity
    {
        public EntityService(OfficeAutomationContext context) : base(context)
        {
        }
    }
}
