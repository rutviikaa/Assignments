namespace Hospital
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DoctorSchedule
    {
        public int DoctorScheduleId { get; set; }

        public int DoctorId { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
