﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Uname { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Bio { get; set; }
    }
}
