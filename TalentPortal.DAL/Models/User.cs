﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentPortal.DAL.Models
{
    public class User:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
      
    }
}
