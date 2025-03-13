using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneBeyond.Model.Entities;

[Table("DBD_RESERVATION")] // DBD aka DOMAIN BUSSINESS DATA
public sealed class Reservation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("DBD_RESERVATION_ID")]
    public int Id { get; set; }

    [Column("DBD_RESERVATION_GUID")]
    public Guid Guid { get; set; } = Guid.NewGuid();

    [Column("DBD_RESERVATION_BORROWER_ID")]
    public Borrower? Borrower { get; set; }

    [Column("DBD_RESERVATION_BOOK_ID")]
    public Book? Book { get; set; }

    [Column("DBD_RESERVATION_DATE")]
    public DateTime ReservationDate { get; set; } = DateTime.UtcNow;

    [Column("DBD_RESERVATION_ACTIVE")]
    public bool IsActive { get; set; }

    [Column("DBD_RESERVATION_EXPECTED_AVAILABLE_DATE")]
    public DateTime ExpectedAvailabilityDate { get; set; }

    [Column("DBD_RESERVATION_IS_DELETED")]
    public bool IsDeleted { get; set; }
}
