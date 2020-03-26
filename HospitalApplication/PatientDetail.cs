namespace Hospital
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PatientDetail
    {
        public int PatientDetailId { get; set; }

        public int PatientId { get; set; }

        public int AssistantId { get; set; }

        public virtual Assistant Assistant { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
