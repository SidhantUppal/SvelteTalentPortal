﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentPortal.BAL.Dto
{
    public class LoginDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; }
    }
}
