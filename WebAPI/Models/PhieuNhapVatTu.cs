﻿using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class PhieuNhapVatTu
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime NgayNhap { get; set; }
        public double TongTien { get; set; }
    }
}
