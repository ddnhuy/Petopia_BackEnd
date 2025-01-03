using Application.Abstractions.Messaging;

namespace Application.Todos.Get;

public sealed record GetTodosQuery(string UserId): IQuery<List<TodoResponse>>;
