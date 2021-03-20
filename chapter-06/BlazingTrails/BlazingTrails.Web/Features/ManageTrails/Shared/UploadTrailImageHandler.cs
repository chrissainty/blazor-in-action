using BlazingTrails.Shared.Features.ManageTrails.Shared;
using MediatR;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingTrails.Web.Features.ManageTrails.Shared
{
    public class UploadTrailImageHandler : IRequestHandler<UploadTrailImageRequest, UploadTrailImageRequest.Response>
    {
        private readonly HttpClient _httpClient;

        public UploadTrailImageHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UploadTrailImageRequest.Response> Handle(UploadTrailImageRequest request, CancellationToken cancellationToken)
        {
            var fileContent = request.File.OpenReadStream(request.File.Size, cancellationToken);

            using var content = new MultipartFormDataContent();
            content.Add(new StreamContent(fileContent), "image", request.File.Name);

            var response = await _httpClient.PostAsync(UploadTrailImageRequest.RouteTemplate.Replace("{trailId}", request.TrailId.ToString()), content, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var uploadSuccessful = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
                return new UploadTrailImageRequest.Response(uploadSuccessful);
            }
            else
            {
                return new UploadTrailImageRequest.Response("");
            }
        }
    }
}
