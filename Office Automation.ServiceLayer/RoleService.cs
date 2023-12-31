﻿using Office_Automation.ModelLayer;
using Office_Automation.ModelLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office_Automation.ServiceLayer
{
    public class RoleService : EntityService<Role>, IRoleService
    {
        public RoleService(OfficeAutomationContext context) : base(context)
        {
        }
    }
}
