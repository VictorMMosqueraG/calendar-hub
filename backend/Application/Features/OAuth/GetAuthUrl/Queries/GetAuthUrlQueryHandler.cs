namespace Application.Features.OAuth.GetAuthUrl.Queries;

using Application.Features.OAuth.GetAuthUrl.Dtos;
using Application.Features.OAuth.GetAuthUrl.Interfaces;
using Core.Dtos.ResponsesDto;
using Core.Messages;
using MediatR;

public class GetAuthUrlQueryHandler(
    IAuthUrlBuilder authUrlBuilder
) : IRequestHandler<GetAuthUrlQuery, ResultDto<GetAuthUrlResponseDto>>
{
    private readonly IAuthUrlBuilder _authUrlBuilder = authUrlBuilder;

    public Task<ResultDto<GetAuthUrlResponseDto>> Handle(
        GetAuthUrlQuery request,
        CancellationToken cancellationToken)
    {
        var authUrl = _authUrlBuilder.BuildAuthUrl(request.Provider);

        return Task.FromResult(new ResultDto<GetAuthUrlResponseDto>
        {
            Results = authUrl,
            Message = Message.GetAllData
        });
    }
}
