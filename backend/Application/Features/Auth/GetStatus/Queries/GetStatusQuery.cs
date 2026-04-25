namespace Application.Features.Auth.GetStatus.Queries;

using Application.Features.Auth.GetStatus.Dtos;
using Core.Dtos.ResponsesDto;
using MediatR;

public record GetStatusQuery : IRequest<ResultDto<GetStatusResponseDto>>;
