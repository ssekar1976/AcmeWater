namespace AcmeWater.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Billing_Txns
    {
        [Key]
        public int BillingId { get; set; }

        public DateTime BillingDate { get; set; }

        public int? CustomerId { get; set; }

        public decimal PastDue { get; set; }

        public decimal CurrentDue { get; set; }

        public bool IsEmailed { get; set; }

        [StringLength(100)]
        public string EmailedTo { get; set; }

        [StringLength(1000)]
        public string EmailText { get; set; }

        public DateTime? EmailedOn { get; set; }

        public int BillingLogId { get; set; }

        public virtual Billing_Log Billing_Log { get; set; }
    }
}
