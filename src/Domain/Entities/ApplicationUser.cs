﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SampleProject.Domain.Entities;
public class ApplicationUser : IdentityUser
{
    public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
}
