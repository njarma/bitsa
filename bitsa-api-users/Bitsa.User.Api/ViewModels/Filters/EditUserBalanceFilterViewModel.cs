﻿using Bitsa.User.Api.ViewModels.ClassesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitsa.User.Api.ViewModels.Filters
{
    public class EditUserBalanceFilterViewModel: IdentificableViewModel
    {
        public float Balance { get; set; }
    }
}
