﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Shared
{
    public class ServiceResponse<T>
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = false;
        public T Data { get; set; }
    }
}
