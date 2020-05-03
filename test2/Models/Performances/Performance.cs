using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace test2.Models.Performances
{
    public class Performance
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }

        public virtual ICollection<PerformanceDate> PerformanceDates { get; set; }
    }
}
