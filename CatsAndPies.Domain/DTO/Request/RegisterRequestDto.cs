﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.DTO.Request
{
    public class RegisterRequestDto
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
