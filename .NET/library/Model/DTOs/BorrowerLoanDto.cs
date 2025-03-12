namespace OneBeyondApi.Model.DTOs;

public sealed record BorrowerLoanDto(string BorrowerName, string BorrowerEmail, string BookTitle, DateTime? LoadEndDate);