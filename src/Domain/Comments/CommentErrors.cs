using SharedKernel;

namespace Domain.Comments;
public static class CommentErrors
{
    public static Error CommentNotFound => Error.NotFound(
        "Comments.CommentNotFound",
        "Không tìm thấy thông tin.");

    public static Error CommentNotHavePermission => Error.Problem(
        "Comments.CommentNotHavePermission",
        "Bạn không có quyền thực hiện hành động này.");
}
