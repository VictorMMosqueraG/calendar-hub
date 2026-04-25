namespace Application.Features.OAuth.GetAuthUrl.Queries;

using Application.Features.OAuth.GetAuthUrl.Dtos;
using Application.Features.OAuth.GetAuthUrl.Services;
using Core.Dtos.ResponsesDto;
using Core.Messages;
using MediatR;

public class GetAuthUrlQueryHandler(
    AuthUrlBuilderService authUrlBuilderService
) : IRequestHandler<GetAuthUrlQuery, ResultDto<GetAuthUrlResponseDto>>
{
    private readonly AuthUrlBuilderService _authUrlBuilderService = authUrlBuilderService;

    public async Task<ResultDto<GetAuthUrlResponseDto>> Handle(
        GetAuthUrlQuery request,
        CancellationToken cancellationToken)
    {
        var authUrl = _authUrlBuilderService.Build(request.Provider);

        var result = ResultDto<GetAuthUrlResponseDto>.Success(authUrl);
        result.Message = Message.GetAllData;

        return result;
    }
}
