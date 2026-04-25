namespace Application.Features.Calendar.GetEvents.Queries;

using Application.Features.Calendar.GetEvents.Dtos;
using AutoMapper;
using Core.Ports.Providers;
using MediatR;

public class GetEventsQueryHandler(
    IEnumerable<ICalendarProvider> providers,
    IMapper mapper
) : IRequestHandler<GetEventsQuery, List<GetEventsResponseDto>>
{
    private readonly IReadOnlyList<ICalendarProvider> _providers = providers.ToList();
    private readonly IMapper _mapper = mapper;

    public async Task<List<GetEventsResponseDto>> Handle(
        GetEventsQuery    request,
        CancellationToken cancellationToken)
    {
        var activeProviders = _providers.Where(p => p.IsAvailable).ToList();

        if (activeProviders.Count == 0)
            return [];

        var tasks = activeProviders
            .Select(p => p.GetEventsAsync(request.From, request.To, cancellationToken));

        var results = await Task.WhenAll(tasks);

        return results
            .SelectMany(r => r)
            .OrderBy(e => e.Start)
            .Select(_mapper.Map<GetEventsResponseDto>)
            .ToList();
    }
}
