using BlazingTrails.Shared.Features.Home.Shared;
using MediatR;
using System.Net.Http.Json;

namespace BlazingTrails.Client.Features.Home.Shared
{
    public class GetTrailsHandler : IRequestHandler<GetTrailsRequest, GetTrailsRequest.Response?>
    {
        private readonly HttpClient _httpClient;

        public GetTrailsHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetTrailsRequest.Response?> Handle(GetTrailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<GetTrailsRequest.Response>(GetTrailsRequest.RouteTemplate);
            }
            catch (HttpRequestException)
            {
                return default!;
            }
        }
    }
}
