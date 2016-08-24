﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication7.Models

{ 
public class AdminUserViewModel
{
    public ApplicationUser User { get; set; }
    public MultiSelectList Roles { get; set; }
    public string[] SelectedRoles { get; set; }
}
}