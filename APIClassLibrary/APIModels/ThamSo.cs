﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class ThamSo
    {
        public Guid Id { get; set; }
        public string TenThamSo { get; set; }
        public double GiaTri { get; set; }
        public string MoTa { get; set; }
    }
}
