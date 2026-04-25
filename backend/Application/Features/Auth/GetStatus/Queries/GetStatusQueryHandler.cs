namespace Application.Features.Auth.GetStatus.Queries;

using Application.Features.Auth.GetStatus.Dtos;
using Application.Interfaces.Services;
using Core.Dtos.ResponsesDto;
using Core.Messages;
using MediatR;

public class GetStatusQueryHandler(
    ITokenStore tokenStore
) : IRequestHandler<GetStatusQuery, ResultDto<GetStatusResponseDto>>
{
    private readonly ITokenStore _tokenStore = tokenStore;

    public async Task<ResultDto<GetStatusResponseDto>> Handle(
        GetStatusQuery request,
        CancellationToken cancellationToken)
    {
        var status = _tokenStore.GetConnectedProviders();

        var result = ResultDto<GetStatusResponseDto>.Success(status);
        result.Message = Message.GetAllData;

        return result;
    }
}
