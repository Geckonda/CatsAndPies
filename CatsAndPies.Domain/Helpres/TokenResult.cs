﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Helpres
{
    public class TokenResult
    {
        public string Token { get; set; }
        public DateTime? ExpiresIn { get; set; }
    }
}
