namespace Application.Features.OAuth.GetAuthUrl.Queries;

using Application.Features.OAuth.GetAuthUrl.Dtos;
using Core.Dtos.ResponsesDto;
using MediatR;

public record GetAuthUrlQuery : IRequest<ResultDto<GetAuthUrlResponseDto>>
{
    public required string Provider { get; init; }
}
