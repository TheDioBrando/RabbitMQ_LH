using Broker.Requests;
using Broker.Responses;
using BrokerRequests;
using ClientSide.Commands.Libraries;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerSide.Commands.Libraries;
using System.Net;

namespace ClientSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibController : ControllerBase
    {
        [HttpPost("post")]
        public async Task<BrokerResponse> PostUserController(
            [FromServices] IRequestClient<PostUserRequest> requestClient,
            [FromBody] PostUserRequest request)
        {
            var response = await requestClient.GetResponse<BrokerResponse>(request);

            return response.Message;
        }

        [HttpPost("create")]
        public async Task<CreateLibraryResponse> Create(
            [FromServices] IRequestClient<CreateLibraryRequest> requestClient,
            [FromServices] ICreateLibraryCommand command,
            [FromBody] CreateLibraryRequest request)
        {
            var response = await command.Execute(request);

            HttpContext.Response.StatusCode = response.IsSuccess
                ? (int)HttpStatusCode.Created
                : (int)HttpStatusCode.BadRequest;

            return response;
        }

        [HttpPut]
        public async Task<UpdateLibraryResponse> Update(
            [FromServices] IRequestClient<UpdateLibraryRequest> requestClient,
            [FromServices] IUpdateLibraryCommand command,
            [FromBody] UpdateLibraryRequest request)
        {
            var response = await command.Execute(request);

            HttpContext.Response.StatusCode = response.IsSuccess
                ? (int)HttpStatusCode.OK
                : (int)HttpStatusCode.BadRequest;

            return response;
        }

        [HttpGet]
        public async Task<ReadLibrariesResponse> Read(
            [FromServices] IRequestClient<ReadLibraryRequest> requestClient,
            [FromServices] IReadLibraryCommand command,
            [FromBody] ReadLibraryRequest request)
        {
            var response = await command.Execute(request);

            HttpContext.Response.StatusCode = !(response.Addresses.Count == 0)
                ? (int)HttpStatusCode.OK
                : (int)HttpStatusCode.NoContent;

            return response;
        }

        [HttpDelete]
        public async Task<DeleteLibraryResponse> Delete(
            [FromServices] IRequestClient<DeleteLibraryRequest> requestClient,
            [FromServices] IDeleteLibraryCommand command,
            [FromBody] DeleteLibraryRequest request)
        {
            var response = await command.Execute(request);

            HttpContext.Response.StatusCode = response.IsSuccess
                ? (int)HttpStatusCode.OK
                : (int)HttpStatusCode.BadRequest;

            return response;
        }
    }
}
