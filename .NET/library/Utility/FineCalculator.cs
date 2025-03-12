namespace OneBeyondApi.Utility;

public sealed class FineCalculator
{
    private const decimal TAX_PER_DAY = 1.0m;

    /// <summary>
    /// Calculates the fine for a late book return.
    /// </summary>
    /// <param name="loanEndDate">The date when the book was supposed to be returned.</param>
    /// <param name="returnDate">The actual date when the book was returned.</param>
    /// <returns>The fine amount in decimal format.</returns>
    public static decimal CalculateFine(DateTime loanEndDate, DateTime returnDate) {
        var overdueDays = (returnDate - loanEndDate).Days;
        return overdueDays > 0 ? overdueDays * TAX_PER_DAY : 0;
    }
}
