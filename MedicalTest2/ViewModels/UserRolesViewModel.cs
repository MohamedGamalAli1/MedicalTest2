﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<RoleViewModelCmBox> RolesCmBoxes { get; set; }
    }
}
