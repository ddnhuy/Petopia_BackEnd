using Application.DTOs.Post;
using Application.Posts.SearchByKeywords;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.OData.Edm;
using SharedKernel;

namespace Web.Api.Endpoints.Posts
{
    public class SearchByKeyword : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
        {
            app.MapGet("v{version:apiVersion}/odata/posts/search", async (string keyword, HttpContext httpContext, [FromServices] ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new SearchPostsByKeywordQuery(keyword);

                Result<List<PostDto>> result = await sender.Send(query, cancellationToken);

                IQueryable<PostDto> queryableResult = result.Value.AsQueryable();

                IEdmModel edmModel = httpContext.RequestServices.GetRequiredService<IEdmModel>();
                var queryContext = new ODataQueryContext(edmModel, typeof(PostDto), null);

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return Results.Ok(Enumerable.Empty<PostDto>().AsQueryable());
                }

                string odataFilter = $"$filter=contains(tolower(Caption),tolower('{keyword}')) or contains(tolower(User/Username),tolower('{keyword}'))";

                HttpRequest request = httpContext.Request;
                var uriBuilder = new UriBuilder(request.Scheme, request.Host.Host, request.Host.Port ?? 80, request.Path)
                {
                    Query = odataFilter
                };

                request.QueryString = new QueryString(uriBuilder.Query);
                

                var queryOptions = new ODataQueryOptions<PostDto>(queryContext, httpContext.Request);

                
                var filteredResult = (IQueryable<PostDto>)queryOptions.ApplyTo(queryableResult);

                return Results.Ok(filteredResult);
            })
            .WithTags(Tags.Post)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(new ApiVersion(2, 0));
        }
    }
}
