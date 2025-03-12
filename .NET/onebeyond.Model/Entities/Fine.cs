using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneBeyond.Model.Entities;

[Table("DBD_FINE")] // DBD aka DOMAIN BUSSINESS DATA
public sealed class Fine
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("DBD_FINE_ID")]
    public int Id { get; set; }

    [Column("DBD_FINE_GUID")]
    public Guid Guid { get; set; } = Guid.NewGuid();

    [Column("DBD_FINE_BORROWER_ID")]
    public Borrower? Borrower { get; set; }

    [Column("DBD_FINE_BOOKSTOCK_ID")]
    public BookStock? BookStock { get; set; }

    [Column("DBD_FINE_AMOUNT")]
    public decimal Amount { get; set; }

    [Column("DBD_FINE_DATE_ISSUED")]
    public DateTime IssuedDate { get; set; } = DateTime.UtcNow;

    [Column("DBD_FINE_IS_PAID")]
    public bool IsPaid { get; set; }

    [Column("DBD_FINE_IS_DELETED")]
    public bool IsDeleted { get; set; }
}
