namespace SharedKernel;
public static class CommonErrors
{
    public static Error GreaterThanTotalPages(int pageNumber, int totalPages) => Error.Problem(
        "Commons.GreaterThanTotalPages",
        $"Page number {pageNumber} is greater than the total number of pages {totalPages}.");

    public static Error PageLessThanOne => Error.Problem(
        "Commons.PageLessThanOne",
        $"Page number must be greater than or equal to 1.");

    public static Error InvalidPageSize => Error.Problem(
        "Commons.InvalidPageSize",
        $"Page size is invalid. It must be greater than 0.");

    public static Error InvalidFile(string message) => Error.Problem(
        "Commons.InvalidFile",
        message);
}
