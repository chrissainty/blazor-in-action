﻿using BlazingTrails.Shared.Features.Home.Shared;
using MediatR;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingTrails.Web.Features.Home.Shared
{
    public class GetTrailsHandler : IRequestHandler<GetTrailsRequest, GetTrailsRequest.Response>
    {
        private readonly HttpClient _httpClient;

        public GetTrailsHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetTrailsRequest.Response> Handle(GetTrailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<GetTrailsRequest.Response>(GetTrailsRequest.RouteTemplate);
            }
            catch (HttpRequestException)
            {
                return new GetTrailsRequest.Response(null);
            }
        }
    }
}
