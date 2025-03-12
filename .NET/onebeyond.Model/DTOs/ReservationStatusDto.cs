namespace OneBeyond.Model.DTOs;

public sealed record ReservationStatusDto(string BorrowerName, string BookName, DateTime ReservationDate, DateTime ExpectedAvailabilityDate);
