﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace WebAPI.Models
{
    public class NhomNguoiDung
    {
        [Key]
        public Guid Id { get; set; } 
        public string TenNhom { get; set; }
    }
}
