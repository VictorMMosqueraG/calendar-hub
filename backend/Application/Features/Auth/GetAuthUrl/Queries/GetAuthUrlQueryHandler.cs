namespace Application.Features.Auth.GetAuthUrl.Queries;

using Application.Features.Auth.GetAuthUrl.Dtos;
using Core.Dtos.ResponsesDto;
using Core.Messages;
using Core.Ports.Auth;
using MediatR;

public class GetAuthUrlQueryHandler(
    IOAuthService oAuthService
) : IRequestHandler<GetAuthUrlQuery, ResultDto<GetAuthUrlResponseDto>>
{
    public async Task<ResultDto<GetAuthUrlResponseDto>> Handle(
        GetAuthUrlQuery request,
        CancellationToken cancellationToken)
    {
        var url = oAuthService.GetAuthorizationUrl(request.Provider!);

        var result = ResultDto<GetAuthUrlResponseDto>.Success(new() { Url = url });
        result.Message = Message.GetAllData;

        return result;
    }
}
