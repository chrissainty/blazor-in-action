using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using MediatR;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingTrails.Web.Features.ManageTrails.EditTrail
{
    public class GetTrailHandler : IRequestHandler<GetTrailRequest, GetTrailRequest.Response>
    {
        private readonly HttpClient _httpClient;

        public GetTrailHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetTrailRequest.Response> Handle(GetTrailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<GetTrailRequest.Response>(GetTrailRequest.RouteTemplate.Replace("{trailId}", request.TrailId.ToString()));
            }
            catch (HttpRequestException)
            {
                return new GetTrailRequest.Response(null);
            }
        }
    }
}
