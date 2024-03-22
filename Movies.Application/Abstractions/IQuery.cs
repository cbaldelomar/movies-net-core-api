using MediatR;

namespace Movies.Application.Abstractions;

internal interface IQuery<out TResponse> : IRequest<TResponse>
{
}
