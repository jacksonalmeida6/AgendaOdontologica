﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaOdontologica.Models
{
    public class NivelAcesso : IdentityRole
    {
        public string Descricao { get; set; }

    }
}
