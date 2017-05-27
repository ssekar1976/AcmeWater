namespace AcmeWater.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer_Import_Log
    {
        [Key]
        public int ImportLogId { get; set; }

        [StringLength(50)]
        public string ImportFileName { get; set; }

        public int TotalCustomers { get; set; }

        public int CustomersNew { get; set; }

        public int CustomersUpdated { get; set; }

        public DateTime ImportedOn { get; set; }
    }
}
