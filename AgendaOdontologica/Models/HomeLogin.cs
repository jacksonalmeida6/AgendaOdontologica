﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaOdontologica.Models
{
    public class HomeLogin :  IdentityUser
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}