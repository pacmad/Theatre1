using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace test2.Models.Performances
{
    public class PerformanceTime
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Time { get; set; }

        public Guid PerformanceDateId { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }

        //[JsonIgnore]
        //public virtual PerformanceDate PerformanceDate { get; set; }
    }
}
