using Application.Abstractions.Messaging;
using Application.DTOs.Post;

namespace Application.Posts.SearchByKeywords;
public sealed record SearchPostsByKeywordQuery(string Keyword) : IQuery<List<PostDto>>;
