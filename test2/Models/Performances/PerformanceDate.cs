using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test2.Models.Performances
{
    public class PerformanceDate
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Guid PerformanceId { get; set; }

        //[JsonIgnore]
        //public virtual Performance Performance { get; set; }

        public virtual ICollection<PerformanceTime> PerformanceTimes { get; set; }
    }
}
