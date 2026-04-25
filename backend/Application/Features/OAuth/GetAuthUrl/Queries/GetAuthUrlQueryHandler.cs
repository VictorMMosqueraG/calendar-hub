namespace Application.Features.OAuth.GetAuthUrl.Queries;

using Application.Features.OAuth.GetAuthUrl.Dtos;
using Core.Dtos.ResponsesDto;
using Core.Messages;
using Application.Interfaces.Wrappers;
using MediatR;

public class GetAuthUrlQueryHandler(
    IOAuthWrapper oAuthWrapper
) : IRequestHandler<GetAuthUrlQuery, ResultDto<GetAuthUrlResponseDto>>
{
    private readonly IOAuthWrapper _oAuthWrapper = oAuthWrapper;

    public async Task<ResultDto<GetAuthUrlResponseDto>> Handle(
        GetAuthUrlQuery request,
        CancellationToken cancellationToken)
    {
        var authUrl = _oAuthWrapper.GetAuthorizationUrl(request.Provider);

        var result = ResultDto<GetAuthUrlResponseDto>.Success(authUrl);
        result.Message = Message.GetAllData;

        return result;
    }
}
