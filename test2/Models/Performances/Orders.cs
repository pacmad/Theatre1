using System;
using System.ComponentModel.DataAnnotations;

namespace test2.Models.Performances
{
    public class Orders
    {
        [Key]
        public Guid Id { get; set; }
        public Int32 Count { get; set; }
        public Guid PerformanceTimeId { get; set; }
        public Guid UserId { get; set; }

    }
}
