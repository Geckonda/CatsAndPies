﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.DTO.Request
{
    public class LoginRequestDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
