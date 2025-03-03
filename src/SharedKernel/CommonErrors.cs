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

    public static Error FileNotFound => Error.Problem(
        "Commons.FileNotFound",
        "Tệp tin không tồn tại.");

    public static Error InvalidFile => Error.Problem(
        "Commons.InvalidFile",
        "Tệp tin không hợp lệ.");

    public static Error InvalidFileSize(int maxSize) => Error.Problem(
        "Commons.InvalidFileSize",
        $"Kích thước tệp tin không được vượt quá {maxSize}MB.");

    public static Error InvalidFileExtension(string[] allowedExtensions) => Error.Problem(
        "Commons.InvalidFileExtension",
        $"Phần mở rộng của tệp tin không hợp lệ. Chỉ chấp nhận các phần mở rộng sau: {string.Join(", ", allowedExtensions)}.");
    public static Error InvalidPaginationParameters(int page, int pageSize) => Error.Problem(
        "Commons.InvalidPaginationParameters",
        $"Tham số phân trang không hợp lệ. Số trang: {page}, kích thước trang: {pageSize}.");
}
