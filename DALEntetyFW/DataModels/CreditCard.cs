namespace DALEntetyFW.DataModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CreditCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(16)]
        public string CreditCardID { get; set; }

        [StringLength(30)]
        public string CardHolderName { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }
    }
}

