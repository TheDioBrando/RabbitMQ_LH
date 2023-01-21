using Broker.Requests;
using Broker.Responses;
using BrokerRequests;
using ClientSide.Commands.Libraries;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            [FromServices] CreateLibraryCommand command,
            [FromBody] CreateLibraryRequest request)
        {
            var response = await command.Execute(request);

            HttpContext.Response.StatusCode = response.IsSuccess
                ? (int)HttpStatusCode.Created
                : (int)HttpStatusCode.BadRequest;

            return response;
        }

        //[HttpPut]
        //public Task<UpdateLibraryResponse> Update(
        //    [FromServices] IRequestClient<UpdateLibraryRequest> requestClient,
        //    [FromBody] UpdateLibraryRequest request)
        //{

        //    HttpContext.Response.StatusCode = response.IsSuccess
        //        ? (int)HttpStatusCode.OK
        //        : (int)HttpStatusCode.BadRequest;

        //    return null;
        //}

        //[HttpGet]
        //public Task<ReadLibrariesResponse> Read(
        //    [FromServices] IRequestClient<ReadLibraryRequest> requestClient,
        //    [FromBody] ReadLibraryRequest request)
        //{
        //    HttpContext.Response.StatusCode = !response.Addresses.IsNullOrEmpty()
        //        ? (int)HttpStatusCode.OK
        //        : (int)HttpStatusCode.NoContent;

        //    return null;
        //}

        //[HttpDelete]
        //public Task<DeleteLibraryResponse> Delete(
        //    [FromServices] IRequestClient<DeleteLibraryRequest> requestClient,
        //    [FromBody] DeleteLibraryRequest request)
        //{
        //    HttpContext.Response.StatusCode = response.IsSuccess
        //        ? (int)HttpStatusCode.OK
        //        : (int)HttpStatusCode.BadRequest;

        //    return null;
        //}
    }
}
