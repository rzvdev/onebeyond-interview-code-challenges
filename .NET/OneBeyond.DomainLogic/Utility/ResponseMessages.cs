namespace OneBeyond.DomainLogic.Utility;

public static class ResponseMessages
{
    // Borrower 
    public const string BorrowerActiveLoansRetrieveSuccessfully = "Borrowers with active loans retrieved successfully";
    public const string BorrowerNotFound = "No borrowers found";
    public const string BorrowerActiveLoansNotFound = "No borrowers found with active loans";

    // Book
    public const string BookNotFound = "Book not found";
    public const string BookNotOnLoan = "Book is not currently on loan";
    public const string BookReturnedLate = "Book returned late. Fine imposed: ${0}";
    public const string BookReturnedSuccessfully = "Book successfully returned";
    public const string BookAlreadyReservedByYou = "You have already reserved that book";
    public const string BookReservedSuccessfully = "Book reserved successfully. Your estimated availability date: ${0}";
    public const string BookNotReserved = "No active reservation found for this book";
    public const string BookReservationStatusSuccessfully = "Reservation status retrieved successfully.";
}
