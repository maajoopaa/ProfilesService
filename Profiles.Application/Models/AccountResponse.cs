﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Application.Models
{
    public class AccountResponse
    {
        public string? Token { get; set; }

        public Guid Id { get; set; }
    }
}
