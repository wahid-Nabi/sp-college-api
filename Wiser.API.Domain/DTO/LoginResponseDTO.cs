﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Wiser.API.Entities.DTO
{
    public class LoginResponseDTO
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string token { get; set; }
        public string Role { get; set; }
    }
}
