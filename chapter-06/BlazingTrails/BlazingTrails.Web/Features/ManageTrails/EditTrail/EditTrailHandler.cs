using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using MediatR;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingTrails.Web.Features.ManageTrails.EditTrail
{
    public class EditTrailHandler : IRequestHandler<EditTrailRequest, EditTrailRequest.Response>
    {
        private readonly HttpClient _httpClient;

        public EditTrailHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EditTrailRequest.Response> Handle(EditTrailRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PutAsJsonAsync(EditTrailRequest.RouteTemplate, request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var updatedSuccessfully = await response.Content.ReadFromJsonAsync<bool>(cancellationToken: cancellationToken);
                return new EditTrailRequest.Response(updatedSuccessfully);
            }
            else
            {
                return new EditTrailRequest.Response(false);
            }
        }
    }
}
