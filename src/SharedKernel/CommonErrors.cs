namespace SharedKernel;
public static class CommonErrors
{
    public static Error GreaterThanTotalPages(int pageNumber, int totalPages) => Error.Problem(
        "Commons.GreaterThanTotalPages",
        $"Số trang {pageNumber} lớn hơn tổng số trang là {totalPages}.");

    public static Error PageLessThanOne => Error.Problem(
        "Commons.PageLessThanOne",
        $"Số trang phải lớn hơn hoặc bằng 1.");

    public static Error InvalidPageSize => Error.Problem(
        "Commons.InvalidPageSize",
        $"Kích thước trang không hợp lệ. Nó phải lớn hơn 0.");

    public static Error InvalidFile => Error.Problem(
        "Commons.InvalidFile",
        "Tệp tin không hợp lệ.");
}
