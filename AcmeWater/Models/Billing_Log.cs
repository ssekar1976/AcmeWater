namespace AcmeWater.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Billing_Log
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Billing_Log()
        {
            Billing_Txns = new HashSet<Billing_Txns>();
        }

        [Key]
        public int LogId { get; set; }

        public DateTime LogDate { get; set; }

        public int BillingGenerated { get; set; }

        public int BillingEmailed { get; set; }

        public decimal BillingAmount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Billing_Txns> Billing_Txns { get; set; }
    }
}
