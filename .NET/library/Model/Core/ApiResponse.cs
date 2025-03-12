namespace OneBeyondApi.Model.Core
{
    /// <summary>
    /// A generic API response wrapper that provides consistency across all API endpoints.
    /// </summary>
    /// <typeparam name="T">The type of data returned in the response.</typeparam>
    public sealed record ApiResponse<T>(bool Success, string Message, T? Data)
    {
        /// <summary>
        /// Creates a successful response with data.
        /// </summary>
        public static ApiResponse<T> SuccessResponse(T data, string message = "Request processed successfully") =>
            new(true, message, data);

        /// <summary>
        /// Creates an error response without data.
        /// </summary>
        public static ApiResponse<T> ErrorResponse(string message) =>
            new(false, message, default);
    }
}
