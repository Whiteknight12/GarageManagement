using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
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
