using MediatR;

namespace Movies.Application.Abstractions;

internal interface ICommand<out TResponse> : IRequest<TResponse>
{
}
