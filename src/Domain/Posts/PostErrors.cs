using SharedKernel;

namespace Domain.Posts;
public static class PostErrors
{
    public static Error PostNotFound => Error.NotFound(
        "Posts.PostNotFound",
        "Không tìm thấy thông tin.");

    public static Error PostNotHavePermission => Error.Problem(
        "Posts.PostNotHavePermission",
        "Bạn không có quyền thực hiện hành động này.");
}
