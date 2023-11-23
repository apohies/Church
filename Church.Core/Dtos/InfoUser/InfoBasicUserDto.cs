﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Church.Core.Dtos.InfoUser
{
    public sealed class InfoBasicUserDto
    {
        public string username { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;
    }
}
