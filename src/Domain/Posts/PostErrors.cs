using SharedKernel;

namespace Domain.Posts;
public static class PostErrors
{
    public static Error PostNotFound => Error.NotFound(
        "Posts.PostNotFound",
        "Không tìm thấy thông tin.");
}
