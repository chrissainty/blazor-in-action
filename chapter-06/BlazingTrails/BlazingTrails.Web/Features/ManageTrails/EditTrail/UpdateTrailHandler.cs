using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using MediatR;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingTrails.Web.Features.ManageTrails.EditTrail
{
    public class UpdateTrailHandler : IRequestHandler<UpdateTrailRequest, UpdateTrailRequest.Response>
    {
        private readonly HttpClient _httpClient;

        public UpdateTrailHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UpdateTrailRequest.Response> Handle(UpdateTrailRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PutAsJsonAsync(UpdateTrailRequest.RouteTemplate, request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var updatedSuccessfully = await response.Content.ReadFromJsonAsync<bool>(cancellationToken: cancellationToken);
                return new UpdateTrailRequest.Response(updatedSuccessfully);
            }
            else
            {
                return new UpdateTrailRequest.Response(false);
            }
        }
    }
}
