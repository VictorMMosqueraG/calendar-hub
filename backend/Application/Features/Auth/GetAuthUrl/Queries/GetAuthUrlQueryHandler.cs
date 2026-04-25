namespace Application.Features.Auth.GetAuthUrl.Queries;

using Application.Features.Auth.GetAuthUrl.Dtos;
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
        var url = _oAuthWrapper.GetAuthorizationUrl(request.Provider!);

        var result = ResultDto<GetAuthUrlResponseDto>.Success(new() { Url = url });
        result.Message = Message.GetAllData;

        return result;
    }
}
