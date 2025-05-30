﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [NotMapped]
    public class EntityBase
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow; 
        public string? History { get; set; }
        public void UpdateTimeStamp()
        {
            LastModifiedDate = DateTime.UtcNow; 
        }
    }
}
