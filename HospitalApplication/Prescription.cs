namespace Hospital
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Prescription
    {
        public int PrescriptionId { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public int DrugId { get; set; }

        public int DoseId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public virtual Dos Dos { get; set; }

        public virtual Drug Drug { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
