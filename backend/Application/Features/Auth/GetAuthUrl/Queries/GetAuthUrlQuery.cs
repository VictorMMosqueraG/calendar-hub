namespace Application.Features.Auth.GetAuthUrl.Queries;

using Application.Features.Auth.GetAuthUrl.Dtos;
using Core.Dtos.ResponsesDto;
using MediatR;

public record GetAuthUrlQuery : IRequest<ResultDto<GetAuthUrlResponseDto>>
{
    public string? Provider { get; init; }
}
