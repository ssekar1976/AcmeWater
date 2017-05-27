namespace AcmeWater.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [StringLength(10)]
        public string CustomerUUID { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerEmail { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerAddress { get; set; }

        [Required]
        [MaxLength(50)]
        public string CustomerCity { get; set; }

        [Required]
        [StringLength(2)]
        public string CustomerState { get; set; }

        [Required]
        [StringLength(5)]
        public string CustomerZip { get; set; }

        public bool IsCustomerActive { get; set; }

        public DateTime CustomerCreatedOn { get; set; }

        public DateTime? CustomerUpdatedOn { get; set; }

        public decimal PastDue { get; set; }
    }
}
